using CoreLib.Models;

namespace VxShutdownTimer.GUI
{
    public class ShutdownModelConverter
    {
        public static ShutdownModelEx ConvertTo(ShutdownModel model)
        {
            return new ShutdownModelEx
            {
                DateTime = model.DateTime,
                ShutdownType = model.ShutdownType,
                Repetition = model.Repetition
            };
        }
        public static ShutdownModel ConvertFrom(ShutdownModelEx modelEx)
        {
            return new ShutdownModel
            {
                DateTime = modelEx.DateTime,
                ShutdownType = modelEx.ShutdownType,
                Repetition = modelEx.Repetition
            };
        }
    }
}
