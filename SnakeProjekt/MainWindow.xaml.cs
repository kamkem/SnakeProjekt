using System;
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

namespace SnakeProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        //declare 2d array with fields
        static int board_x = 20;
        static int board_y = 12;

        temp_GameField[,] gameFields = new temp_GameField[board_x, board_y];
        List<int[]> bodyFields = new List<int[]>();

        //movement direction
        int[] movementDirection = new int[] { 1, 0 };

        int totalScore = 0;

        public MainWindow()
        {
            InitializeComponent();

            //timer
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick; //event!!
            dispatcherTimer.Interval = new TimeSpan(20, 0, 0);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Start();

            //initialize fields
            for (int column = 0; column < gameFields.GetLength(1); column++)
            {
                for (int row = 0; row < gameFields.GetLength(0); row++)
                {
                    gameFields[row, column] = new temp_GameField();
                }
            }

            //initial snake body
            int[] body = new int[] { 2, 5 };
            bodyFields.Add(body);
            body = new int[] { 2, 4 };
            bodyFields.Add(body);
            body = new int[] { 2, 3 };
            bodyFields.Add(body);
            body = new int[] { 2, 2 };
            bodyFields.Add(body);
            body = new int[] { 2, 1 };
            bodyFields.Add(body);


            DrawSnake();


            gameFields[3, 8] = new SpecialFoodField(15);
            gameFields[2, 10] = new BasicFoodField();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            //Random rnd = new Random();
            //gameFields[rnd.Next(10), rnd.Next(1)].state = FieldState.basic_food;
            MoveSnake();
            checkFieldsState();
            RedrawGrid();
        }

        private void generateSpecialFood()
        {

        }

        private void MoveSnake()
        {

            //if (bodyFields[0][0] == 8) { dispatcherTimer.Stop(); }


            int[] head = bodyFields[0];
            int[] new_head = { head[0] + movementDirection[0], head[1] + movementDirection[1] };

            //move outside the board
            
            if (new_head[0] < 0) { new_head[0] = board_x-1; }
            else if (new_head[0] == board_x) { new_head[0] = 0; }
            else if (new_head[1] < 0) { new_head[1] = board_y-1; }
            else if (new_head[1] == board_y) { new_head[1] = 0; }


            if (gameFields[new_head[0], new_head[1]].collision_type == CollisionType.collision)
            {
                gameOver();
                return;
            }

            //check if food is next?

            if(gameFields[new_head[0], new_head[1]].collision_type == CollisionType.food)
            {
                //add score
                if (gameFields[new_head[0], new_head[1]].state == FieldState.basic_food) { totalScore += 1; }
                else if (gameFields[new_head[0], new_head[1]].state == FieldState.special_food) { totalScore += 5; }
            }
            else
            {
                //remove last bodyfield to move the snake
                int[] lastElement = bodyFields[bodyFields.Count - 1];
                gameFields[lastElement[0], lastElement[1]] = new temp_GameField();
                bodyFields.RemoveAt(bodyFields.Count - 1);
            }


            //old head becomes "normal" body
            gameFields[head[0], head[1]] = new BodyField(false, "placeholder");

            bodyFields.Insert(0, new_head);
            gameFields[new_head[0], new_head[1]] = new BodyField(true, "placeholder");

        }

        private void DrawSnake()
        {
            foreach (int[] field in bodyFields)
            {
                int raw = field[0];
                int column = field[1];
                gameFields[raw, column].state = FieldState.body;
            }
        }


        private void checkFieldsState()
        {
            for (int column = 0; column < gameFields.GetLength(1); column++)
            {
                for (int row = 0; row < gameFields.GetLength(0); row++)
                {
                    if (gameFields[row, column].state == FieldState.basic_food) { gameFields[row, column] = new BasicFoodField(); }
                    if (gameFields[row, column].state == FieldState.special_food) { gameFields[row, column] = new SpecialFoodField(); }
                    if (gameFields[row, column].state == FieldState.empty) { gameFields[row, column] = new temp_GameField(); }
                    //if (gameFields[row, column].state == FieldState.body) { gameFields[row, column] = new BodyField(); }
                    //DrawSnake();
                }
            }
        }

        private void gameOver()
        {
            dispatcherTimer.Stop();
        }

        public void RedrawGrid()
        {
            GameCanvas.Children.Clear();
            System.Windows.Shapes.Rectangle rect;
            for (int column = 0; column < gameFields.GetLength(1); column++)
            {
                for (int row = 0; row < gameFields.GetLength(0); row++)
                {

                    if (gameFields[row,column].state == FieldState.empty) { continue; } //skip empty fields

                    if (gameFields[row,column].image != null)
                    {
                        BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/SnakeProjekt;component/Resources/" + gameFields[row,column].image));

                        Image imageControl = new Image
                        {
                            Source = bitmapImage,
                            Width = 50,
                            Height = 50,
                            Margin = new Thickness(row * 50, column * 50, 0, 0)
                        };
                    
                    
                        GameCanvas.Children.Add(imageControl);
                    }
                    /*
                    rect = new System.Windows.Shapes.Rectangle();

                    rect.Stroke = new SolidColorBrush(Colors.Black);

                    rect.Fill = new SolidColorBrush(gameFields[row, column].color);

                    rect.Width = 50;
                    rect.Height = 50;
                    Canvas.SetLeft(rect, row * 50); ;
                    Canvas.SetTop(rect, column * 50);
                    GameCanvas.Children.Add(rect);
                    */
                }
            }
        }

        public void NewDrawGrid()
        {

        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            int[] newMovement = new int[2];
            if (e.Key == Key.Down) {newMovement[0] = 0; newMovement[1] = 1;}
            else if (e.Key == Key.Up) { newMovement[0] = 0; newMovement[1] = -1; }
            else if (e.Key == Key.Left) { newMovement[0] = -1; newMovement[1] = 0; }
            else if (e.Key == Key.Right) { newMovement[0] = 1; newMovement[1] = 0; }
            
            if (newMovement[0] * movementDirection[0] + newMovement[1] * movementDirection[1] != -1) 
            {
                movementDirection = newMovement;
            }
        }
    }
}