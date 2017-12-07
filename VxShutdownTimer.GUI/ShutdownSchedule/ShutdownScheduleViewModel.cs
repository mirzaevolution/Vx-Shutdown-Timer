using System;
using ShutdownLib;
using System.Collections.ObjectModel;
using System.Timers;

namespace VxShutdownTimer.GUI.ShutdownSchedule
{
    public class ShutdownSelection : BindableBase
    {
        private string _shutdownType;
        private string _imgLocation;
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
        public string ImageLocation
        {
            get
            {
                return _imgLocation;
            }
            set
            {
                OnPropertyChanged(ref _imgLocation, value, nameof(ImageLocation));
            }
        }
    }
    public class ShutdownScheduleViewModel:BindableBase
    {
        private TimeSpan _timeout;
        private Timer _timer;
        private string _selectedShutdownType;
        private bool _isRunning, _isEnabled = true;
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
        public TimeSpan TimeOut
        {
            get
            {
                return _timeout;
            }
            set
            {
                OnPropertyChanged(ref _timeout, value, nameof(TimeOut));
            }
        }
        public ObservableCollection<ShutdownSelection> ShutdownSelectionCollection { get; set; }
        public RelayCommand<string> StartCommand { get; private set; }
        public RelayCommand<string> ImmediateStartCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public event EventHandler<TimeSpan> Tick;
        public override event EventHandler<string> Error;
        public override event EventHandler<string> Info;
        public ShutdownScheduleViewModel()
        {
            Load();
        }
        private void Load()
        {
            _timer = new Timer
            {
                Interval = 1000
            };
            _timer.Elapsed += TimerElapsed;
            ShutdownSelectionCollection = new ObservableCollection<ShutdownSelection>
            {
                new ShutdownSelection
                {
                    ShutdownType = "Shutdown",
                    ImageLocation = "/Contents/shutdown.ico"
                },
                new ShutdownSelection
                {
                    ShutdownType = "Hibernate",
                    ImageLocation = "/Contents/hibernate.ico"
                },
                new ShutdownSelection
                {
                    ShutdownType = "Restart",
                    ImageLocation = "/Contents/restart.ico"
                },
                new ShutdownSelection
                {
                    ShutdownType = "Sleep",
                    ImageLocation = "/Contents/sleep.ico"
                },
                new ShutdownSelection
                {
                    ShutdownType = "Log Off",
                    ImageLocation = "/Contents/logoff.ico"
                },
                new ShutdownSelection
                {
                    ShutdownType = "Lock",
                    ImageLocation = "/Contents/lock.ico"
                }

            };
            StartCommand = new RelayCommand<string>(OnStart, CanStart);
            ImmediateStartCommand = new RelayCommand<string>(OnImmediateStart);
            CancelCommand = new RelayCommand(OnCancel, CanCancel);
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

        private void OnStart(string obj)
        {
            if(TimeOut<=DateTime.Now.TimeOfDay)
            {
                OnErrorOccured("Timeout value cannot be less than current time");
            }
            else
            {
                _selectedShutdownType = obj;
                IsRunning = true;
                IsEnabled = false;
                _timer.Start();
            }
        }

        private bool CanStart(string arg)
        {
            return !IsRunning;
        }

        private void OnImmediateStart(string obj)
        {
            ProcessCommand(obj);
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
        protected virtual void OnTimerTick(TimeSpan e)
        {
            Tick?.Invoke(this, e);
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            OnTimerTick(now);
            if ((now.Seconds == TimeOut.Seconds) &&
               (now.Minutes == TimeOut.Minutes) &&
               (now.Hours == TimeOut.Hours))
            {
                ProcessCommand(_selectedShutdownType);
                OnCancel();
            }
        }
    }
}
