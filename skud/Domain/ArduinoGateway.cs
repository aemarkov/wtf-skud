using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skud.Domain
{
    /// <summary>
    /// Общается с Arduino, считывает карты и управляет турникетом
    /// </summary>
    public class ArduinoGateway
    {
        AccessRequestDelegate _accessRequest;

        public ArduinoGateway(AccessRequestDelegate request)
        {
            _accessRequest = request;
        }
    }
}
