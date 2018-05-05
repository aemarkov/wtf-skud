using System;
using System.Collections.Generic;
using System.Linq;
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
using skud.Domain;

namespace skud.Views.Windows
{
    /// <summary>
    /// Interaction logic for FakeHardwareWindow.xaml
    /// </summary>
    public partial class FakeHardwareWindow : Window
    {
        private AccessRequestDelegate _handler;

        public FakeHardwareWindow(AccessRequestDelegate handler)
        {
            InitializeComponent();
            _handler = handler;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Direction direction = (rbtnIn.IsChecked ?? false) ? Direction.IN : Direction.OUT;
            ulong uid = ulong.Parse(txtUid.Text);
            bool isAccess = _handler(uid, direction);
            if (isAccess)
                lblAccess.Text = "разрешен";
            else
                lblAccess.Text = "запрещен";
        }
    }
}
