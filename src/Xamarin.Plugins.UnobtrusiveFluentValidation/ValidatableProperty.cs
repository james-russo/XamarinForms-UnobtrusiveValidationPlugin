using System.ComponentModel;
using Xamarin.Plugins.FluentValidation;

namespace Xamarin.Plugins.UnobtrusiveFluentValidation
{

	public class ValidatableProperty<TType> : INotifyPropertyChanged, IValidatableProperty
	{
        public ValidatableProperty()
        {

        }

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

		public bool IsInValid
		{
			get { return !string.IsNullOrEmpty(_message); }
		}


		public void SetError(string message)
		{
			Message = message;
		}

        public void ClearError()
        {
            Message = string.Empty;
        }
	}
}
