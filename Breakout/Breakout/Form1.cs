// Program Name:        Breakout
// Project File Name:   Breakout
// Author:              Isaac Thomas
// Date:                22/9/2019
// Language:            C#
// Platform:            Microsoft Visual Studio 2017
// Purpose:             To build the retro arcade game, breakout. 
//                      The game will be styled with a nice aesthetic, colours and extra features such as a pause functionality, the ability to restart and a game over screen.
//
// Description:         Breakout is a game that involves a ball that moves around the screen and bounces off the edges of the form,
//                      the paddle, and the bricks at the top of the screen.
//                      The paddle moves on a horizontal plane at the bottom of the screen, the paddle will not move off the edge of 
//                      the screen and is controlled by the players mouse.
//                      The set of bricks is located at the top of the screen, the bricks will disappear when they are hit by the ball.
//                      The player will receive points for every brick that hit by by the ball.
//                      The game is won when the player clears all the bricks from the form, The game is lost if the ball touches the bottom of the form.
//                      The player will be provided with a game over screen and a score breakdown upon winning or losing the game.
//
// Known Bugs:          The Bricks and paddle do not have collision on the left and right sides, so the ball will pass through and hit the top or bottom edge.
//                      The Paddle may not move all the way to the edge of the form if the player moves their cursor too quickly outside the bounds of the form
//                      After Application load, there may be a slight freeze when playing the first sound, however, subsequent uses of sound have no delay.
//
// Additional Features: Menu System, Reactive Sounds, Bonus points and time penalty, gameover screen with score calculation, pause, resume and restart functionality.
//
// Notes:               Code used to make button5 and button6 transparent in Form1.Designer.cs was adapted from: https://stackoverflow.com/a/27628398
//                      Code to play sounds during gameplay was adapted from: https://stackoverflow.com/questions/4125698/how-to-play-wav-audio-file-from-resources/4125755
//                      Background and Menu Image sourced from WallpaperPlay: https://wallpaperplay.com/walls/full/7/7/6/76243.jpg
//                      Sound effects used from free sound library found on freesound.org;
//                      8-Bit Sound Effects Library by LittleRobotSoundFactory: https://freesound.org/people/LittleRobotSoundFactory/packs/16681/?page=1#sound
//
//                      Additional help received from Irving and Isaac during tutorial periods on specific issues with the brick manager and ball collision that I could
//                      not figure out on my own accord, and from Grayson Orr regarding an issue with my menu background images being bugged. 
//                      
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Breakout
{
    public partial class Form1 : Form
    {
        //constants
        private const int BWIN = 1000;  //bonus points awarded on winning the game

        //fields
        private Bitmap backgroundImage;
        private Bitmap bufferImage;
        private Graphics graphics;
        private Graphics bufferGraphics;
        private Controller controller;
        private Stopwatch stopWatch;    //stopwatch is used to time the game

        //constructor
        public Form1() //initializes the form and the fields required to be displayed and passed to the rest of the program so the user can control the game from the form.
        {
            InitializeComponent();

            backgroundImage = new Bitmap(Properties.Resources.Background);  //works in conjunction with bufferGraphics
            bufferImage = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(bufferImage);               //bufferGraphics is used to prevent flickering when rendering graphics to the game form
            graphics = CreateGraphics();
            controller = new Controller(bufferGraphics, ClientSize);        //initialises controller in the form class so that it can be used to control the game 
            stopWatch = new Stopwatch();                                

            timer1.Interval = 10;
            timer1.Enabled = false;

            MenuScreen();   //initializes the menu screen on game start
        }
        private void MenuScreen() //Enables the menu panel and sets it to the menu screen while toggling the required buttons and labels that are relevant to the menu. 
        {
            panel2.BackgroundImage = Properties.Resources.Menu;
            timer1.Enabled = false; //stop timer to stop the game

            button5.Enabled = true; //enable start and exit game buttons
            button5.Visible = true;
            button6.Enabled = true;
            button6.Visible = true;

            panel1.Visible = false; //hide in game panel strip
            panel1.Enabled = false;
            panel2.Visible = true;  //show menu panel
            panel2.Enabled = true;

            label3.Visible = false; //hide game end screen labels 
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
        }
        private void timer1_Tick(object sender, EventArgs e) //The game is controlled by the timer, as it executes all of the functions in this method on every tick.
        {
            bufferGraphics.DrawImage(backgroundImage, 0, 0, Width, Height); //bufferGraphics to prevent flickering
            controller.Run();                                                       //execute the run method in controller, makes the game work
            graphics.DrawImage(bufferImage, 0, 0);                                  //bufferGraphics to prevent flickering
            label2.Text = controller.TotalScore.ToString();                         //show current score on the form
            GameLost();                                                             //check for game lose condition
            GameWon();                                                              //check for game win condition
        }
        private void GameLost() //Enables the menu/gameover panel and sets it to the gameover screen, ends the game timer and outputs the relevant losing text to the end screen, then executes the gameover method.
        {
            if (controller.GameLose() == true)
            {
                panel2.BackgroundImage = Properties.Resources.GameOver;
                panel2.Visible = true;
                panel2.Enabled = true;
                timer1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;

                label3.Text = "You Lose...";
                label9.Text = "Better Luck Next Time...";

                stopWatch.Stop();
                GameOver();
            }
        }
        private void GameWon() //Enables the menu/gameover panel and sets it to the gameover screen, ends the game timer and outputs the relevant winning text to the end screen, then executes the gameover method.
        {
            if (controller.GameWin() == true)
            {
                timer1.Enabled = false;

                panel2.BackgroundImage = Properties.Resources.GameOver;
                panel2.Visible = true;
                panel2.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;

                label3.Text = "You Won!";
                label9.Text = "Nice Work!";
                stopWatch.Stop(); 
                GameOver();
            }
        }
        private void GameOver() //Executes the game over screen calculations and toggles the required buttons and labels that are relevant to the game over screen. 
        {                       //Uses an if-else statement to do the required calculations for the end game score depending on whether the player won or lost.
            TimeSpan ts = stopWatch.Elapsed;    //pass stopwatch value into "ts"
            label3.Visible = true; 
            label4.Visible = true;  //show labels for game end screen
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;

            if (controller.GameWin() == true)
            {
                label4.Text = controller.TotalScore.ToString();                             //display total score from gameplay on gameover screen
                label5.Text = BWIN.ToString();                                              //show bonus points for winning
                string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);//format elapsed time onto screen for player to see how long they took
                label6.Text = elapsedTime;                                                  
                string totalTime = String.Format("{0:0}{1:00}", ts.Minutes, ts.Seconds);    //format string that can be converted to an integer
                int timePenalty = Convert.ToInt32(totalTime) * -1;                          //convert time to integer and calculate negative value
                label7.Text = timePenalty.ToString();                                       //show time penalty amount to user
                label8.Text = (controller.TotalScore + BWIN + timePenalty).ToString();      //calculate total score based on score, bonus and time penalty.
            }
            else
            {
                label4.Text = controller.TotalScore.ToString();
                label5.Text = "0";                                                          //no bonus as the player did not win
                string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
                label6.Text = elapsedTime;
                string totalTime = String.Format("{0:0}{1:00}", ts.Minutes, ts.Seconds);
                int timePenalty = Convert.ToInt32(totalTime) * -1;
                label7.Text = timePenalty.ToString();
                label8.Text = (controller.TotalScore + timePenalty).ToString();
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) //Allows the user to use the Mouse to move the paddle left and right.
        {
            controller.Paddle.Move(PointToClient(MousePosition));
        }

        private void button1_Click(object sender, EventArgs e) //The “Resume” button, enables the timer which in-turn resumes the game. Disables itself on use and enables the pause button to pause the game.
        {
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
            stopWatch.Start();
        }

        private void button2_Click(object sender, EventArgs e) //The “Pause” button, pauses the timer which in-turn pauses the game. Disables itself on use and enables the resume button to unpause the game. 
        {
            timer1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
            stopWatch.Stop();
        }

        private void button3_Click(object sender, EventArgs e)  //The "restart" button, starts the stopwatch timer for the game and toggles all of the relevant buttons, panels and controls that are needed to play the game.
        {
            controller.Restart(bufferGraphics, ClientSize); //resets the game field
            button5.Enabled = false;
            button5.Visible = false;
            button6.Enabled = false;
            button6.Visible = false;
            panel2.Visible = false;     //hides the menu panel
            panel2.Enabled = false;
            panel1.Visible = true;      //shows the panel strip with in game controls
            panel1.Enabled = true;
            button1.Enabled = false;    //disable resume button as the game is not paused
            button2.Enabled = true;     //enables pause button
            timer1.Enabled = true;      
            stopWatch.Restart();        
        }

        private void button4_Click(object sender, EventArgs e) //The “Menu” button, executes the MenuScreen method.
        {
            MenuScreen();
        }

        private void button5_Click(object sender, EventArgs e) //The “Exit Game” button, uses application.exit to close the form. 
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e) //The “Start Game” button, uses the same functions as the restart button.
        {
            button3_Click(sender,e);
        }
    }
}
