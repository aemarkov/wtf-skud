using skud.Data;
using skud.Domain;
using skud.Domain.Models;
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
        public AccessController Controller { get; set; }

        private ArduinoGateway _arduino;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Controller = new AccessController(new SkudContext());
            _arduino = new ArduinoGateway(Controller.AccessRequest);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    
}
