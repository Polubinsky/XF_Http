using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.Model;

namespace UkrGo.ViewModel
{
    public class ImagePageViewModel : BaseViewModel
    {
        public ImagePageViewModel(DetailData data)
        {
            Data = data;
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
