//The Brick class is used to draw the bricks that will be arranged in the game window.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Breakout
{
    public class Brick
    {
        //constants

        //fields
        private Graphics bufferGraphics;
        private Point position;
        private Brush brush;
        private Pen pen;
        private bool hit;

        //constructor
        public Brick(Graphics bufferGraphics, int xPos, int yPos) //initializes the fields required for the brick class and passes variables needed to create the layout of bricks in the BrickManager list.
        {
            brush = new SolidBrush(Color.FromArgb(72, 60, 108));
            pen = new Pen(Color.Cyan, 2);
            this.bufferGraphics = bufferGraphics;

            position.X = xPos; //xpos and ypos is set here to pass into
            position.Y = yPos; //the brickmanager class to build the List of bricks
        }
        public void Draw() //Draws a brick to the screen using bufferGraphics and a solidbrush/pen
        {
            bufferGraphics.FillRectangle(brush, position.X, position.Y, (int)eSize.SIZEW, (int)eSize.SIZEH);
            bufferGraphics.DrawRectangle(pen, position.X, position.Y, (int)eSize.SIZEW, (int)eSize.SIZEH);
        }
        public Point Position { get => position; set => position = value; }
        public bool Hit { get => hit; set => hit = value; }
    }
}
