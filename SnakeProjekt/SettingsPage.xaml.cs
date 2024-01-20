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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsPage.xaml
    /// </summary>
    /// 

    /*public enum GameMap
    {
        Empty,
        Borders,
        Tunnel,
        Star
    }; */

    public partial class SettingsPage : Page
    {

        public SettingsPage()
        {
            InitializeComponent();


            //add radiobuttons for maps types
            string radiobuttonGroupName = "radioMapGroup";

            foreach (GameMap enumValue in Enum.GetValues(typeof(GameMap))){
                RadioButton radioButton = new RadioButton {
                    Content = enumValue.ToString(),
                    GroupName = radiobuttonGroupName,
                    Tag = enumValue
                };

                if (enumValue == GameProperties.gameMapSelected)
                {
                    radioButton.IsChecked = true;
                }

                radioButton.Checked += RadioButton_StateChanged;
                //radioButton.Unchecked += RadioButton_StateChanged;

                stackPanelMap.Children.Add(radioButton);

            }


            //set slider
            sliderLevel.Value = (double)GameProperties.level;
            labelLevel.Content = GameProperties.level.ToString();
            sliderLevel.ValueChanged += sliderLevel_ValueChanged;

        }

        private void RadioButton_StateChanged(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            GameProperties.gameMapSelected = (GameMap)radioButton.Tag;
        }

        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PageMenu.xaml", UriKind.Relative));
        }

        private void sliderLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GameProperties.level = (int)sliderLevel.Value;
            labelLevel.Content = GameProperties.level.ToString();
        }
    }

}
