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

            List <int> highScoreList = new List <int>();

            highScoreList = HighScores.getHighScores(GameMap.Borders);
            CreateColumns();


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

                stackPanel.Children.Add(title);

                DataGrid dataGrid = new DataGrid
                {
                    Margin = new Thickness(10, 10, 10, 10),//(10, 40, 10, 10)
                    Width = columnWidth - 20,
                    Height = Height - 60,

                    BorderThickness = new Thickness(0),
                    BorderBrush = new SolidColorBrush(Colors.Transparent),
                    ItemsSource = (System.Collections.IEnumerable)HighScores.generateHighScoreTable((GameMap)mapsEnum.GetValue(i)).DefaultView,
            };

                stackPanel.Children.Add(dataGrid);

                Grid.SetRow(stackPanel, 0);
                Grid.SetColumn(stackPanel, i);
                grid.Children.Add(stackPanel);

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

