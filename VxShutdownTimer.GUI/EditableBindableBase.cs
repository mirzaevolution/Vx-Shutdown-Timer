using System.ComponentModel;
namespace VxShutdownTimer.GUI
{
    public class EditableBindableBase<T> : BindableBase, IEditableObject
    {

        private T _temp;

        protected virtual T Property { get; set; }

        public void BeginEdit()
        {
            _temp = Property;

        }

        public void CancelEdit()
        {
            if (!Equals(_temp, null))
                Property = _temp;
        }

        public void EndEdit()
        {
            _temp = default(T);
        }
    }
}
