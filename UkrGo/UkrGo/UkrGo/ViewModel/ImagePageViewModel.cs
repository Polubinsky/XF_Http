using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrGo.ViewModel
{
    public class ImagePageViewModel : BaseViewModel
    {
        public ImagePageViewModel(string link)
        {
            Link = link;
        }

        private string _link;
        public string Link
        {
            get { return _link; }
            set
            {
                _link = value;
                OnPropertyChanged("Link");
            }
        }
    }
}
