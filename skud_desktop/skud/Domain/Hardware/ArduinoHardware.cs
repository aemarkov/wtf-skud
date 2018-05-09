using System;
using System.Threading;

namespace skud.Domain.Hardware
{
    /// <summary>
    /// Общается с Arduino, считывает карты и управляет турникетом
    /// </summary>
    public sealed  class ArduinoGateway
    {
        public delegate void AccessRequestHandler(ulong uid, Direction direction);
        public delegate void CardReadEventHandler(ulong uid);

        public event AccessRequestHandler AccessRequested;
        public event CardReadEventHandler CardRead;

        private static readonly Object s_lock = new Object();
        private static ArduinoGateway instance = null;

        private ArduinoGateway()
        {
        }

        public static ArduinoGateway Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(s_lock);
                var temp = new ArduinoGateway();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }

        public void SetAccess(AccessStatus status)
        {
            
        }
    }
}
