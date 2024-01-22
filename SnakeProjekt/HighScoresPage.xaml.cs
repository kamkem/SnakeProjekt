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
    /// Logika interakcji dla klasy HighScoresPage.xaml
    /// </summary>
    public partial class HighScoresPage : Page
    {
        public HighScoresPage()
        {
            InitializeComponent();

            //HighScores highScores = new HighScores();
            //string output = highScores.readDatabase();

           // tempTextHighScores.Text = output;

            tempTextHighScores.Text += "\n\n\n";

            //tempTextHighScores.Text += "Borders: " + highScores.getHighscore(GameMap.Borders).ToString() + "\n";
            //tempTextHighScores.Text += "Star: " + highScores.getHighscore(GameMap.Star).ToString() +"\n";
            //tempTextHighScores.Text += "Empty: " + highScores.getHighscore(GameMap.Empty).ToString() + "\n";

            List <int> highScoreList = new List <int>();

            highScoreList = HighScores.getHighScores(GameMap.Borders);


            foreach (int highScore in highScoreList)
            {
                tempTextHighScores.Text += highScore.ToString();
            }


        }
    }
}
