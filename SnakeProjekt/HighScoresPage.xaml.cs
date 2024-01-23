using System;
using System.Collections.Generic;
using System.Data;
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

            //tempTextHighScores.Text += "\n\n\n";

            //tempTextHighScores.Text += "Borders: " + highScores.getHighscore(GameMap.Borders).ToString() + "\n";
            //tempTextHighScores.Text += "Star: " + highScores.getHighscore(GameMap.Star).ToString() +"\n";
            //tempTextHighScores.Text += "Empty: " + highScores.getHighscore(GameMap.Empty).ToString() + "\n";

            List <int> highScoreList = new List <int>();

            highScoreList = HighScores.getHighScores(GameMap.Borders);
            CreateColumns();

            foreach (int highScore in highScoreList)
            {
                //tempTextHighScores.Text += highScore.ToString();
            }


        }

        private void CreateColumns()
        {

            Array mapsEnum = Enum.GetValues(typeof(GameMap));
            int numberOfColumns = mapsEnum.Length;
            // Create a Grid
            Grid grid = new Grid();
            Content = grid;

            // Define column width
            double columnWidth = this.Width / numberOfColumns;

            // Add three columns
            for (int i = 0; i < numberOfColumns; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(column);

                StackPanel stackPanel = new StackPanel
                {
                    Margin = new Thickness(0, 250, 0, 0)
                };

                // Create a title
                TextBlock title = new TextBlock
                {
                    Text = mapsEnum.GetValue(i).ToString(),
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(10),
                };
                //Grid.SetRow(title, 0);
                //Grid.SetColumn(title, i);
                //grid.Children.Add(title);
                stackPanel.Children.Add(title);

                DataGrid dataGrid = new DataGrid
                {
                    Margin = new Thickness(10, 10, 10, 10),//(10, 40, 10, 10)
                    Width = columnWidth - 20,
                    Height = Height - 60,

                    BorderThickness = new Thickness(0),
                    BorderBrush = new SolidColorBrush(Colors.Transparent),
                    ItemsSource = (System.Collections.IEnumerable)HighScores.generateHighScoreTable((GameMap)mapsEnum.GetValue(i), true).DefaultView,
            };

                stackPanel.Children.Add(dataGrid);

                Grid.SetRow(stackPanel, 0);
                Grid.SetColumn(stackPanel, i);
                grid.Children.Add(stackPanel);

                //*/

                if (i == numberOfColumns-1)
                {
                    Button exitButton = new Button
                    {
                        Content = "Back",
                    };

                    exitButton.Click += (sender, e) => { this.NavigationService.Navigate(new Uri("PageMenu.xaml", UriKind.Relative)); };
                    stackPanel.Children.Add(exitButton);

                }
            }




        }

        }
        
    
}


/*
         <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>

        <Border Grid.Column="0" BorderBrush="Transparent" BorderThickness="0"  Margin="5, 150" Background="Transparent">
            <ListView Name="listView0">
                <ListView.View>
                    <GridView>

                    </GridView>
                </ListView.View>
            </ListView>

        </Border>

        <Border Grid.Column="1" BorderBrush="Transparent" BorderThickness="0"  Margin="5, 150" Background="Transparent">
            <ListView Name="listView1">
            <ListView.View>
                <GridView>

                </GridView>
            </ListView.View>
        </ListView>
        </Border>

        <Border Grid.Column="2" BorderBrush="Transparent" BorderThickness="0"  Margin="5, 150" Background="Transparent">
            <ListView Name="listView2">
                <ListView.View>
                    <GridView>

                    </GridView>
                </ListView.View>
            </ListView>
        </Border>

        <Border Grid.Column="3" BorderBrush="Transparent" BorderThickness="0"  Margin="5, 150" Background="Transparent">
            <ListView Name="listView3">
                <ListView.View>
                    <GridView>

                    </GridView>
                </ListView.View>
            </ListView>
        </Border>


 
 */