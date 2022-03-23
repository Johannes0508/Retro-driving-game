using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RIKTIGA_RETRO_RACING_GAME_nya
{
    public partial class Form1 : Form
    {
        //int variabler till koden
        int roadSpeed;
        int trafficSpeed;
        int playerSpeed = 12;
        int score;
        int carImage;


        Random rand = new Random();
        Random carPosition = new Random();

        //bool data typ som gör att man kan åka från höger till vänster
        bool goleft, goright;



        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //kod så att man kan åka höger och vänster.
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //kod som gör att om knappen är nere så ska inte bilen göra något.
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            //att text score ska ökas när spelet startas.
            txtScore.Text = "Score " + score;
            score++;


            //bestämmer hur långt man ska kunna få åka från öger till vänster
            if(goleft == true && player.Left > 10)
            {
                player.Left -= playerSpeed;

            }
            if (goright == true && player.Left < 265)
            {
                player.Left += playerSpeed;

            }

            //att roadtrack rör sig i samma fart som roadspeed
            roadTrack2.Top += roadSpeed;
            roadTrack1.Top += roadSpeed;

            //if statements om hur banan ska rulla(vilket inte funka!)
            if (roadTrack2.Top > 518)
            {
                roadTrack2.Top = -518;
            }

            if (roadTrack1.Top > 518)
            {
                roadTrack1.Top = -518;
            }

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;


            //om AI1 går under 530 så ska den byta bil
            if(AI1.Top > 530)
            {
                changeAIcars(AI1);

            }

            //om AI2 går under 530 så ska den byta bil
            if (AI2.Top > 530)
            {
                changeAIcars(AI2);
            }

            if (player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI1.Bounds))
            {
                gameOver();

            }

            //kod som visar vilken medalj som ska synas, och när banan ska bli snabbare
            if (score > 40 && score < 500)
            {
                award.Image = Properties.Resources.bronze;
            }

            if (score > 500 && score < 1250 )
            {
                award.Image = Properties.Resources.silver;
                roadSpeed = 25;
                trafficSpeed = 35;
            }

            if (score > 2000)
            {
                award.Image = Properties.Resources.gold;
                roadSpeed = 20;
                trafficSpeed = 22;
            }


        }

        private void changeAIcars(PictureBox tempCar)
        {

            //en random funktion som slumpässigt slumpar ett nummer från 1 - 9
            carImage = rand.Next(1, 9);

            switch (carImage)
            {
                //när den har slumpats till t.ex. 1 så kommer case 1 att aktiveras och köras.
                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;

                case 2:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;

                case 3:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;

                case 4:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;

                case 5:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;

                case 6:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;

                case 7:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;

                case 8:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;

                case 9:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;

            }

            tempCar.Top = carPosition.Next(100, 400) * -1;

            if ((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(5, 200);
            }

            if((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(245, 422);
            }

        }

        //vad som ska gända när man har förlorat!
        private void gameOver()
        {
            playSound();
            gameTimer.Stop();

            award.BringToFront();
            award.Visible = true;

            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            btnStart.Enabled = true;



        }

        //vad som ska hända när spelet startas
        private void ResetGame()
        {
            btnStart.Enabled = false;
            explosion.Visible = false;
            award.Visible = false;
            goleft = false;
            goright = false;
            score = 0;
            award.Image = Properties.Resources.bronze;

            roadSpeed = 12;
            trafficSpeed = 15;

            AI1.Top = carPosition.Next(200, 500) * -1;
            AI2.Left = carPosition.Next(5, 200);

            AI2.Top = carPosition.Next(200, 500) *-1;
            AI2.Left = carPosition.Next(245, 422);

            gameTimer.Start();
        }

        //kör ResetGame funktionen
        private void restartGame(object sender, EventArgs e)
        {
            ResetGame();
        }

        

        private void playSound()
        {

        }
    }
}
