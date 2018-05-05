namespace skud.Domain.Hardware
{
    /// <summary>
    /// Базовый класс по работе с аппаратным обеспечением
    /// </summary>
    public abstract class BaseHardware
    {
        protected AccessRequestDelegate _accessRequestHandler;

        public BaseHardware(AccessRequestDelegate requestHandler)
        {
            _accessRequestHandler = requestHandler;
        }
    }
}