using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PositionsWindow.xaml
    /// </summary>
    public partial class PositionsWindow : INotifyPropertyChanged
    {        
        private SkudContext _ctx;

        public PositionsWindow() : base()
        {
            InitializeComponent();
            DataContext = this; 
            _ctx = new SkudContext();            
            Update();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _ctx.SaveChanges();
            Update();
        }

        private void MnuRemoveSelected_OnClick(object sender, RoutedEventArgs e)
        {
          
        }

        private void Update()
        {
            EditHelpers.DetachAllEntities<Position>(_ctx);
            _ctx.Positions.Load();
            grid.ItemsSource = _ctx.Positions.Local;
        }
    }
}
