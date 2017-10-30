using Acr.UserDialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UkrGo.Helpers;
using UkrGo.Model;
using Xamarin.Forms;

namespace UkrGo.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {

        private int pageId = 1;
        private ObservableCollection<RowData> _rows = new ObservableCollection<RowData>();



        public ObservableCollection<RowData> Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                OnPropertyChanged("Rows");
            }
        }

        private ObservableCollection<RowData> RemoveDuplicateRows(ObservableCollection<RowData> rows)
        {
            if (Settings.RemoveDublicate)
                return new ObservableCollection<RowData>(rows.GroupBy(x => x.MainImage.ImageSize).Select(y => y.First()).ToList());
            else return rows;
        }

        public MainPageViewModel(string url)
        {
            _url = url;
            Task.Run(() =>
                RefreshCommand.Execute(null));
        }




        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }


        private Command _nextDataCommand;

        public Command NextDataCommand
        {
            get { return _nextDataCommand ?? (_nextDataCommand = new Command(async () => await UpdateNextRowsCommand())); }
        }

        string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                OnPropertyChanged("Url");
            }
        }
        private async Task UpdateNextRowsCommand()
        {
            if (IsBusy)
                return;

            pageId++;
            IsBusy = true;
            RefreshCommand.ChangeCanExecute();

            string url = Url + "&page=" + pageId;
           
            ObservableCollection<RowData> rows = await WebManager.LoadItemsAsync(url);
            Rows.RemoveAt(Rows.Count - 1);
            foreach (RowData rowData in rows)
            {
                Rows.Add(rowData);
            }
            Rows = RemoveDuplicateRows(Rows);
            Rows.Add(GetMoreRowData());
            IsBusy = false;
            RefreshCommand.ChangeCanExecute();
        }
        private Command _refreshCommand;

        public Command RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new Command(async () => await UpdateRowsCommand())); }
        }

        private async Task UpdateRowsCommand()
        {
            if (string.IsNullOrEmpty(Url)) return;
            if (IsBusy)
                return;
            pageId = 1;
            IsBusy = true;
            RefreshCommand.ChangeCanExecute();
            var rows = await WebManager.LoadItemsAsync(Url);
            Rows = RemoveDuplicateRows(rows);
            Rows.Add(GetMoreRowData());
            IsBusy = false;
            RefreshCommand.ChangeCanExecute();
        }

        
        RowData GetMoreRowData()
        {
            RowData rd = new RowData();
            rd.MainImage.ImageLink = "more.png";
            return rd;
        }
    }
}
