namespace Company.Finance.Models.ValueObjects
{
    public class Account
    {
        public string AccountId { get; set; }

        public static implicit operator Account(string accountId)
        {
            return new()
            {
                AccountId = accountId
            };
        }

        public override string ToString()
        {
            return this.AccountId;
        }

        public override bool Equals(object? obj)
        {
            return this.AccountId == ((Account) obj)?.AccountId;
        }
    }
}