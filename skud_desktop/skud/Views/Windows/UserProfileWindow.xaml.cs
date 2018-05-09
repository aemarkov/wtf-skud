using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using skud.Data;
using skud.Domain.Models;

namespace skud.Views.Windows
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Пользователь
        public User User { get; private set; }

        // Рабочие смены
        public List<WorkShift> Shifts { get; private set; }

        private SkudContext _ctx;
        private int _userId;

        public UserProfileWindow(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _userId = userId;

            // Загружаем пользователя
            _ctx = new SkudContext();
            User = _ctx.Users.Include(x=>x.Position).Include(x=>x.Department).Include(x=>x.Rank).FirstOrDefault(x => x.Id == userId);
            if (User == null)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }

            // Загружаем рабочие смены пользователя
            Shifts = (from shift in _ctx.WorkShifts
                      join card in _ctx.Cards on shift.CardUid equals card.Uid
                      where card.UserId == userId
                      select shift).ToList();

            // Загружаем карты
            UpdateCards();
        }
       
        // Открываем окно добавления кары
        private void BtnAddCard_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new AddCardWindow(_userId);
            if (dlg.ShowDialog() ?? false)
            {
                // Если добавление прошло успешно, обновляем список карт
                UpdateCards();
            }
        }

        private void UpdateCards()
        {
            _ctx.Cards.Where(x => x.UserId == _userId).ToList();
            cardsGrid.ItemsSource = _ctx.Cards.Local;
        }

        // Удаление выбранных карт
        private void mnuRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            while (cardsGrid.SelectedItems.Count > 0)
            {
                var card = (Card)cardsGrid.SelectedItems[0];
                _ctx.Cards.Remove(card);
            }

            _ctx.SaveChanges();
        }
    }
}
