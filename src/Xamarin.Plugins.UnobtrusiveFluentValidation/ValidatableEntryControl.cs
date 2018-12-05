using Xamarin.Forms;

namespace Xamarin.Plugins.UnobtrusiveFluentValidation
{
    /// <summary>
    /// A Entry user control that works with a EnhancedAbstractValidator view model.
    /// </summary>
    public class ValidatableEntryControl : StackLayout
    {
        /// <summary>
        /// The bindable property for the Entry's text value.
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

        /// <summary>
        /// The bindable property for whether the control is invalid.
        /// </summary>
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
        /// The bindable property for the error message that should appear to the user.
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
        /// The bindable property to bind to the view model.
        /// </summary>
        public static BindableProperty BindingNameProperty = BindableProperty.Create(nameof(BindingName),
                                                                                    typeof(string),
                                                                                    typeof(ValidatableEntryControl),
                                                                                    string.Empty,
                                                                                     BindingMode.OneWay,
                                                                                     propertyChanged: (bindable, oldValue, newValue) =>
                                                                                     {

                                                                                         bindable.SetBinding(ValidatableEntryControl.TextEntryProperty, new Binding($"{newValue}.Value"));
                                                                                         bindable.SetBinding(ValidatableEntryControl.IsInValidProperty, new Binding($"{newValue}.IsInValid"));
                                                                                         bindable.SetBinding(ValidatableEntryControl.MessageProperty, new Binding($"{newValue}.Message"));
                                                                                     });

        /// <summary>
        /// The property bound to the view model.
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
        /// The bindable property to bind the placeholder against.
        /// </summary>
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),
                                                                                    typeof(string),
                                                                                     typeof(ValidatableEntryControl),
                                                                                     string.Empty,
                                                                                     BindingMode.OneWay);
        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>The placeholder.</value>
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }


        /// <summary>
        /// A control that encapsulates an Entry and Label to simplify form validation.
        /// </summary>
        public ValidatableEntryControl()
        {
            Control.BindingContext = this;
            Control.SetBinding(Entry.TextProperty, "EntryText");
            Control.SetBinding(Entry.PlaceholderProperty, "Placeholder");

            Children.Add(Control);

            MessageLabel.SetBinding(VisualElement.IsVisibleProperty, "IsInValid");
            MessageLabel.SetBinding(Label.TextProperty, "Message");
            MessageLabel.BindingContext = this;
            MessageLabel.TextColor = Color.Red;

            Children.Add(MessageLabel);

        }

        /// <summary>
        /// A control that encapsulates an Entry and Label to simplify form validation.
        /// </summary>
        /// <param name="bindingName">Set the binding name when instainated.</param>
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

