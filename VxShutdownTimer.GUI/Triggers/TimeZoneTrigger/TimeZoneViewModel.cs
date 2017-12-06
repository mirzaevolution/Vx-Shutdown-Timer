using System;
using ShutdownLib;
using Microsoft.Win32;
namespace VxShutdownTimer.GUI.Triggers.TimeZoneTrigger
{
    public class TimeZoneViewModel:BindableBase
    {
        private bool _isRunning, _isEnabled = true;
        private string _shutdownType;
        private string _currentTimezone;
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
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public override event EventHandler<string> Info;
        public override event EventHandler<string> Error;

        public TimeZoneViewModel()
        {
            Load();
        }

        private void Load()
        {
            StartCommand = new RelayCommand(OnStart, CanStart);
            CancelCommand = new RelayCommand(OnCancel, CanCancel);

        }

       
        private void ProcessCommand(string shutdownType)
        {
            try
            {
                switch (shutdownType)
                {
                    case "Shutdown":
                        Console.WriteLine("Shutdown");
                        //ShutdownInvoker.InvokeShutdown();
                        break;
                    case "Hibernate":
                        Console.WriteLine("Hibernate");
                        //ShutdownInvoker.SetSuspendState(true, true, true);
                        break;
                    case "Restart":
                        Console.WriteLine("Restart");
                        //ShutdownInvoker.InvokeRestart();
                        break;
                    case "Sleep":
                        Console.WriteLine("Sleep");
                        //ShutdownInvoker.SetSuspendState(false, true, true);
                        break;
                    case "Log Off":
                        Console.WriteLine("Log Off");
                        //ShutdownInvoker.ExitWindowsEx(0, 0);
                        break;
                    case "Lock":
                        Console.WriteLine("Lock");
                        //ShutdownInvoker.LockWorkStation();
                        break;
                }
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        private void OnStart()
        {
            try
            {
                TimeZoneInfo.ClearCachedData();
                _currentTimezone = TimeZoneInfo.Local.DisplayName;
                SystemEvents.TimeChanged += TimeZoneChanged;
                IsRunning = true;
                IsEnabled = false;
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        private void TimeZoneChanged(object sender, EventArgs e)
        {
            TimeZoneInfo.ClearCachedData();
            if (TimeZoneInfo.Local.DisplayName != _currentTimezone)
            {
                ProcessCommand(ShutdownType);
                //to avoid error or non-shutdown/restart/log off operation
                OnCancel();
            }
        }

        private bool CanStart()
        {
            return !IsRunning;
        }

        private void OnCancel()
        {
            try
            {
                SystemEvents.TimeChanged -= TimeZoneChanged;
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
