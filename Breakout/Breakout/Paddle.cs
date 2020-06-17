//The Paddle class is used to draw the paddle and control the movement of the paddle along the horizontal plane.
//This class also constrains the paddle to the game window. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Breakout
{
    public class Paddle
    {
        //constants
        private const int POSY = 580; //Y-pos of the paddle, does not change during gameplay
        private const int POSX = 500; //starting pos of paddle on the x-axis

        //fields
        private Graphics bufferGraphics;
        private Point position;
        private Size boundaries;
        private Brush brush;
        private Pen pen;

        //constructor
        public Paddle(Graphics bufferGraphics, Size boundaries) //initializes the fields required for the paddle class and passes through the fields that are required to draw the paddle to the form
        {
            this.bufferGraphics = bufferGraphics;
            this.boundaries = boundaries;
            brush = new SolidBrush(Color.FromArgb(255, 33, 124));
            pen = new Pen(Color.Black, 2);
            position.X = POSX;
            position.Y = POSY;
        }
        public void Move(Point mousePos) //Controls the horizontal movement of the paddle and ensures it does not go off the end of the form
        {
            position.X = mousePos.X; 
            if (mousePos.X < 0) //if the left side of the paddle hits the left side of the form, dont allow it to move any further left.
            {
                position.X = 0;
            }
            else if (mousePos.X + (int)eSize.PSIZEW > boundaries.Width) //if the right side of the paddle hits the right side of the form, dont allow it to move any further right.
            {
                position.X = boundaries.Width - (int)eSize.PSIZEW;
            }
        }
        public void Draw() //Draws the paddle to the form using bufferGraphics and a solidbrush/pen
        {
            bufferGraphics.FillRectangle(brush, position.X, position.Y, (int)eSize.PSIZEW, (int)eSize.PSIZEH);
            bufferGraphics.DrawRectangle(pen, position.X, position.Y, (int)eSize.PSIZEW, (int)eSize.PSIZEH);
        }
        public Point Position { get => position; set => position = value; }
    }
}
