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
using skud.Views.Windows;

namespace skud
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public AccessController Controller { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            var dlg = new SelectPortWindow();
            if (dlg.ShowDialog() == true)
            {
                string com = dlg.SelectedPort;
                ArduinoGateway.Init(com);
            }
            else
            {
                MessageBox.Show("COM-порт не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                Application.Current.Shutdown();
                return;
            }

            
            Controller = new AccessController(new SkudContext());
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void mnuPositions_Click(object sender, RoutedEventArgs e)
        {
            new PositionsWindow().Show();
        }

        private void mnuRanks_Click(object sender, RoutedEventArgs e)
        {
            new RanksWindow().Show();
        }

        private void mnuUsers_Click(object sender, RoutedEventArgs e)
        {
            new UsersWindow().Show();
        }

        private void mnuHistory_Click(object sender, RoutedEventArgs e)
        {
            new HistoryWindow().Show();
        }

        private void MnuDepartments_OnClick(object sender, RoutedEventArgs e)
        {
            new DepartmentsWindow().Show();
        }
    }


}
