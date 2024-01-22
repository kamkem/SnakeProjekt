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
    /// Logika interakcji dla klasy PageGame.xaml
    /// </summary>
    public partial class PageGame : Page
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        //declare 2d array with fields
        static int board_x = 20;
        static int board_y = 12;

        GameField[,] gameFields = new GameField[board_x, board_y];
        List<int[]> bodyFields = new List<int[]>();

        //movement direction
        int[] movementDirection = new int[] { 1, 0 };

        int totalScore = 0;
        int? specialFoodTimer;
        bool isBasicFood = true;
        bool isSpecialFood = false;

        Random rnd = new Random();

        public PageGame()
        {
            InitializeComponent();

            GameCanvas.Focus();

            int timerTick = 800 / GameProperties.level;
            //timer
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(20, 0, 0);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(timerTick);


            //initialize fields
            for (int column = 0; column < gameFields.GetLength(1); column++)
            {
                for (int row = 0; row < gameFields.GetLength(0); row++)
                {
                    gameFields[row, column] = new GameField();
                }
            }

            //TO CHECK!
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


            // DrawSnake();

            //add high and low score
            List<int> highScoresList = HighScores.getHighScores(GameProperties.gameMapSelected);
            label_high_score.Content = highScoresList[0];

            setMap();

            RedrawGrid();
            RedrawGrid();
            RedrawGrid();

            gameFields[2, 10] = new GameField(FieldState.basic_food);

            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            checkFieldsState();
            RedrawGrid();
        }


        private void MoveSnake()
        {

            int[] head = bodyFields[0];
            int[] new_head = { head[0] + movementDirection[0], head[1] + movementDirection[1] };

            //move outside the board

            if (new_head[0] < 0) { new_head[0] = board_x - 1; }
            else if (new_head[0] == board_x) { new_head[0] = 0; }
            else if (new_head[1] < 0) { new_head[1] = board_y - 1; }
            else if (new_head[1] == board_y) { new_head[1] = 0; }


            if (gameFields[new_head[0], new_head[1]].collision_type == CollisionType.collision)
            {
                gameOver();
                return;
            }

            //check if food is next?

            if (gameFields[new_head[0], new_head[1]].collision_type == CollisionType.food)
            {
                //add score and set flags
                if (gameFields[new_head[0], new_head[1]].state == FieldState.basic_food) { totalScore += 1; isBasicFood = false; }
                else if (gameFields[new_head[0], new_head[1]].state == FieldState.special_food) { totalScore += 5; isSpecialFood = false; label_specialFoodTimer.Content = ""; }
                label_score.Content = totalScore.ToString();

            }
            else
            {
                //remove last bodyfield to move the snake
                int[] lastElement = bodyFields[bodyFields.Count - 1];
                gameFields[lastElement[0], lastElement[1]] = new GameField();
                bodyFields.RemoveAt(bodyFields.Count - 1);
            }


            //old head becomes "normal" body
            gameFields[head[0], head[1]] = new BodyField(false, "placeholder");

            //add new head
            bodyFields.Insert(0, new_head);
            gameFields[new_head[0], new_head[1]] = new BodyField(true, "placeholder");

        }

        private void checkFieldsState()
        {
            for (int column = 0; column < gameFields.GetLength(1); column++)
            {
                for (int row = 0; row < gameFields.GetLength(0); row++)
                {
                    //change to swithc case
                    FieldState state = gameFields[row, column].state;

                    switch (state)
                    {
                        case FieldState.empty:
                            gameFields[row, column] = new GameField();
                            break;
                        case FieldState.wall:
                            gameFields[row, column] = new GameField(FieldState.wall);
                            break;
                        case FieldState.basic_food:
                            gameFields[row, column] = new GameField(FieldState.basic_food);
                            break;
                        case FieldState.special_food:
                            if (specialFoodTimer > 0)
                            {
                                gameFields[row, column] = new GameField(FieldState.special_food);
                                specialFoodTimer--;
                            }
                            else
                            {
                                gameFields[row, column] = new GameField();
                                isSpecialFood = false;
                                specialFoodTimer = null;
                            }
                            label_specialFoodTimer.Content = specialFoodTimer.ToString();
                            break;
                    }

                }
            }

            //change to delegates!
            if (!isBasicFood)
            {
                addFood(FieldState.basic_food);
                isBasicFood = true;
            }
            if (!isSpecialFood)
            {
                if (rnd.Next(100) < 5) //spawn with probability 5%
                {
                    addFood(FieldState.special_food);
                    isSpecialFood = true;
                    specialFoodTimer = 20;
                }
            }
        }

        private void addFood(FieldState foodType)
        {

            int[] randomField = [rnd.Next(board_x), rnd.Next(board_y)];

            while (gameFields[randomField[0], randomField[1]].state != FieldState.empty)
            {
                randomField = [rnd.Next(board_x), rnd.Next(board_y)];
            }

            gameFields[randomField[0], randomField[1]].state = foodType;

        }

        private void gameOver()
        {
            label_specialFoodTimer.Content = "lskjfsd";
            GameCanvas.Children.Clear();
            GameCanvas.UpdateLayout();

            dispatcherTimer.Stop();
        }

        public void RedrawGrid()
        {
            GameCanvas.Children.Clear();
            for (int column = 0; column < gameFields.GetLength(1); column++)
            {
                for (int row = 0; row < gameFields.GetLength(0); row++)
                {

                    if (gameFields[row, column].state == FieldState.empty) { continue; } //skip empty fields

                    if (gameFields[row, column].image != null)
                    {
                        BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/SnakeProjekt;component/Resources/" + gameFields[row, column].image));

                        Image imageControl = new Image
                        {
                            Source = bitmapImage,
                            Width = 50,
                            Height = 50,
                            Margin = new Thickness(row * 50, column * 50, 0, 0)
                        };

                        GameCanvas.Children.Add(imageControl);
                    }

                }
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            int[] newMovement = new int[2];
            if (e.Key == Key.Down) { newMovement[0] = 0; newMovement[1] = 1; }
            else if (e.Key == Key.Up) { newMovement[0] = 0; newMovement[1] = -1; }
            else if (e.Key == Key.Left) { newMovement[0] = -1; newMovement[1] = 0; }
            else if (e.Key == Key.Right) { newMovement[0] = 1; newMovement[1] = 0; }

            if (newMovement[0] * movementDirection[0] + newMovement[1] * movementDirection[1] != -1)
            {
                movementDirection = newMovement;
            }
        }

        private void setMap()
        {
            switch (GameProperties.gameMapSelected)
            {
                case GameMap.Empty:
                    break;
                case GameMap.Borders:

                    for (int i=0; i<board_x; i++)
                    {
                        gameFields[i, 0] = new GameField(FieldState.wall);
                        gameFields[i, board_y-1] = new GameField(FieldState.wall);
                    }
                    for (int i=1; i<board_y-1; i++)
                    {
                        gameFields[0, i] = new GameField(FieldState.wall);
                        gameFields[board_x-1, i] = new GameField(FieldState.wall);
                    }
                    break;
                case GameMap.Tunnel:
                    break;
                case GameMap.Star:
                    break;
            }
        }
    }
}
