using System;
using System.ComponentModel;

namespace VxShutdownTimer.GUI
{
    
    public class BindableBase : INotifyPropertyChanged
    {
        public virtual event EventHandler<string> Error;
        public virtual event EventHandler<string> Info;
        protected virtual void OnPropertyChanged<PropType>(ref PropType member, PropType value, string propertyName)
        {
            if (Equals(member, value))
                return;
            member = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
