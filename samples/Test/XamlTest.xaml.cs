using System;
using System.Collections.Generic;
using FluentValidation;
using Xamarin.Forms;
using Xamarin.Plugins.FluentValidation;
using Xamarin.Plugins.UnobtrusiveFluentValidation;

namespace Test
{
    public partial class XamlTest : ContentPage
    {
        public XamlTest()
        {
            this.BindingContext = new XamlTestViewModel();

            InitializeComponent();
        }
    }

    [FluentValidation.Attributes.Validator(typeof(XamlTestValidator))]
    public class XamlTestViewModel : AbstractValidationViewModel
    {
        public ValidatableProperty<string> Name
        {
            get;
            set;
        } = new ValidatableProperty<string>();

        protected override bool Validate()
        {
            var status = base.Validate();

            return status;
        }
    }

    public class XamlTestValidator : EnhancedAbstractValidator<XamlTestViewModel>
    {
        public XamlTestValidator()
        {
            RuleForProp(a => a.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
