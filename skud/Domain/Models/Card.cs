using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skud.Domain.Models
{
    /// <summary>
    /// Пропуск
    /// </summary>
    public class Card
    {        
        /// <summary>
        /// Уникальный идентификатор карты
        /// </summary>
        [Key]
        public ulong Uid { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Дата истечения
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Пользователь, кому выдана карта
        /// </summary>
        public User User { get; set; }
        public int UserId { get; set; }       
    }
}
