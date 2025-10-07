using FluentValidation;
using Project.Models.TakedFromBody;

namespace Project.Validators
{
    /// <summary>
    /// Validator for right getting data from json
    /// </summary>
    public class AddLimitRequestValidator : AbstractValidator<AddLimitRequest>
    {
        public AddLimitRequestValidator() 
        {
            RuleFor(x => x.СategoryName)
                .NotNull();

            RuleFor(x => x.Limit)
                .GreaterThan(0).WithMessage("Limit должен быть больше 0");
        }
    }
}