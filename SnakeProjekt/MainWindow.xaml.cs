using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Net.Http.Headers;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using static System.Net.Mime.MediaTypeNames;

namespace SnakeProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public enum GameMap
    {
        Empty,
        Borders,
        Tunnel,
        Star
    };

    class GameProperties
    {
        public static int level = 5;
        public static GameMap gameMapSelected;
    }

    public partial class MainWindow : Window
    {

        //public int level;
        //public static GameMap gameMapSelected;

        public MainWindow()
        {
            InitializeComponent();

            _mainFrame.Navigate(new PageMenu());

            GameProperties.level = 5;
            GameProperties.gameMapSelected = GameMap.Empty;

        }

    }
}