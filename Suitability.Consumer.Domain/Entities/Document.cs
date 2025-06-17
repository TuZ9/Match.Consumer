namespace Suitability.Consumer.Domain.Entities
{
    public class Document
    {
        public Guid IdDocument { get; set; }
        public Guid DocumentType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? DocumentDescription { get; set; }
        public string? DocumentNumber { get; set; }        
        
    }
}
