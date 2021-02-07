using System;
using Company.Finance.Models.Entities;
using Company.Finance.Models.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Finance.Migrations
{
    public partial class CreateTranscationsAndStatementsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:currency", "brl")
                .Annotation("Npgsql:Enum:transaction_type", "credit,debit,other");

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Transactions = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Account = table.Column<string>(type: "text", nullable: true),
                    Currency = table.Column<Currency>(type: "currency", nullable: false),
                    Type = table.Column<TransactionType>(type: "transaction_type", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Memo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
