using NUnit.Framework;
using Xamarin.Plugins.UnobtrusiveFluentValidation.Tests.Models;
using FluentValidation.Attributes;
using System;

namespace Xamarin.Plugins.UnobtrusiveFluentValidation.Tests
{
    [TestFixture()]
    public class ValidationTest
    {
        [Test()]
        public void PersonViewModel_New()
        {
            var personViewModel = new PersonViewModel();

			var attributeFactory = new AttributedValidatorFactory();

			var type = personViewModel.GetType();

			var validator = attributeFactory.GetValidator(type);

			var results = validator.Validate(personViewModel);

            Assert.IsFalse(results.IsValid);

            Assert.AreEqual($"'{nameof(PersonViewModel.FirstName)}' must not be empty.", results.Errors[0].ErrorMessage);

			Assert.AreEqual($"'{nameof(PersonViewModel.LastName)}' must not be empty.", results.Errors[1].ErrorMessage);

			Assert.AreEqual($"'{nameof(PersonViewModel.DateOfBirth)}' should not be empty.", results.Errors[2].ErrorMessage);
        }

        [Test]
        public void PersonViewModel_HasName()
        {
            var personViewModel = new PersonViewModel(){
                FirstName = new ValidatableProperty<string>("John"),
                LastName = new ValidatableProperty<string>("Doe"),
                DateOfBirth = new ValidatableProperty<DateTime>(DateTime.Now.AddYears(-10))
            };

			var attributeFactory = new AttributedValidatorFactory();

			var type = personViewModel.GetType();

			var validator = attributeFactory.GetValidator(type);

			var results = validator.Validate(personViewModel);

			Assert.IsFalse(results.IsValid);

            Assert.AreEqual(1, results.Errors.Count);

			Assert.AreEqual($"Age must be greater or equal to 18", results.Errors[0].ErrorMessage);

		}

        [Test]
        public void PersonViewModel_Valid()
        {
			var personViewModel = new PersonViewModel()
			{
				FirstName = new ValidatableProperty<string>("John"),
				LastName = new ValidatableProperty<string>("Doe"),
                DateOfBirth = new ValidatableProperty<System.DateTime>(System.DateTime.Now.AddYears(-20))
			};

			var attributeFactory = new AttributedValidatorFactory();

			var type = personViewModel.GetType();

			var validator = attributeFactory.GetValidator(type);

			var results = validator.Validate(personViewModel);

			Assert.IsTrue(results.IsValid);
        }
    }
}
