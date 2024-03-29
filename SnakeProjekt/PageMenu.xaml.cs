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

namespace SnakeProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
        }

        private void buttonGame_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PageGame.xaml", UriKind.Relative));


        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SettingsPage.xaml", UriKind.Relative));
        }

        private void buttonHighScores_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HighScoresPage.xaml", UriKind.Relative));
        }
    }
}
