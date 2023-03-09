using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideInvestimentosAPI.Business.Models.Validations
{
    public class AssetValidation : AbstractValidator<Asset>
    {
        public AssetValidation()
        {
            //RuleFor(x => x.Currency)
            //    .NotNull()
            //    .NotEmpty().WithMessage("The property {PropertyName} must be inserted.")
            //    .MaximumLength(5)
            //    .WithMessage("The property {PropertyName} must be less than {MaxLength} characters");

            //RuleFor(x => x.Date)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("A propriedade {PropertyName} precisa ser fornecido.");

            //RuleFor(x => x.Value)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("The property {PropertyName} must be inserted.");

            //RuleFor(x => x.Symbol)
            //    .NotNull()
            //    .NotEmpty().WithMessage("The property {PropertyName} must be inserted.")
            //    .MaximumLength(10)
            //    .WithMessage("The property {PropertyName} must be less than {MaxLength} caracteres.");

            RuleFor(x => x.Currency)
                .NotNull()
                .NotEmpty().WithMessage("Yahoo API with error.")
                .MaximumLength(5)
                .WithMessage("Yahoo API with error.");

            RuleFor(x => x.Date)
                .NotNull()
                .NotEmpty()
                .WithMessage("Yahoo API with error.");

            RuleFor(x => x.Value)
                .NotNull()
                .NotEmpty()
                .WithMessage("Yahoo API with error.");

            RuleFor(x => x.Symbol)
                .NotNull()
                .NotEmpty().WithMessage("Yahoo API with error.")
                .MaximumLength(10)
                .WithMessage("Yahoo API with error.");
        }
    }
}
