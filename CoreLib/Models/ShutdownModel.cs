using System;
namespace CoreLib.Models
{
    public enum ShutdownType
    {
        Shutdown,
        Restart,
        Hibernate,
        Sleep,
        LogOff,
        Lock
    }
    public enum Repetition
    {
        None,
        Daily,
        Weekly,
        Monthly
    }
    public class ShutdownModel
    {
        public DateTime DateTime { get; set; }
        public ShutdownType ShutdownType { get; set; }
        public Repetition Repetition { get; set; }
    }
}
