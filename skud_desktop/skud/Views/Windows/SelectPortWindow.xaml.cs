using System.Windows;
using skud.Domain;

namespace skud.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectPortWindow.xaml
    /// </summary>
    public partial class SelectPortWindow : Window
    {
        public string SelectedPort { get; set; }
        public string[] Ports { get; set; }

        public SelectPortWindow()
        {
            InitializeComponent();
            DataContext = this;
            Ports = ArduinoGateway.GetPorts();
        }

      
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
