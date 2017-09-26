using System;
using Xamarin.Forms;

namespace Xamarin.Plugins.FluentValidation
{
    public class ValidatableView : View
    {
        public View MainView { get; }

        public ValidatableView(View mainView, object bindingObject)
        {
            MainView = mainView;
        }

    }
}
