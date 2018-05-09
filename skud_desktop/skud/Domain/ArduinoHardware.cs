using System;
using System.Threading;
using System.IO.Ports;

namespace skud.Domain
{
    /// <summary>
    /// Общается с Arduino, считывает карты и управляет турникетом
    /// </summary>
    public sealed class ArduinoGateway : IDisposable

    {
        public delegate void AccessRequestHandler(ulong uid, Direction direction);

        public delegate void CardReadEventHandler(ulong uid);

        public event AccessRequestHandler AccessRequested;
        public event CardReadEventHandler CardRead;

        private static readonly Object s_lock = new Object();
        private static ArduinoGateway instance = null;

        private SerialPort _port;
        private PackageBuilder _builder;

        private ArduinoGateway()
        {
            _builder = new PackageBuilder(new byte[] { 0x37, 0x83 }, 4);
            _builder.PackageReceived += _builder_PackageReceived;

            _port = new SerialPort("COM3", 9600);
            _port.Open();
            _port.DataReceived += _port_DataReceived;
        }     

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int size = _port.BytesToRead;
            byte[] buffer = new byte [size];
            _port.Read(buffer, 0, size);

            _builder.ProcessPart(buffer);
        }

        private void _builder_PackageReceived(object sender, byte[] package)
        {
            ulong uid = BitConverter.ToUInt32(package, 0);

            if (CardRead != null)
                CardRead(uid);
            else if (AccessRequested != null)
                AccessRequested(uid, Direction.IN);            
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

        public void Dispose()
        {
            _port?.Dispose();
        }
    }
}
