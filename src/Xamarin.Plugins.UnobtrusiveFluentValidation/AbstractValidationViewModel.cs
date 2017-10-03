using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using FluentValidation.Attributes;
using FluentValidation.Results;
using Xamarin.Forms;

namespace Xamarin.Plugins.FluentValidation
{
	public abstract class AbstractValidationViewModel
    {
		public ICommand ValidateCommand { get; set; }

        private readonly IDictionary<string, IValidatableProperty> _properties = new Dictionary<string, IValidatableProperty>();

        private void ClearProperties()
		{
			foreach (var prop in _properties.Where(a => a.Value.IsInValid))
			{
				prop.Value.ClearError();
			}
		}
         
		protected AbstractValidationViewModel()
		{
			var typeInfo = this.GetType()
							  .GetTypeInfo();

			_properties = typeInfo
			    .DeclaredProperties
                .Where(a => typeof(IValidatableProperty).GetTypeInfo().IsAssignableFrom(a.PropertyType.GetTypeInfo()))
                .ToDictionary(a => $"{a.Name}.Value", b => b.GetValue(this) as IValidatableProperty);

			ValidateCommand = new Command(() => Validate());
		}

		private ValidationResult GetValidationResult()
		{
			var attributeFactory = new AttributedValidatorFactory();

            var type = this.GetType();

			var validator = attributeFactory.GetValidator(type);

			var results = validator.Validate(this);

			return results;
		}

		protected virtual bool ValidateProperty(string propertyName)
		{
			if (!_properties.ContainsKey(propertyName))
			{
				return false;
			}

			var property = _properties[propertyName];

			var results = GetValidationResult();

			var error = results.Errors.FirstOrDefault(a => a.PropertyName.Equals(propertyName));

			if (error != null)
			{
				property.SetError(error.ErrorMessage);
			}
			else
			{
				property.ClearError();
			}

			return error == null;
		}

		protected virtual bool Validate()
		{
			var results = GetValidationResult();

			ClearProperties();

			foreach (var error in results.Errors)
			{
                var property = _properties[error.PropertyName];

				if (property != null)
				{
					property.SetError(error.ErrorMessage);
				}
			}

			return results.IsValid;
		}
	}
}
