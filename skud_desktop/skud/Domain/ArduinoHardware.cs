using System;
using System.Threading;
using System.IO.Ports;
using System.Configuration;
using System.Linq;
using skud.Properties;

namespace skud.Domain
{
    /// <summary>
    /// Общается с Arduino, считывает карты и управляет турникетом
    /// </summary>
    public sealed class ArduinoGateway : IDisposable
    {
        public delegate void AccessRequestHandler(ulong uid, Direction direction);
        public delegate void CardReadEventHandler(ulong uid);

        /// <summary>
        /// Событие запроса доступа
        /// </summary>
        public event AccessRequestHandler AccessRequested;

        /// <summary>
        /// Событие чтения карты (без запроса)
        /// </summary>
        public event CardReadEventHandler CardRead;

        private static readonly Object s_lock = new Object();
        private static ArduinoGateway instance = null;

        private SerialPort _port;
        private PackageBuilder _builder;
        private byte[] _sendPacket = new byte[1];



        public static void Init(string port)
        {
            if (instance != null)
                throw new Exception("Already initialized");

            Monitor.Enter(s_lock);
            var temp = new ArduinoGateway(port);
            Interlocked.Exchange(ref instance, temp);
            Monitor.Exit(s_lock);
        }

        private ArduinoGateway(string port)
        {
            _builder = new PackageBuilder(new byte[] { 0x37, 0x83 }, 5);
            _builder.PackageReceived += _builder_PackageReceived;

            _port = new SerialPort(port, 9600);
            _port.Open();
            _port.DataReceived += _port_DataReceived;
        }     

        // Получены данные по COM-порту
        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int size = _port.BytesToRead;
            byte[] buffer = new byte [size];
            _port.Read(buffer, 0, size);

            _builder.ProcessPart(buffer);
        }

        // Пакет полностью считан
        private void _builder_PackageReceived(object sender, byte[] package)
        {
            // Получаем UID карты
            ulong uid = BitConverter.ToUInt32(package, 0);

            // Получаем направление движения
            Direction dir;
            byte dirByte = package[sizeof(int)];
            if (dirByte == 0)
                dir = Direction.NONE;
            else if (dirByte == 1)
                dir = Direction.IN;
            else if (dirByte == 2)
                dir = Direction.OUT;
            else
                throw new Exception("Invalid direction value");
             

            if (CardRead != null)
                CardRead(uid);
            else if (AccessRequested != null && dir != Direction.NONE)
                AccessRequested(uid, dir);            
        }



        // Синглтон

        public static ArduinoGateway Instance
        {
            get
            {
                if (instance != null) return instance;
                throw new Exception("Not initialized");
            }
        }

        public void SendResponse(AccessStatus status)
        {
            if (status == AccessStatus.GRANTED)
                _sendPacket[0] = 1;
            else if (status == AccessStatus.DENIED)
                _sendPacket[0] = 2;
            else
                _sendPacket[0] = 0;        
            
            _port.Write(_sendPacket, 0, _sendPacket.Length);
        }

        public void Dispose()
        {
            _port?.Dispose();
        }

        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        /*public static string GetPort()
        {
            string com =  ConfigurationManager.AppSettings["com"];
            if (GetPorts().Contains(com))
                return com;
            else
                return null;
        }*/
    }
}
