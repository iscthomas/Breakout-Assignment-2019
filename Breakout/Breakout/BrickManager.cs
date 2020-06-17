//The BrickManager class is used to create a list of bricks that is sent to the brick class to be drawn in the game window.
//The score and brick hit conditions are also determined here as well as the game win condition.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Breakout
{
    public class BrickManager
    {
        //constants                     //the size of the bricks must be changed from the eSize class enumeration.
        private const int NCOLUMNS = 5; //amount of columns across the width game space can be changed here.
        private const int NROWS = 7;    //amount of rows from the top down can be changed here.
        private const int NPOINT = 10;  //how many points are awarded per brick broken

        //fields
        private List<Brick> bricks;         //list that stores the brick array
        private Graphics bufferGraphics;
        private int totalScore;
        private int bricksLeft;

        //constructor
        public BrickManager(Graphics bufferGraphics) //initializes the fields required for the brick layout and passes through bufferGraphics so the bricks can be drawn to the form
        {
            bricks = new List<Brick>();
            this.bufferGraphics = bufferGraphics;
        }

        public void CreateBricks() //Creates the layout of the bricks on the playing field using a List to store the position of each brick on the form
        {
            for (int row = 0; row < NROWS; row++)   
            {
                for (int col = 0; col < NCOLUMNS; col++) //for every and column, add a brick to the list while calculating its position based on the values in the nested for loops.
                {
                    int xPos = (int)eSize.SIZEW * col;
                    int yPos = (int)eSize.SIZEH * row;
                    bricks.Add(new Brick(bufferGraphics, xPos, yPos)); //xpos and ypos passed from the brick class
                }
            }
        }

        public void DrawBricks() //Draws a brick to the screen after checking that its hit property is set to false, if the ball has not "broken" the brick yet.
        {
            foreach (Brick brick in bricks)
            {
                if (brick.Hit == false)
                    brick.Draw();
            }
        }
        public void Score() //Calculates the total score of the current game by adding 10 points to the score per brick in the List that has been “broken”.
        {
            totalScore = 0; //set score to 0 before each calculation so it doesnt incorrectly add on to the score

            foreach (Brick brick in bricks)
            {
                if (brick.Hit == true)
                {
                    totalScore += NPOINT; //calculate total score to be awarded to the player
                }
            }
        }
        public bool CheckWin() //Checks for win condition by calculating the total amount of bricks and returning a true value when all bricks have been cleared from the screen.
        {
            System.Media.SoundPlayer gameWin = new System.Media.SoundPlayer(Properties.Resources.win1);      //code adapted from: https://stackoverflow.com/a/4125755
            gameWin.Stream.Position = 0;                                                                    //and: https://social.msdn.microsoft.com/Forums/vstudio/en-US/3d40aad9-f441-4c40-9713-a6c9a6ce733d/soundplayer-exception-the-wave-header-is-corrupt

            bricksLeft = NROWS * NCOLUMNS;      //bricks left = rows multiplied by columns

            foreach (Brick brick in bricks)
            {
                if (brick.Hit == true)
                {
                    bricksLeft--;
                    if (bricksLeft <= 0)    //if the amount of bricks left is less than the amount of bricks stored in the list, then the player wins. 
                    {
                        gameWin.Play();
                        return true;
                    }
                }
            }
            return false;
        }
        public List<Brick> Bricks { get => bricks; set => bricks = value; }
        public int TotalScore { get => totalScore; set => totalScore = value; }
    }
}
