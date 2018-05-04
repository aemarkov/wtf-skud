using skud.Data;
using skud.Domain.Models;
using skud.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skud.Domain
{
    /// <summary>
    /// Основная логика - проверка пропусков, определение разрешения или 
    /// запрета прохода, сохранение смен
    /// </summary>
    public class AccessController : INotifyPropertyChanged
    {     
        public ObservableCollection<LogItemViewModel> EventLog { get; set; }
        public User User { get; set; }

        private SkudContext _ctx;

        public AccessController(SkudContext ctx)
        {
            _ctx = ctx;
            EventLog = new ObservableCollection<LogItemViewModel>();
        }

        /// <summary>
        /// Проверка доступа
        /// </summary>
        /// <param name="uid">Идентификатор карты пользователя</param>
        /// <param name="direction">Направление прохода</param>
        /// <returns>Можно пропустить или нет</returns>        
        public bool AccessRequest(ulong uid, Direction direction)
        {
            User = GetUser(uid);
            AccessStatus status;

            if (User != null)
            {
                if (direction == Direction.IN)
                {
                    var ws = new WorkShift()
                    {
                        ArrivalTime = DateTime.Now,
                        CardId = uid
                    };
                    _ctx.WorkShifts.Add(ws);
                    _ctx.SaveChanges();
                }
                else 
                {
                    var last = _ctx.WorkShifts.Where(x => x.CardId == uid).LastOrDefault();
                    if (last != null)
                    {
                        last.LeavingTime = DateTime.Now;
                        _ctx.SaveChanges();
                    }
                }

                status = AccessStatus.GRANTED;
            }
            else
            {
                status = AccessStatus.DENIED;
            }

            EventLog.Add(new LogItemViewModel()
            {
                Date = DateTime.Now,
                CardId = uid,
                Fio = User.FIO,
                Direction = Direction.IN,
                Status = status
            });
            return (status == AccessStatus.GRANTED);
        }

        private User GetUser(ulong uid)
        {
            //_ctx.Cards.FirstOrDefault(x => x.Uid == uid && x.IssueDate <= DateTime.Now && x.ExpirationDate >= DateTime.Now);
            return _ctx.Cards
                .Where(x => x.Uid == uid && x.IssueDate <= DateTime.Now && x.ExpirationDate >= DateTime.Now)
                .Select(x => x.User)
                .FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
