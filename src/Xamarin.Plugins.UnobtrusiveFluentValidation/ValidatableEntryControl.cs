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

        public static BindableProperty BindingNameProperty = BindableProperty.Create(nameof(BindingName),
                                                                                    typeof(string),
                                                                                    typeof(ValidatableEntryControl),
                                                                                    string.Empty,
                                                                                    BindingMode.OneWay);

        public string BindingName
        {
            get
            {
                return (string)GetValue(BindingNameProperty);
            }
            set
            {
                SetValue(BindingNameProperty, value);
            }
        }

        public ValidatableEntryControl()
        {
            Control.BindingContext = this;
            Control.SetBinding(Entry.TextProperty, "EntryText");

            Children.Add(Control);

            MessageLabel.SetBinding(VisualElement.IsVisibleProperty, "IsInValid");
            MessageLabel.SetBinding(Label.TextProperty, "Message");
            MessageLabel.BindingContext = this;
            MessageLabel.TextColor = Color.Red;

            Children.Add(MessageLabel);

            SetBinding(ValidatableEntryControl.TextEntryProperty, new Binding($"{BindingName}.Value"));
            SetBinding(ValidatableEntryControl.IsInValidProperty, new Binding($"{BindingName}.IsInValid"));
            SetBinding(ValidatableEntryControl.MessageProperty, new Binding($"{BindingName}.Message"));
        }

        public ValidatableEntryControl(string bindingName) : this()
        {
            BindingName = bindingName;           
        }

        public Entry Control = new Entry();

        public Label MessageLabel = new Label();
    }
}

