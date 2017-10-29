using FluentValidation;
using Xamarin.Forms;
using Xamarin.Plugins.UnobtrusiveFluentValidation;
using Xamarin.Plugins.FluentValidation;
namespace Test
{
    [FluentValidation.Attributes.Validator(typeof(TestValidator))]
    public class TestViewModel : AbstractValidationViewModel
    {
        public ValidatableProperty<string> FirstName { get; set; } = new ValidatableProperty<string>();

        public ValidatableProperty<string> LastName { get; set; } = new ValidatableProperty<string>();
    }

    public class TestValidator : EnhancedAbstractValidator<TestViewModel>
    {
        public TestValidator()
        {
            RuleForProp(a => a.FirstName)
                .NotNull()
                .NotEmpty();


            RuleForProp(a => a.LastName)
                .NotNull()
                .NotEmpty();
        }
    }


    public class TestPage : ContentPage
    {
        public TestPage()
        {
            BindingContext = new TestViewModel();

            var firstNameControl = new ValidatableEntryControl(nameof(TestViewModel.FirstName));

            var lastNameControl = new ValidatableEntryControl(nameof(TestViewModel.LastName));

            var sl = new StackLayout()
            {
                Orientation = StackOrientation.Vertical
            };

            sl.Children.Add(firstNameControl);
            sl.Children.Add(lastNameControl);

            var submitButton = new Button();

            submitButton.SetBinding(Button.CommandProperty, nameof(TestViewModel.ValidateCommand));

            sl.Children.Add(submitButton);

            this.Content = sl;
        }

    }

    public class App : Application
    {
        public App()
        {
            // The root page of your application
            var content = new XamlTest();

            //var content = new TestPage();

            MainPage = new NavigationPage(content);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
