using skud.Models;
using skud.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace skud
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Direction? Direction { get; set; }
        public AccessStatus? AccessStatus { get; set; }
        public User User { get; set; }
        public ObservableCollection<LogItemViewModel> EventLog { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            User = new User()
            {
                Name = "Иван",
                Surname = "Иванов",
                Position = new Position() { Name = "Должность" },
                Rank = new Rank() { Name = "Сержант" },
                Department = new Department() { Name = "Отдел по борье с отделами" },
                Photo = @"F:\Markov\Projects\skud\skud\Images\NewFile2.bmp"
            };

            EventLog = new ObservableCollection<LogItemViewModel>()
            {
                new LogItemViewModel()
                {
                    Date = DateTime.Now,
                    CardId = 1,
                    Fio="Петров Петр Петрович",
                    Direction=Models.Direction.IN,
                    Status=Models.AccessStatus.DENIED
                },
                new LogItemViewModel()
                {
                    Date = DateTime.Now,
                    CardId = 2,
                    Fio="Иванов Иванович",
                    Direction=Models.Direction.OUT,
                    Status=Models.AccessStatus.GRANTED
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    
}
