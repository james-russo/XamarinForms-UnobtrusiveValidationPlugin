using Xamarin.Forms;

namespace Xamarin.Plugins.UnobtrusiveFluentValidation
{
    /// <summary>
    /// A Entry user control that works with a EnhancedAbstractValidator view model.
    /// </summary>
    public class ValidatableEntryControl : StackLayout
    {
        /// <summary>
        /// 
        /// </summary>
        public static BindableProperty TextEntryProperty = BindableProperty.Create(nameof(EntryText),
                                                                                   typeof(string),
                                                                                   typeof(ValidatableEntryControl),
                                                                                   string.Empty,
                                                                                   BindingMode.TwoWay);
        /// <summary>
        /// The value of the Entry displayed to the user.
        /// </summary>
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
        /// <summary>
        /// Status of control.
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public static BindableProperty MessageProperty = BindableProperty.Create(nameof(Message),
                                                                                   typeof(string),
                                                                                   typeof(ValidatableEntryControl),
                                                                                   string.Empty,
                                                                                   BindingMode.TwoWay);

        /// <summary>
        /// Message displayed to the user.
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public static BindableProperty BindingNameProperty = BindableProperty.Create(nameof(BindingName),
                                                                                    typeof(string),
                                                                                    typeof(ValidatableEntryControl),
                                                                                    string.Empty,
                                                                                     BindingMode.OneWay,
                                                                                     propertyChanged: (bindable, oldValue, newValue) => {
            
            bindable.SetBinding(ValidatableEntryControl.TextEntryProperty, new Binding($"{newValue}.Value"));
            bindable.SetBinding(ValidatableEntryControl.IsInValidProperty, new Binding($"{newValue}.IsInValid"));
            bindable.SetBinding(ValidatableEntryControl.MessageProperty, new Binding($"{newValue}.Message"));
        });

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingName"></param>
        public ValidatableEntryControl(string bindingName) : this()
        {
            BindingName = bindingName;           
        }

        /// <summary>
        /// The entry control shown to the user.
        /// </summary>
        public Entry Control = new Entry();

        /// <summary>
        /// The message label shown to the user.
        /// </summary>
        public Label MessageLabel = new Label();
    }
}

