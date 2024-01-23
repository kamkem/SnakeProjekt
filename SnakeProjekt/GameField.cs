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
using static System.Net.Mime.MediaTypeNames;
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


    class GameField
    {
        public FieldState state { get; set; }
        public CollisionType collision_type { get; set; }
        public String image;

        public GameField(FieldState state)
        {
            this.state = state;

            switch (state)
            {
                case FieldState.wall:
                    collision_type = CollisionType.collision;
                    image = "crate1_diffuse.png";
                    break;
                case FieldState.basic_food:
                    collision_type = CollisionType.food;
                    image = "red-apple.png";
                    break;
                case FieldState.special_food:
                    collision_type = CollisionType.food;
                    image = "cake.png";
                    break;
                case FieldState.body:
                    collision_type = CollisionType.collision;
                    image = null;
                    break;
            }
        }

        public GameField()
        {
            this.state = FieldState.empty;
            collision_type = CollisionType.none;
            image = null;
        }

    }

    enum BodyOrientation
    {
        none,
        up,
        down,
        left,
        right,
        bottomleft,
        bottomright,
        topleft,
        topright,
        vertical,
        horizontal,
    }

    enum BodyType
    {
        head,
        body,
        tail
    }
    class BodyField : GameField
    {
        public BodyField(BodyType bodyType, BodyOrientation bodyOrientation) : base(FieldState.body)
        {

            image = bodyType.ToString() + "_" + bodyOrientation.ToString() + ".png";

        }
    }
}