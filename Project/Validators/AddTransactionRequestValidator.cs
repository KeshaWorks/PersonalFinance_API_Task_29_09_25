using FluentValidation;
using Project.Models;

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
                .NotNull();

            RuleFor(x => x.Description)
                .NotNull();

            RuleFor(x => x.Amount)
                .GreaterThan(0);
        }
    }
}