namespace Suitability.Consumer.Domain.Entities
{
    public class Position
    {
        public bool? ImportFromCRS { get; set; }
        public string? AccountNumber { get; set; }
        public string? PositionDate { get; set; }
        public bool? IsBlocked { get; set; }
        public decimal? TotalAmmount { get; set; }
        public DateTime Date { get; set; }
        public object SummaryAccountsd { get; set; }
        public object InvestmentFund { get; set; }
        public object FixedIncome { get; set; }
        public object Credits { get; set; }
        public object PensionInformations { get; set; }
        public object Equities { get; set; }
        public object Commodity { get; set; }
    }
}
