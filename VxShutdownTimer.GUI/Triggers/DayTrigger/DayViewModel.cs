using ShutdownLib;
using System;
using System.Timers;
namespace VxShutdownTimer.GUI.Triggers.DayTrigger
{
    public class DayViewModel:BindableBase
    {

        private int _second = 1;
        private bool _isRunning, _isEnabled = true;
        private string _shutdownType;
        private Timer _timer;
        private DateTime _currentDateTime;
        public int Second
        {
            get
            {
                return _second;
            }
            set
            {
                OnPropertyChanged(ref _second, value, nameof(Second));
            }
        }
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

        public DayViewModel()
        {
            Load();
        }

        private void Load()
        {
            StartCommand = new RelayCommand(OnStart, CanStart);
            CancelCommand = new RelayCommand(OnCancel, CanCancel);
            _timer = new Timer();
            _timer.Elapsed += TimerElapsed;

        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (_currentDateTime.Day!=DateTime.Now.Day)
                {
                    ProcessCommand(ShutdownType);
                    //to avoid error or non-shutdown/restart/log off operation
                    OnCancel();
                }
            }
            catch { }
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

        private void OnStart()
        {
            try
            {
                _currentDateTime = DateTime.Now;
                _timer.Interval = Second * 1000;
                IsRunning = true;
                IsEnabled = false;
                _timer.Start();
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
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
                _timer.Stop();
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
