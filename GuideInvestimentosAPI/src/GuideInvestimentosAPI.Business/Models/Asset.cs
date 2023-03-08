
namespace GuideInvestimentosAPI.Business.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }

        public Asset()
        {
            Id = Guid.NewGuid();
        }
    }
}