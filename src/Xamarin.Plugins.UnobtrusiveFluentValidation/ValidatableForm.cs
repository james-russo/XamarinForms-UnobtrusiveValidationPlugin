using Xamarin.Forms;

namespace Xamarin.Plugins.UnobtrusiveFluentValidation
{
    public class ValidatableEntryControl : StackLayout
    {
        public static BindableProperty TextEntryProperty = BindableProperty.Create(nameof(EntryText),
                                                                                   typeof(string),
                                                                                   typeof(ValidatableEntryControl),
                                                                                   string.Empty,
                                                                                   BindingMode.TwoWay);

        public string EntryText
        {
            get
            {
                return (string)GetValue(TextEntryProperty);
            }
            set
            {
                SetValue(TextEntryProperty, value);
            }
        }

        public static BindableProperty IsInValidProperty = BindableProperty.Create(nameof(IsInValid),
                                                                                   typeof(bool),
                                                                                   typeof(ValidatableEntryControl),
                                                                                   false,
                                                                                   BindingMode.TwoWay);

        public bool IsInValid
        {
            get
            {
                return (bool)GetValue(IsInValidProperty);
            }
            set
            {
                SetValue(IsInValidProperty, value);
            }
        }

        public static BindableProperty MessageProperty = BindableProperty.Create(nameof(Message),
                                                                                   typeof(string),
                                                                                   typeof(ValidatableEntryControl),
                                                                                   string.Empty,
                                                                                   BindingMode.TwoWay);

        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public ValidatableEntryControl(string bindingName)
        {
            Control.BindingContext = this;
            Control.SetBinding(Entry.TextProperty, "EntryText");

            Children.Add(Control);

            MessageLabel.SetBinding(VisualElement.IsVisibleProperty, "IsInValid");
            MessageLabel.SetBinding(Label.TextProperty, "Message");
            MessageLabel.BindingContext = this;
            MessageLabel.TextColor = Color.Red;

            Children.Add(MessageLabel);

            SetBinding(ValidatableEntryControl.TextEntryProperty, new Binding($"{bindingName}.Value"));
            SetBinding(ValidatableEntryControl.IsInValidProperty, new Binding($"{bindingName}.IsInValid"));
            SetBinding(ValidatableEntryControl.MessageProperty, new Binding($"{bindingName}.Message"));
        }

        public Entry Control = new Entry();

        public Label MessageLabel = new Label();
    }
}

