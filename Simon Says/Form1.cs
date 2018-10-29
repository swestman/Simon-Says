using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Simon_Says
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        
        // declare variables
        int onInList = 0;
        List<int> pattern = new List<int>();
        Random rand = new Random();
        bool playingBack = false;



        //Set up buttons
        private void RedButton_Click(object sender, EventArgs e)
        {
            testCorrect(0);
        }

        private void BlueButton_Click(object sender, EventArgs e)
        {
            testCorrect(1);
        }

        private void YellowButton_Click(object sender, EventArgs e)
        {
            testCorrect(2);
        }

        private void GreenButton_Click(object sender, EventArgs e)
        {
            testCorrect(3);
        }



        //Game Logic - If playback sequence is running, do nothing.  If the correct sequence is entered, increase the sequence count, otherwise end the game and display message.  Update the score and pattern count labels
        void testCorrect(int Color)
        {
            if (playingBack)
            {
                return;
            }
            if (pattern[onInList] == Color)
            {
                onInList = onInList + 1;
            }
            else
            {
                MessageBox.Show("Game Over!  Your final score is: " + pattern.Count.ToString());
                onInList = 0;
                pattern = new List<int>();
                new Thread(playback).Start();
            }
            if (onInList >= pattern.Count)
            {
                pattern.Add(rand.Next(0, 4));
                onInList = 0;
                new Thread(playback).Start();
            }
            ScoreLabel.Text = ("Score: " + pattern.Count.ToString());
            PatternLabel.Text = ("Pattern: " + onInList.ToString());
        }
            


        // Buttons
        void playback()
        {
            playingBack = true;
            foreach(int color in pattern)
            {
                switch (color)
                {
                    case 0:
                        RedButton.BackColor = Color.Red;
                        Thread.Sleep(500);
                        RedButton.BackColor = Color.Transparent;
                        break;

                    case 1:
                        BlueButton.BackColor = Color.Blue;
                        Thread.Sleep(500);
                        BlueButton.BackColor = Color.Transparent;
                        break;

                    case 2:
                        YellowButton.BackColor = Color.Yellow;
                        Thread.Sleep(500);
                        YellowButton.BackColor = Color.Transparent;
                        break;

                    case 3:
                        GreenButton.BackColor = Color.Green;
                        Thread.Sleep(500);
                        GreenButton.BackColor = Color.Transparent;
                        break;
                }
                Thread.Sleep(500);
            }
            playingBack = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pattern.Add(rand.Next(0, 4));
            new Thread(playback).Start();
        }
    }
}
