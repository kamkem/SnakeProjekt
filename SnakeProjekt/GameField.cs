using SnakeProjekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
//using static System.Net.Mime.MediaTypeNames;

namespace SnakeProjekt
{
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



    class temp_GameField
    {


        public int state { get; set; }

        public temp_GameField(int stateVal)
        {
            state = stateVal;

        }
    }
}
