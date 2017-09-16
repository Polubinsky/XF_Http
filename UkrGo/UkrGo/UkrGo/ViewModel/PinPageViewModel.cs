using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsPinView.PCL;
using UkrGo.Views;

namespace UkrGo.ViewModel
{
    class PinPageViewModel
    {
        public PinViewModel PinViewModel { get; private set; } = new PinViewModel
        {
            TargetPinLength = 4,
            ValidatorFunc = (arg) =>
            {
                App.Page.Navigation.PushAsync(new TopicPage());
                return true;
            }
        };
    }
}
