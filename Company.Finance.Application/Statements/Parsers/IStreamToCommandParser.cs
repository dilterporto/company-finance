using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Company.Finance.Application.Statements.Commands;
using Company.Finance.Domain.Entities;
using Company.Finance.Domain.ValueObjects;
using FluentValidation.Results;
using HtmlAgilityPack;
using Humanizer;
using MediatR;

namespace Company.Finance.Application.Statements.Parsers
{
    public interface IStreamToCommandParser<TCommand> 
        where TCommand : IRequest
    {
        Task<(ValidationResult result, TCommand command)> Parse(IngestStatementFileCommand ingestStatementFileCommand);
    }

    public class OfxStatementStreamToCommandParser : IStreamToCommandParser<CreateStatementCommand>
    {
        public ValidationResult Result { get; set; } = new();
        
        public async Task<(ValidationResult result, CreateStatementCommand command)> Parse(
            IngestStatementFileCommand ingestStatementFileCommand)
        {
            var createStatementCommand = await this.ParseStream(ingestStatementFileCommand.Stream);
            
            return (this.Result, createStatementCommand);
        }

        private async Task<CreateStatementCommand> ParseStream(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            var fileContent = await streamReader.ReadToEndAsync();

            var fileXmlContent = fileContent.Substring(fileContent.IndexOf("<OFX>"));
            var htmlContent = new HtmlDocument();
            htmlContent.LoadHtml(fileXmlContent);
            var xmlContentDocument = new XmlDocument();
            xmlContentDocument.LoadXml(htmlContent.DocumentNode.InnerHtml);

            var balanceValue = ExtractValues(xmlContentDocument.GetElementsByTagName("balamt")[0].InnerText)[0];
            var statementValues = ExtractValues(xmlContentDocument.GetElementsByTagName("curdef")[0].InnerText);

            var currency = Enum.Parse<Currency>(statementValues[0]);
            var accountId = statementValues[2];
            var start = ExtractDate(statementValues[4].Substring(0, 8), "yyyyMMdd", "DTSTART");
            var end = ExtractDate(statementValues[5].Substring(0, 8), "yyyyMMdd", "DTEND");
            
            var transactions = new List<Transaction>(); 
            
            
            foreach (XmlNode stmttrn in xmlContentDocument.GetElementsByTagName("stmttrn"))
            {
                var transactionValues = ExtractValues(stmttrn.InnerText);

                var transaction = new Transaction
                {
                    Currency = currency,
                    Account = accountId,
                    Type = Enum.Parse<TransactionType>(transactionValues[0].ToLower().Pascalize()),
                    Amount = decimal.Parse(transactionValues[2], new CultureInfo("en-US")),
                    Memo = transactionValues[3],
                    Date = ExtractDate(transactionValues[1].Substring(0, 14)),
                };
                
                transactions.Add(transaction);
            }
            
            return new CreateStatementCommand
            {
                Balance = decimal.Parse(balanceValue, new CultureInfo("en-US")),
                Start = start,
                End = end, 
                Transactions = transactions
            };
        }

        private List<string> ExtractValues(string value)
        {
            try
            {
                return value.Split("\r\n")
                    .Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            }
            catch (Exception e)
            {
                this.Result.Errors.Add(new ValidationFailure("", e.Message, value));
                return null;
            }
        }

        private DateTime? ExtractDate(string date, string format = "yyyyMMddhhmmss", string propertyName = "date")
        {
            try
            {
                return DateTime.ParseExact(date, format, new CultureInfo("en-US"));
            }
            catch (Exception e)
            {
                this.Result.Errors.Add(new ValidationFailure(propertyName, e.Message));
                return null;
            }
        }
    }
}