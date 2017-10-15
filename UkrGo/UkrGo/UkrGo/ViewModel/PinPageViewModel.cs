using UkrGo.Views;
using Acr.UserDialogs;
using UkrGo.Extensions;
using UkrGo.Helpers;
using UkrGo.Controls.PinViewControl;

namespace UkrGo.ViewModel
{
    class PinPageViewModel
    {
        
        public PinViewModel PinViewModel { get; private set; } = new PinViewModel
        {
            TargetPinLength = 4,
            ValidatorFunc = (arg) =>
            {
                string s = string.Concat(arg);
                var sha = s.GetSHA();
                if (string.IsNullOrEmpty(Settings.PinCode))
                {
                    UserDialogs.Instance.ShowSuccess("Please reenter PIN");
                    Settings.PinCode = sha;
                    return false;
                }
                else
                {
                    if (sha == Settings.PinCode)
                    {
                        App.Page.Navigation.PushAsync(new TopicPage());
                        return true;
                    }
                    else
                    {
                        UserDialogs.Instance.ShowError("PIN is incorrect!");
                        return false;
                    }
                }
            }
        };
    }
}
