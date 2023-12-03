using System;
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

namespace SnakeProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            temp_GameField[,] gameFields = new temp_GameField[10, 10];

            for (int column = 0; column < gameFields.GetLength(0); column++)
            {
                for (int row = 0; row < gameFields.GetLength(1); row++)
                {
                    gameFields[row, column] = new temp_GameField(0);
                }
            }

            gameFields[2, 3].state = 1;
            gameFields[2, 4].state = 1;
            gameFields[2, 5].state = 1;



            System.Windows.Shapes.Rectangle rect;


            for (int column = 0; column < gameFields.GetLength(0); column++)
            {
                for(int row = 0; row < gameFields.GetLength(1); row++)
                {
                    

                    rect = new System.Windows.Shapes.Rectangle();

                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    rect.Fill = new SolidColorBrush(Colors.GreenYellow);

                    if (gameFields[row, column].state == 1)
                    {
                        rect.Fill = new SolidColorBrush(Colors.Blue);
                    }
                    

                    rect.Width = 50;
                    rect.Height = 50;
                    Canvas.SetLeft(rect, row * 50); ;
                    Canvas.SetTop(rect, column*50);
                    GameCanvas.Children.Add(rect);
                    
                    


                    
                    //Canvas.SetTop(gameFields[row, column], column*50);
                    //Canvas.SetLeft(gameFields[row, column], row*50);

                    //GameCanvas.Children.Add(gameFields[row, column]);

                    // gameFields[row, column].Margin = new Thickness(row*50, column*50, 0, 0);
                }
            }



        }
    }
}