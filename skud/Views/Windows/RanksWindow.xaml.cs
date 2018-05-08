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
using skud.Helpers;

namespace skud.Views.Windows
{
    /// <summary>
    /// Interaction logic for RanksWindow.xaml
    /// </summary>
    public partial class RanksWindow : Window, INotifyPropertyChanged
    {
        private SkudContext _ctx;

        public RanksWindow()
        {
            InitializeComponent();
            DataContext = this;
            _ctx = new SkudContext();
            Update();
        }
       
        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            _ctx.SaveChanges();
            Update();
        }

        private void MnuRemoveSelected_OnClick(object sender, RoutedEventArgs e)
        {
            while (grid.SelectedItems.Count > 0)
            {
                var item = (Rank)grid.SelectedItems[0];
                _ctx.Ranks.Remove(item);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void Update()
        {
            EditHelpers.DetachAllEntities<Rank>(_ctx);
            _ctx.Ranks.Load();            
            grid.ItemsSource = _ctx.Ranks.Local;
        }
    }
}
