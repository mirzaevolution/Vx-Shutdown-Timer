using CoreLib.Models;
using System;

namespace VxShutdownTimer.GUI
{
    public class ShutdownModelEx:EditableBindableBase<ShutdownModelEx>
    {
        protected override ShutdownModelEx Property
        {
            get
            {
                return new ShutdownModelEx
                {
                    DateTime = _dateTime,
                    ShutdownType = _shutdownType,
                    Repetition = _repetition,
                    IsChecked = _isChecked
                };
            }
            set
            {
                ShutdownModelEx model = value;
                if(model!=null)
                {
                    DateTime = model.DateTime;
                    ShutdownType = model.ShutdownType;
                    Repetition = model.Repetition;
                    IsChecked = model.IsChecked;
                }
            }
        }
        private DateTime _dateTime;
        private ShutdownType _shutdownType;
        private Repetition _repetition;
        private bool _isChecked;
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                OnPropertyChanged(ref _dateTime, value, nameof(DateTime));
            }
        }
        public ShutdownType ShutdownType
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
        public Repetition Repetition
        {
            get
            {
                return _repetition;
            }
            set
            {
                OnPropertyChanged(ref _repetition, value, nameof(Repetition));
            }
        }
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                OnPropertyChanged(ref _isChecked, value, nameof(IsChecked));
            }
        }
    }

}
