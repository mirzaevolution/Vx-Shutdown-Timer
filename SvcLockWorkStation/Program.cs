using System.Runtime.InteropServices;

namespace SvcLockWorkStation
{
    class Program
    {
        [DllImport("user32")]
        public static extern void LockWorkStation();
        static void Main(string[] args)
        {
            try
            {
                LockWorkStation();
            }
            catch { }
        }
    }
}
