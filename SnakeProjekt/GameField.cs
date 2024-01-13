using SnakeProjekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
//using static System.Net.Mime.MediaTypeNames;

namespace SnakeProjekt
{

    enum FieldState
    {
        empty,
        wall,
        body,
        basic_food,
        special_food
    }
    /*
    //-----images filenames:--------
    string normalFood = "red-apple.png";
    List<string> specialFood = new List<string> { "cake.png", "jello.png" };

    static Dictionary<string, string> snakeBody = new Dictionary<string, string>
        {
        {"head", "head_ph.png"},
        {"body", "body_ph.png"},
        {"tail", "tail.png"},
        {"curve", "curve.png" }
        };
    //-----------------------------
    */
    class temp_GameField
    {



        public FieldState state { get; set; }

        //public Image image { get; set; }
        public String image;

        public int score { get; set; }

       // public Color color { get; set; }

        public temp_GameField()
        {
           state = FieldState.empty;
            //image =
            //color = Colors.Transparent;//Colors.GreenYellow;
            score = 0;
            image = null;

        }
    }

    class BodyField : temp_GameField
    {
        public BodyField(bool head, string direction)
        {
            state = FieldState.body;
            score = 0;

            //image = "red-apple.png";

            if (head)
            {
                image = "head_ph.png";
               // color = Colors.Blue;
            }
            else
            {
                //color = Colors.Green;
                image = "body_ph.png";
            }
            
        }


    }

    class BasicFoodField : temp_GameField
    {

        public BasicFoodField()
        {
            state = FieldState.basic_food;
            image = "red-apple.png";
           // color = Colors.IndianRed;
            score = 1;
        }

    }

    class SpecialFoodField : temp_GameField {
        static int timing { get; set; }
       // List<string> specialFood = new List<string> { "cake.png", "jello.png" };


        public SpecialFoodField(int timingVal)
        {
            state = FieldState.special_food;
            image = "cake.png";
            //color = Colors.DarkRed;
            score = 5;
            timing = timingVal;
        }
        public SpecialFoodField()
        {
            state = FieldState.special_food;
            image = "cake.png";
            //color = Colors.DarkRed;
            score = 5;
            countdown();
            
        }

        void countdown()
        {
            if (timing <= 0) { state = FieldState.empty; }
            else
            {
                timing--;
            }
        }

    }

}



    /*

    class GameField : Image
    {

        public int state { get; set; } //0 - empty, 1-body - change to enum or sth


        public GameField(int stateVal)
        {
            state = stateVal;
            changeProperties();
            this.Visibility = System.Windows.Visibility.Visible;

        }


        //add subscriber - state changed!!!

        private void changeProperties()
        {
            //   ResourceManager rm = Resources.ResourceManager;
            if (state == 0)
            {
                this.Source = (ImageSource)Resources["empty.png"];
            }
            else
            {
                this.Source = (ImageSource)Resources["body.png"];
            }
        }
    }
        */