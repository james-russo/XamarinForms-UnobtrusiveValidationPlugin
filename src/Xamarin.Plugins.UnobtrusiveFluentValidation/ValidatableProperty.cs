using System.ComponentModel;
using Xamarin.Plugins.FluentValidation;

namespace Xamarin.Plugins.UnobtrusiveFluentValidation
{

    /// <summary>
    /// A property wrapper that will be used in a corresponding validator.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
	public class ValidatableProperty<TType> : INotifyPropertyChanged, IValidatableProperty
	{
        /// <summary>
        /// 
        /// </summary>
        public ValidatableProperty()
        {
        }

        /// <summary>
        /// A constructor for the intial value.
        /// </summary>
        /// <param name="value"></param>
        public ValidatableProperty(TType @value)
        {
            Value = @value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private TType _value;

        /// <summary>
        /// The value of the given type.  Usually, the text that should be shown to the user.
        /// </summary>
		public TType Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				OnPropertyChanged(nameof(Value));
			}
		}

		private string _message;

        /// <summary>
        /// The message to show the user when invalid.
        /// </summary>
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
				OnPropertyChanged(nameof(Message));
				OnPropertyChanged(nameof(IsInValid));
			}
		}

        /// <summary>
        /// State whether or not field is valid.
        /// </summary>
		public bool IsInValid
		{
			get { return !string.IsNullOrEmpty(_message); }
		}

        /// <summary>
        /// Set a custom error message.
        /// </summary>
        /// <param name="message"></param>
		public void SetError(string message)
		{
			Message = message;
		}

        /// <summary>
        /// Clear the error on the control.
        /// </summary>
        public void ClearError()
        {
            Message = string.Empty;
        }
	}
}
