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
        public User User { get; set; }
        private SkudContext _ctx;

        public UserProfileWindow(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _ctx = new SkudContext();
            User = _ctx.Users.Include(x=>x.Position).Include(x=>x.Department).Include(x=>x.Rank).FirstOrDefault(x => x.Id == userId);
            if (User == null)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
