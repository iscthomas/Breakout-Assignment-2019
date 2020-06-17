//The controller class is used to start, end and control the game, and pass through win/loss conditions to the form.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Breakout
{
    public class Controller
    {
        //constants
        private const int STARTPOS = 400; //starting position (x/y axis) of the ball.
        private const int BALLSPEED = 5;  //how fast the ball moves across the game form

        //fields
        private Ball ball;
        private Paddle paddle;
        private BrickManager brickManager;
        private int totalScore;

        //constructor
        public Controller(Graphics bufferGraphics, Size boundaries) //The constructor initializes the fields required to run the game as well as passing fields that are needed to run the game
        {
            paddle = new Paddle(bufferGraphics, boundaries);
            brickManager = new BrickManager(bufferGraphics);
            brickManager.CreateBricks();
            ball = new Ball(new Point(STARTPOS, STARTPOS), new Point(BALLSPEED, BALLSPEED), Color.Cyan, bufferGraphics, boundaries); //creates a ball for the game.
        }
        public void Restart(Graphics bufferGraphics, Size boundaries) //re-initializes all of the fields to restart the game
        {
            paddle = new Paddle(bufferGraphics, boundaries);
            brickManager = new BrickManager(bufferGraphics);
            brickManager.CreateBricks();
            ball = new Ball(new Point(STARTPOS, STARTPOS), new Point(BALLSPEED, BALLSPEED), Color.Cyan, bufferGraphics, boundaries); //creates a ball for the game.
        }
        public void Run() //Runs the game, initialises all required components of the playfield
        {
            ball.Move();
            ball.Draw();
            ball.BounceEdges();
            ball.BouncePaddle(paddle);
            ball.BounceBrick(brickManager.Bricks);
            ball.CheckLose();

            paddle.Draw();
            brickManager.DrawBricks();
            brickManager.Score();
            totalScore = brickManager.TotalScore;
            brickManager.CheckWin();
        }
        public bool GameWin() //Checks for game win condition (cleared all bricks from the playfield without losing.)
        {
            if (brickManager.CheckWin() == true) //passed in from brickmanager class to go to the form
            {
                return true;
            }
            return false;
        }
        public bool GameLose() //Checks for game loss condition (the ball has left the playfield).
        {
            if (ball.CheckLose() == true) //passed in from ball class to go to the form
            {
                return true;
            }
            return false;
        }
        public Paddle Paddle { get => paddle; set => paddle = value; }
        public int TotalScore { get => totalScore; set => totalScore = value; }
    }
}
