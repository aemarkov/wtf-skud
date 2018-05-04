using skud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skud.ViewModels
{
    /// <summary>
    /// Модель вида для строчки лога
    /// </summary>
    public class LogItemViewModel
    {        
        public DateTime Date { get; set; }
        public ulong CardId { get; set; }
        public string Fio { get; set; }
        public Direction Direction { get; set; }
        public AccessStatus Status { get; set; }
    }
}
