using System.ComponentModel;
using Xamarin.Forms;

namespace UkrGo.ViewModel
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public INavigation Navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
