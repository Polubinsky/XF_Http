using UkrGo.Model;

namespace UkrGo.ViewModel
{
    public class SettingsPageViewModel: BaseViewModel
    {
        public readonly SettingsData Data = new SettingsData();

        public bool AskPinCode
        {
            get
            {
                return Data.AskPinCode;
            }
            set
            {
                Data.AskPinCode = value;
                OnPropertyChanged("AskPinCode");
            }
        }

        public bool RemoveDuplicate
        {
            get
            {
                return Data.RemoveDuplicate;
            }
            set
            {
                Data.RemoveDuplicate = value;
                OnPropertyChanged("RemoveDuplicate");
            }
        }
    }
}
