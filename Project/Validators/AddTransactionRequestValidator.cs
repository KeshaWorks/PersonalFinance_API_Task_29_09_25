using FluentValidation;
using Project.Models.TakedFromBody;

namespace Project.Validators
{
    /// <summary>
    /// Validator for right getting data from json
    /// </summary>
    public class AddTransactionRequestValidator : AbstractValidator<AddTransactionRequest>
    {
        public AddTransactionRequestValidator() 
        {
            RuleFor(x => x.СategoryName)
                .NotNull()
                .WithMessage("CategoryName не должен быть пустым");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Description не должен быть пустым");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount должен быть больше 0");
        }
    }
}