using Azure;
using System.ComponentModel.DataAnnotations;

namespace GuideInvestimentosAPI.Application.ViewModels
{
    public class GetAssetViewModel
    {
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }

        public string VariationD1 { get; set; }
        public string VariationFirstDate { get; set; }
    }

    public class AssetViewModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(10, ErrorMessage = "The field {0} must be between {2} e {1} characters.", MinimumLength = 2)]
        public string Symbol { get; set; }
    }
}
