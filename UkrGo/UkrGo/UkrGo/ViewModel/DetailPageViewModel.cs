﻿using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using UkrGo.Interfaces;
using UkrGo.Model;
using Xamarin.Forms;

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
                IsSaving = false;
                IsDataReady = false;
                Task.Run(async () =>
                {
                    //await Task.Delay(5000);
                    Data = await WebManager.GetDataByLink(_link);
                    IsLoading = false;
                    IsDataReady = true;
                    IsSaving = true;
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

        private bool _isSaving = true ;

        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                if (_isSaving == value)
                    return;

                _isSaving = value;
                OnPropertyChanged("IsSaving");
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

        private Command _saveCommand;
        public Command SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new Command
                    (async () =>
                    {
                        IsSaving = false;
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                        if (status != PermissionStatus.Granted)
                            status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage))[Permission.Storage];
                        if (status == PermissionStatus.Granted)
                        {
                            IScreenshotManager sm = DependencyService.Get<IScreenshotManager>();
                            byte[] b = await sm.CaptureAsync();
                            sm.SavePictureToDisk("ukrgo", b);
                        }
                        IsSaving = true;
                    }));
            }
        }
    }
}
