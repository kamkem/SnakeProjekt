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

    enum CollisionType
    {
        none,
        food,
        collision
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


    /// <summary>
    /// ///////////////////////////////////CHANGE TO ONE CLASS - WITH STRUCT?????
    /// </summary>
    class temp_GameField
    {



        public FieldState state { get; set; }
        public CollisionType collision_type { get; set; }

         public String image;


        public temp_GameField()
        {
           state = FieldState.empty;

           collision_type = CollisionType.none;
           image = null;

        }
    }

    class BodyField : temp_GameField
    {

        string direction;

        public BodyField(bool head, string direction)
        {
            state = FieldState.body;
            collision_type = CollisionType.collision;

            if (head)
            {
                image = "head_ph.png";
            }
            else
            {
                image = "body_ph.png";
            }
            
        }

    }

    class BasicFoodField : temp_GameField
    {

        public BasicFoodField()
        {
            collision_type = CollisionType.food;
            state = FieldState.basic_food;
            image = "red-apple.png";

        }

    }

    class SpecialFoodField : temp_GameField {
        // List<string> specialFood = new List<string> { "cake.png", "jello.png" };


        //just one constructor:
        public SpecialFoodField()
        {
            collision_type = CollisionType.food;
            state = FieldState.special_food;
            image = "cake.png";
        }

    }


    class WallField : temp_GameField
    {

        public WallField()
        {
            collision_type = CollisionType.collision;
            state = FieldState.wall;
            image = "crate1_diffuse.png";
        }
    }
}