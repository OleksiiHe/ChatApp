﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp.WPF.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void LoginButton_Click(object sender, RoutedEventArgs e) //TODO: To command. If Name already exist, display error
        //{
        //    OpenScreen(ChatScreen);
        //}

        //private void OpenScreen(Border screen)
        //{
        //    LoginScreen.Visibility = Visibility.Hidden;
        //    ChatScreen.Visibility = Visibility.Hidden;

        //    screen.Visibility = Visibility.Visible;
        //}

        private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
