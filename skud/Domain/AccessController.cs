using skud.Data;
using skud.Domain.Models;
using skud.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
        public Direction? Direction { get; set; }
        public AccessStatus? Access { get; set; }

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

            if (User != null)
            {
                if (direction == Domain.Direction.IN)
                {
                    var ws = new WorkShift()
                    {
                        ArrivalTime = DateTime.Now,
                        CardId = (long)uid
                    };
                    _ctx.WorkShifts.Add(ws);
                    _ctx.SaveChanges();
                }
                else
                {
                    long id = (long) uid;
                    var last = _ctx.WorkShifts.Where(x => x.CardId == id).OrderByDescending(x => x.Id)
                        .FirstOrDefault();
                    if (last != null)
                    {
                        last.LeavingTime = DateTime.Now;
                        _ctx.SaveChanges();
                    }
                }

                Access = AccessStatus.GRANTED;
                Direction = direction;
            }
            else
            {
                Access = AccessStatus.DENIED;
                Direction = null;
            }

            EventLog.Add(new LogItemViewModel()
            {
                Date = DateTime.Now,
                CardId = uid,
                Fio = User?.FIO,
                Direction = direction,
                Status = Access.Value
            });
            return (Access == AccessStatus.GRANTED);
        }

        private User GetUser(ulong uid)
        {
            long id = (long) uid; //EF не поддерживает unsigned
            return _ctx.Cards
                .Where(x => x.Uid == id && x.IssueDate <= DateTime.Now && x.ExpirationDate >= DateTime.Now)
                .Select(x => x.User)
                .Include(x=>x.Department)
                .Include(x => x.Position)
                .Include(x => x.Rank)
                .FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
