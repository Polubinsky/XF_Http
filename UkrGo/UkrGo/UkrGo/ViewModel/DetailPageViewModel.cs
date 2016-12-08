using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.Model;

namespace UkrGo.ViewModel
{
    public class DetailPageViewModel : BaseViewModel
    {

        public DetailPageViewModel(string link)
        {
            Link = link;
        }

        private DetailData _data;

        public DetailData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }


        private string _link;

        public string Link
        {
            get { return _link; }
            set
            {
                if (value == _link) return;
                _link = value;
                Task.Run(async () =>
                    Data = await WebManager.GetDataByLink(_link));
                OnPropertyChanged("Link");
            }
        }

        private int _position;
        public int Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }
    }
}
