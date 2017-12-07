using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace ShutdownLib
{
    public class ShutdownInvoker
    {
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        [DllImport("user32")]
        public static extern void LockWorkStation();


        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSLogoffSession(IntPtr hServer, int SessionId, bool bWait);


        [DllImport("kernel32.dll")]
        static extern int WTSGetActiveConsoleSessionId();


        [DllImport("wtsapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr WTSOpenServer(string pServerName);

        public static void InvokeShutdown()
        {
            try
            {
                Process.Start("shutdown", "/s /f /t 00");
            }
            catch { }
        }
        public static void InvokeRestart()
        {
            try
            {
                Process.Start("shutdown", "/r /f /t 00");
            }
            catch { }
        }
        public static void InvokeLogOffSvc()
        {
            try
            {
                int id = WTSGetActiveConsoleSessionId();
                IntPtr ptr = WTSOpenServer(Environment.MachineName);
                WTSLogoffSession(ptr, id, false);
            }
            catch { }
        }
    }
}
