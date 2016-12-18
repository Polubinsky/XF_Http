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
                IsLoading = true;
                IsDataReady = false;
                Task.Run(async () =>
                {
                    //await Task.Delay(5000);
                    Data = await WebManager.GetDataByLink(_link);
                    IsLoading = false;
                    IsDataReady = true;
                }
                );

                OnPropertyChanged("Link");
                
            }
        }
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value)
                    return;

                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private bool _isDataReady;

        public bool IsDataReady
        {
            get { return _isDataReady; }
            set
            {
                if (_isDataReady == value)
                    return;

                _isDataReady = value;
                OnPropertyChanged("IsDataReady");
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
