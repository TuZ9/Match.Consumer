namespace Suitability.Consumer.Domain.Entities
{
    public class Account
    {
        public Guid IdAccount { get; set; }
        public required string ClientName { get; set; }
        public required string CPF { get; set; }
        public required string RG { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public required string Email { get; set; }
        private string _accountNumber;
        public required string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length != 9 || !long.TryParse(value.Trim(), out _))
                {
                    throw new ArgumentException("Account number must be exactly 9 digits.");
                }
                _accountNumber = value.Trim();
            }
        }
    }
}
