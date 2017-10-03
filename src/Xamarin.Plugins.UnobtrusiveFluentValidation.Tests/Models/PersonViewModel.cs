using System;
using FluentValidation;
using FluentValidation.Attributes;
using Xamarin.Plugins.FluentValidation;

namespace Xamarin.Plugins.UnobtrusiveFluentValidation.Tests.Models
{
    [Validator(typeof(PersonValidator))]
    public class PersonViewModel : AbstractValidationViewModel
    {
        public ValidatableProperty<string> FirstName { get; set; } = new ValidatableProperty<string>();

        public ValidatableProperty<string> LastName { get; set; } = new ValidatableProperty<string>();

        public ValidatableProperty<DateTime> DateOfBirth { get; set; } = new ValidatableProperty<DateTime>();
    }

    public class PersonValidator : EnhancedAbstractValidator<PersonViewModel>
    {
        private const int MINIMUM_AGE = 18;

        public PersonValidator()
        {
            RuleForProp(a => a.FirstName)
                .NotNull();

            RuleForProp(p => p.LastName)
                .NotNull();

            RuleForProp(p => p.DateOfBirth)
                .LessThanOrEqualTo(DateTime.UtcNow.AddYears(MINIMUM_AGE * -1))
                .WithMessage("Age must be greater or equal to 18")
                .NotEmpty();
        
        }
    }
}
