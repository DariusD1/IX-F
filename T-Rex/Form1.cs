using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex
{
    public partial class Form1 : Form
    {

        bool jumping = false; // boolean to check if jumping is jumping or not
        bool isGameOver = false;
        Random random = new Random();
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        int position = 0;


        public Form1()
        {
            InitializeComponent();
            GameReset();
        }

        private void GameReset()
        {
            force = 12;
            score = 0;
            jumpSpeed = 0;
            jumping = false;
            obstacleSpeed = 10;
            isGameOver = false;
            TRexPictureBox.Image = Properties.Resources.running;
            ScorLabel.Text = "Scor: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (String)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + random.Next(500, 800) + (x.Width * 10);
                    x.Left = position;
                }
            }
            GameTimer.Start();
        }

        private void labell_Click(object sender, EventArgs e)
        {

        }



        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Space) && (jumping == false))
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (jumping)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }

        private void GameEvent(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GameTimerTick(object sender, EventArgs e)
        {
            TRexPictureBox.Top = TRexPictureBox.Top + jumpSpeed;

            ScorLabel.Text = "Score: " + score + " Jumping speed: " + jumpSpeed + " force: " + force;

            if (jumping == true && force == 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force--;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (TRexPictureBox.Top > 366 && jumping == false)
            {
                force = 15;
                TRexPictureBox.Top = 367;
                jumpSpeed = 0;
            }

            foreach (Control x in Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = ClientSize.Width + random.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (TRexPictureBox.Bounds.IntersectsWith(x.Bounds))
                    {
                        GameTimer.Stop();
                        TRexPictureBox.Image = Properties.Resources.dead;
                        ScorLabel.Text += "Press R to restart the game: ";
                        isGameOver = true;
                    }
                }

                if (score > 5)
                {
                    obstacleSpeed = 15;
                }

            }
        }
    }
}