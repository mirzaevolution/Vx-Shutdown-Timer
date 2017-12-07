using System;
using System.IO;
namespace CoreLib
{
    public class Global
    {
        public static string GetDataFileLocation()
        {
            string result = "";
            try
            {
                string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create);
                string path = Path.Combine(commonAppData, "Vx Shutdown Timer");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                result = Path.Combine(path, "vx.dat");
            }
            catch { result = ""; }
            return result;
        }
        public static string GetLogFileLocation()
        {
            string result = "";
            try
            {
                string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create);
                string path = Path.Combine(commonAppData, "Vx Shutdown Timer");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                result = Path.Combine(path, "log.txt");
            }
            catch { result = ""; }
            return result;
        }
    }
}
