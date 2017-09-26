namespace Xamarin.Plugins.FluentValidation
{
	interface IValidatableProperty
	{
		bool IsInValid { get; }

		void SetError(string message);

		void ClearError();
	}
}
