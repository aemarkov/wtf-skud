﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using skud.Data;
using skud.Domain.Hardware;
using skud.Domain.Models;

namespace skud.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : Window, INotifyPropertyChanged
    {
        public ulong? CardUid { get; set; }
        public DateTime ExpirationDate { get; set; }

        private int _userId;
        public Card Card { get; private set; }

        public AddCardWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            DataContext = this;
            ExpirationDate = DateTime.Now.AddMonths(1);
            ArduinoGateway.Instance.CardRead += Instance_CardRead;
        }

        private void Instance_CardRead(ulong uid)
        {
            CardUid = uid;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            Card = new Card()
            {
                UserId = _userId,
                IssueDate = DateTime.Now,
                ExpirationDate = ExpirationDate,
                Uid = (long)CardUid
            };

            using (var ctx = new SkudContext())
            {
                ctx.Cards.Add(Card);
            }

            DialogResult = true;
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}