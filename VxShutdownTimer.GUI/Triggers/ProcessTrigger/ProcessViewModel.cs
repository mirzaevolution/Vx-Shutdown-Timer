using ShutdownLib;
using System;
using System.Diagnostics;
using System.Timers;
using Microsoft.Win32;
using System.IO;

namespace VxShutdownTimer.GUI.Triggers.ProcessTrigger
{
    public class ProcessViewModel:BindableBase
    {

        private int _second = 1;
        private bool _isRunning, _isEnabled = true;
        private string _shutdownType;
        private Timer _timer;
        private string _exeFile;
        public string ExeFile
        {
            get
            {
                return _exeFile;
            }
            set
            {
                OnPropertyChanged(ref _exeFile, value, nameof(ExeFile));
            }
        }
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
        public RelayCommand BrowseCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public override event EventHandler<string> Info;
        public override event EventHandler<string> Error;

        public ProcessViewModel()
        {
            Load();
        }

        private void Load()
        {
            StartCommand = new RelayCommand(OnStart, CanStart);
            BrowseCommand = new RelayCommand(OnBrowse, CanBrowse);
            CancelCommand = new RelayCommand(OnCancel, CanCancel);
            _timer = new Timer();
            _timer.Elapsed += TimerElapsed;

        }

       

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                bool found = false;
                foreach(Process process in Process.GetProcesses())
                {
                    if(process.ProcessName.ToLower() == ExeFile)
                    {
                        found = true;
                        break;
                    }
                }
                if(found)
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
        private void OnBrowse()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    FileName = "",
                    Filter = "Exe File (*.exe)|*.exe"
                };
                if (fileDialog.ShowDialog().Value)
                {
                    ExeFile = Path.GetFileNameWithoutExtension(fileDialog.FileName).ToLower();
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
