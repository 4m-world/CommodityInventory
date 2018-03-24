using System;
namespace MyInventoryApp.Controls
{
    public class FloatingActionEventArgs : EventArgs
    {
        public readonly string EventName;
        public readonly int EventIndex;
        public readonly string EventDescription;
        public readonly object CastObject;

        public FloatingActionEventArgs(string eventName, int eventIndex)
        {
            EventName = eventName;
            EventIndex = eventIndex;
        }

        public FloatingActionEventArgs(string eventName, object castObject)
        {
            EventName = eventName;
            CastObject = castObject;
        }

        public FloatingActionEventArgs(string eventName, int eventIndex, string eventDescription)
        {
            EventName = eventName;
            EventIndex = eventIndex;
            EventDescription = eventDescription;
        }
    }
}
