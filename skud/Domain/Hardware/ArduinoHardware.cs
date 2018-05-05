namespace skud.Domain.Hardware
{
    /// <summary>
    /// Общается с Arduino, считывает карты и управляет турникетом
    /// </summary>
    public class ArduinoGateway : BaseHardware
    {
        public ArduinoGateway(AccessRequestDelegate requestHandler) : base(requestHandler)
        {
        }
    }
}
