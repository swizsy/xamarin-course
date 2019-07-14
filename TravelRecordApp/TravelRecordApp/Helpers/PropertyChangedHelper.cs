using System.ComponentModel;

namespace TravelRecordApp.Helpers
{
    public abstract class PropertyChangedHelper
    {
        public static void RaisePropertyChangedEvent(string propertyName, PropertyChangedEventHandler propertyChanged)
        {
            propertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
