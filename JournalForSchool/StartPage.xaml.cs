using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static JournalForSchool.ApplicationViewModel;

namespace JournalForSchool
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    /// 
   

    public partial class StartPage : Page
    {
        MainWindow mainWindow;

        public List<string> kek_list;
        
        public StartPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new LoginPage(mainWindow));
        }
    }
}
