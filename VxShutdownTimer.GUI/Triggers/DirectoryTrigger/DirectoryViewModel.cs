using System;
using System.IO;
using System.Timers;
using ConnectivityLib;
using Microsoft.Win32;
using ShutdownLib;

namespace VxShutdownTimer.GUI.Triggers.DirectoryTrigger
{
    public class DirectoryViewModel:BindableBase
    {
        private bool _isRunning, _isEnabled = true;
        private FileSystemWatcher _watcher;
        private string _shutdownType, _location;
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                OnPropertyChanged(ref _isRunning, value, nameof(IsRunning));
            }
        }
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                OnPropertyChanged(ref _isEnabled, value, nameof(IsEnabled));
            }
        }
        public string ShutdownType
        {
            get
            {
                return _shutdownType;
            }
            set
            {
                OnPropertyChanged(ref _shutdownType, value, nameof(ShutdownType));
            }
        }
        public string DirectoryLocation
        {
            get
            {
                return _location;
            }
            set
            {
                OnPropertyChanged(ref _location, value, nameof(DirectoryLocation));
            }
        }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand BrowseCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public override event EventHandler<string> Info;
        public override event EventHandler<string> Error;

        public DirectoryViewModel()
        {
            Load();
        }
        private void ProcessCommand(string shutdownType)
        {
            try
            {
                switch (shutdownType)
                {
                    case "Shutdown":
                        ShutdownInvoker.InvokeShutdown();
                        break;
                    case "Hibernate":
                        ShutdownInvoker.SetSuspendState(true, true, true);
                        break;
                    case "Restart":
                        ShutdownInvoker.InvokeRestart();
                        break;
                    case "Sleep":
                        ShutdownInvoker.SetSuspendState(false, true, true);
                        break;
                    case "Log Off":
                        ShutdownInvoker.ExitWindowsEx(0, 0);
                        break;
                    case "Lock":
                        ShutdownInvoker.LockWorkStation();
                        break;
                }
            }
            catch (Exception ex)
            {
                OnCancel();
                OnErrorOccured(ex.Message);
            }
        }
        private void Load()
        {
            StartCommand = new RelayCommand(OnStart, CanStart);
            BrowseCommand = new RelayCommand(OnBrowse, CanBrowse);
            CancelCommand = new RelayCommand(OnCancel, CanCancel);
            _watcher = new FileSystemWatcher
            {
                NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName |
                NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size,
                IncludeSubdirectories = false
            };
        }

        private void OnWatcherChanged(object sender, FileSystemEventArgs e)
        {
            ProcessCommand(ShutdownType);
            //to avoid error or non-shutdown/restart/log off operation
            OnCancel();
        }

        private void OnWatcherCreated(object sender, FileSystemEventArgs e)
        {
            ProcessCommand(ShutdownType);
            //to avoid error or non-shutdown/restart/log off operation
            OnCancel();
        }

        private void OnWatcherDeleted(object sender, FileSystemEventArgs e)
        {
            ProcessCommand(ShutdownType);
            //to avoid error or non-shutdown/restart/log off operation
            OnCancel();
        }

        private void OnWatcherRenamed(object sender, RenamedEventArgs e)
        {
            ProcessCommand(ShutdownType);
            //to avoid error or non-shutdown/restart/log off operation
            OnCancel();
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            OnCancel();
        }

        private void OnStart()
        {
            try
            {
                _watcher.Changed += OnWatcherChanged;
                _watcher.Created += OnWatcherCreated;
                _watcher.Deleted += OnWatcherDeleted;
                _watcher.Renamed += OnWatcherRenamed;
                _watcher.Error += OnWatcherError;
                _watcher.Path = DirectoryLocation;
                _watcher.Filter = "*.*";
                _watcher.EnableRaisingEvents = true;
                IsRunning = true;
                IsEnabled = false;
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }

        }

        private bool CanStart()
        {
            return !IsRunning && !String.IsNullOrEmpty(DirectoryLocation);
        }

        private void OnBrowse()
        {
            try
            {

                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DirectoryLocation = dialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        private bool CanBrowse()
        {
            return !IsRunning;
        }

        private void OnCancel()
        {
            try
            {

                _watcher.EnableRaisingEvents = false;
                _watcher.Filter = "";
                _watcher.Changed -= OnWatcherChanged;
                _watcher.Created -= OnWatcherCreated;
                _watcher.Deleted -= OnWatcherDeleted;
                _watcher.Renamed -= OnWatcherRenamed;
                _watcher.Error -= OnWatcherError;
                IsRunning = false;
                IsEnabled = true;
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        private bool CanCancel()
        {
            return IsRunning;
        }

        protected virtual void OnInfoRequested(string e)
        {
            Info?.Invoke(this, e);
        }
        protected virtual void OnErrorOccured(string e)
        {
            Error?.Invoke(this, e);
        }
    }
}
