using Azure;
using System.ComponentModel.DataAnnotations;

namespace GuideInvestimentosAPI.Application.ViewModels
{

    public class AssetViewModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(10, ErrorMessage = "The field {0} must be between {2} e {1} characters.", MinimumLength = 2)]
        public string Symbol { get; set; }
    }
}
