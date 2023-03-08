namespace GuideInvestimentosAPI.Business.Models
{
    public class RootObject
    {
        public Chart chart { get; set; }
    }

    public class Chart
    {
        public Result[] result { get; set; }
    }

    public class Result
    {
        public Meta meta { get; set; }
        public long[] timestamp { get; set; }
        public Indicators indicators { get; set; }
    }


    public class Meta
    {
        public string symbol { get; set; }

        public string currency { get; set; }
    }

    public class Indicators
    {
        public Quote[] quote { get; set; }
    }

    public class Quote
    {
        public decimal?[] open { get; set; }
    }

}
