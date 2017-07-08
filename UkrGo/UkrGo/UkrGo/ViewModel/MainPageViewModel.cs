using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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


            ObservableCollection<RowData> rows =
                await
                    WebManager.LoadItemsAsync(
                        Url + "&page=" + pageId);
            Rows.RemoveAt(Rows.Count - 1);
            foreach (RowData rowData in rows)
            {
                if (!Rows.Select(a => a.Link == rowData.Link).Any())
                    Rows.Add(rowData);
            }
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
            Rows = await WebManager.LoadItemsAsync(Url);
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
