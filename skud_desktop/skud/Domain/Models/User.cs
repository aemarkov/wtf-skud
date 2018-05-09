using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skud.Domain.Models
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Middlename { get; set; }

        /// <summary>
        /// Имя файла фотографии
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public Position Position { get; set; }
        public int PositionId { get; set; }     
        
        /// <summary>
        /// Звание
        /// </summary>
        public Rank Rank { get; set; }
        public int RankId { get; set; }

        /// <summary>
        /// Список пропусков, выданных пользвователю
        /// </summary>
        public ICollection<Card> Cards
        {
            get { return _cards ?? (_cards = new List<Card>()); }
            set { _cards = value; }
        }
        private ICollection<Card> _cards;
       
        /// <summary>
        /// Отдел
        /// </summary>
        public Department Department { get; set; }
        public int DepartmentId { get; set; }           

        public string FIO { get { return String.Format("{0} {1} {2}", Surname, Name, Middlename); } }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}
