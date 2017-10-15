using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UkrGo.Data;
using Xamarin.Forms;

namespace UkrGo.ViewModel
{
    public class TopicViewModel: BaseViewModel
    {
        public Topic CurrentTopic { get; set; }

        public ICommand DeleteCurrentTopicCommand { get; private set; }

        private ObservableCollection<Topic> _topics;
        public ObservableCollection<Topic> Topics
        {
            get { return _topics; }
            set
            {
                _topics = value;
                OnPropertyChanged("Topics");
            }
        }

        public TopicViewModel()
        {
            DeleteCurrentTopicCommand = new Command(DeleteCurrentTopic);
            _topics = new ObservableCollection<Topic>(Task.Run(async () => await App.Database.GetItemsAsync()).Result);
        }

        public void DeleteCurrentTopic(object s)
        {
            App.Database.DeleteItemAsync(s as Topic);
        }
    }
}
