//The Ball class is used draw the ball and to determine the position and movement of the ball during gameplay, 
//this class will also control the ball bouncing off the bricks, paddle and edges of the screen as well as checking for the game lose condition.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Breakout
{
    public class Ball
    {
        //constants
        private const int BSIZE = 12;   //Size of the ball
        private const int CHANGEDIRECTION = -1; //constant to reverse velocity on collision

        //fields
        private Point position;
        private Point velocity;
        private Graphics bufferGraphics;
        private Brush brush;
        private Pen pen;
        private Size boundaries;

        //constructor
        public Ball(Point position, Point velocity, Color colour, Graphics bufferGraphics, Size boundaries) //intializes the fields required to run the ball class and passes through the fields 
        {                                                                                                   // that are required to draw the ball on the form.
            this.position = position;
            this.velocity = velocity;
            brush = new SolidBrush(colour);
            pen = new Pen(Color.Black, 2);  
            this.bufferGraphics = bufferGraphics;
            this.boundaries = boundaries;
        }
        public void Move() //Controls the balls position and velocity 
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
        }
        public void Draw() //Draws the ball to the screen using bufferGraphics and a solidbrush/pen
        {
            bufferGraphics.FillEllipse(brush, position.X, position.Y, BSIZE, BSIZE);
            bufferGraphics.DrawEllipse(pen, position.X, position.Y, BSIZE, BSIZE);
        }
        public bool CheckLose() //Checks if the game is lost by checking if the ball has touched the bottom of the form
        {
            System.Media.SoundPlayer gameLoses = new System.Media.SoundPlayer(Properties.Resources.lose1);     //code adapted from: https://stackoverflow.com/a/4125755
            gameLoses.Stream.Position = 0;                                                                     //and: https://social.msdn.microsoft.com/Forums/vstudio/en-US/3d40aad9-f441-4c40-9713-a6c9a6ce733d/soundplayer-exception-the-wave-header-is-corrupt

            if (position.Y + BSIZE >= boundaries.Height)
            {
                gameLoses.Play();
                return true;
            }
            return false;
        }
        public void BounceEdges() //Ensures the ball bounces off the top and sides of the screen by checking the balls position with the height and width (boundaries) of the form
        {
            System.Media.SoundPlayer hitEdge = new System.Media.SoundPlayer(Properties.Resources.hit_2);      //code adapted from: https://stackoverflow.com/a/4125755
            hitEdge.Stream.Position = 0;                                                                      //and: https://social.msdn.microsoft.com/Forums/vstudio/en-US/3d40aad9-f441-4c40-9713-a6c9a6ce733d/soundplayer-exception-the-wave-header-is-corrupt

            if ((position.X < 0) || (position.X + BSIZE > boundaries.Width)) //bounce off of the left/right sides of the form
            {
                velocity.X *= CHANGEDIRECTION;
                hitEdge.Play();
            }
            if ((position.Y < 0) || (position.Y + BSIZE > boundaries.Height)) //bounce off of the top/bottom of the form
            {
                velocity.Y *= CHANGEDIRECTION;
                hitEdge.Play();
            }
        }
        public void BouncePaddle(Paddle paddle) //Controls when the ball should bounce off the paddle by checking the balls position relative to the top edge of the paddle
        {
            System.Media.SoundPlayer hitPaddle = new System.Media.SoundPlayer(Properties.Resources.hit_1);      //code adapted from: https://stackoverflow.com/a/4125755
            hitPaddle.Stream.Position = 0;                                                                      //and: https://social.msdn.microsoft.com/Forums/vstudio/en-US/3d40aad9-f441-4c40-9713-a6c9a6ce733d/soundplayer-exception-the-wave-header-is-corrupt

            if ((position.Y + BSIZE >= paddle.Position.Y) && (position.Y + BSIZE <= paddle.Position.Y + (int)eSize.SIZEH)) //bounces off of the top of the paddle
            {
                if ((position.X >= paddle.Position.X) && (position.X <= paddle.Position.X + (int)eSize.PSIZEW))
                {
                    velocity.Y *= CHANGEDIRECTION;
                    hitPaddle.Play();
                }
            }
        }
        public void BounceBrick(List<Brick> bricks) //Controls the ball bouncing off the bricks by checking the balls position relative to the edges of each brick
        {
            System.Media.SoundPlayer hitBrick = new System.Media.SoundPlayer(Properties.Resources.hit_0);      //code adapted from: https://stackoverflow.com/a/4125755
            hitBrick.Stream.Position = 0;                                                                      //and: https://social.msdn.microsoft.com/Forums/vstudio/en-US/3d40aad9-f441-4c40-9713-a6c9a6ce733d/soundplayer-exception-the-wave-header-is-corrupt

            foreach (Brick brick in bricks)
            {
                if (((position.Y <= brick.Position.Y + (int)eSize.SIZEH) && (position.Y + BSIZE >= brick.Position.Y) && (brick.Hit == false))) //bounce off of the top and bottom of the bricks
                {
                    if ((position.X >= brick.Position.X) && (position.X <= brick.Position.X + (int)eSize.SIZEW))
                    {
                        velocity.Y *= CHANGEDIRECTION;
                        hitBrick.Play();
                        brick.Hit = true;
                    }
                }
            }
        }
    }
}
