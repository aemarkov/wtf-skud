using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //public int Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор карты
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Uid { get; set; }

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
