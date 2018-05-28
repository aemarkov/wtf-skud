using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using skud.Data;
using skud.Domain.Models;
using skud.Helpers;
using MessageBox = System.Windows.MessageBox;
using System.IO;

namespace skud.Views.Windows
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window, INotifyPropertyChanged
    {
        private SkudContext _ctx;

        public List<Department> Departments { get; set; }
        public List<Position> Positions { get; set; }
        public List<Rank> Ranks { get; set; }

        public UsersWindow()
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
            try
            {
                _ctx.SaveChanges();
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные заполнены неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MnuRemoveSelected_OnClick(object sender, RoutedEventArgs e)
        {
            while (grid.SelectedItems.Count > 0)
            {
                var item = (User)grid.SelectedItems[0];
                _ctx.Users.Remove(item);
            }
        }

        private void MnuShowProfile_OnClick(object sender, RoutedEventArgs e)
        {
            var user = (User)grid.SelectedItem;
            new UserProfileWindow(user.Id).Show();
        }

        private void Update()
        {
            EditHelpers.DetachAllEntities<User>(_ctx);
            Departments = _ctx.Departments.ToList();
            Positions = _ctx.Positions.ToList();
            Ranks = _ctx.Ranks.ToList();
            _ctx.Users.Include(x => x.Position).Include(x => x.Department).Include(x => x.Rank).Load();
            grid.ItemsSource = _ctx.Users.Local;
        }

        private void MnuBrowse_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(!Directory.Exists("Photos"))
                    Directory.CreateDirectory("Photos");
                

                string extension = Path.GetExtension(dlg.FileName);
                string saveFile = Path.Combine("Photos", Guid.NewGuid().ToString() + extension);
                File.Copy(dlg.FileName, saveFile);

                var item = (User)grid.SelectedItem;
                item.Photo = saveFile;
                grid.CommitEdit();
            }
            else
            {
                grid.CancelEdit();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
