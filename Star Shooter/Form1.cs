using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Star_Shooter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
        }

       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            { MoveUp(); }
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            { MoveDown(); }

            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            { MoveForeward(); }

            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            { MoveBack(); }
            else if (e.KeyCode == Keys.Space)
            { Fire(); }
        }
       
        private void MoveUp()
        {
            Point spacePoz = pBSpaceShip.Location;
            if (spacePoz.Y>20)
            {
                pBSpaceShip.Top -= 10;
                pBSpaceShip.ImageLocation = @"c:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\ship\left.png";
            }
           
            
        }
        private void MoveDown()
        {
            Point spacePoz = pBSpaceShip.Location;
            if (spacePoz.Y < 469)
            {
                pBSpaceShip.Top += 10;
                pBSpaceShip.ImageLocation = @"c:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\ship\right.png";
            }
        }
        private void MoveForeward()
        {
            Point spacePoz = pBSpaceShip.Location;
            if (spacePoz.X < 1350)
            { pBSpaceShip.Left += 10; }
        }
        private void MoveBack()
        {
            Point spacePoz = pBSpaceShip.Location;
            if (spacePoz.X > 20)
            { pBSpaceShip.Left -= 10; }
        }

        private void Fire()
        {
            axWindowsMediaPlayer2.URL = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\ship\fire.mp3";
            axWindowsMediaPlayer2.Ctlcontrols.play();
            Point firePoz = pBSpaceShip.Location;
            firePoz.X = firePoz.X + 120;
            firePoz.Y = firePoz.Y + (109/2);
            PictureBox fire = new PictureBox();
            fire.ImageLocation = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\ship\fire.png";
            fire.Height = 15;
            fire.Width = 20;
            fire.Name = "fire";
            fire.Location=firePoz;
            panel1.Controls.Add(fire);
            axWindowsMediaPlayer2.Ctlcontrols.stop();

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up || e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                pBSpaceShip.ImageLocation= @"c:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\ship\ship.png";
            }   
        }
        
        private void CreateAliens()
        {
            Random rnd = new Random();
            int randomNumber=rnd.Next(0, 5);
            int randomPozition= rnd.Next(0, 5);
            PictureBox alien = new PictureBox();
            alien.Name = "Alien";
            alien.Width = 75;
            alien.Height = 75;
           

            switch (randomNumber)
            {
                case 1:
                    alien.ImageLocation = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\Aliens\1.png";
                    break;
                case 2:
                    alien.ImageLocation = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\Aliens\2.png";
                    break;
                case 3:
                    alien.ImageLocation = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\Aliens\3.png";
                    break;
                case 4:
                    alien.ImageLocation = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\Aliens\4.png";
                    break;
            }
            switch (randomPozition)
            {
                case 1:
                    alien.Location = new Point(panel1.Width, 100);
                    panel1.Controls.Add(alien);
                    break;

                case 2:
                    alien.Location = new Point(panel1.Width, 200);
                    panel1.Controls.Add(alien);
                    break;
                case 3:
                    alien.Location = new Point(panel1.Width, 300);
                    panel1.Controls.Add(alien);
                    break;

                case 4:
                    alien.Location = new Point(panel1.Width, 400);
                    panel1.Controls.Add(alien);
                    break;

            }
            alien.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }
        private void Stars()
        {
            PictureBox stars = new PictureBox();
            stars.Name = "Stars";
            stars.ImageLocation = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\star\star.png";
            stars.Width = 10;
            stars.Height = 10;
            stars.SizeMode = PictureBoxSizeMode.StretchImage;
            Random rnd = new Random();
            int starLocation = rnd.Next(10,panel1.Height-10);
            stars.Location = new Point(panel1.Width, starLocation);
            stars.BackColor = Color.Transparent;
            panel1.Controls.Add(stars);
        }

        int counter = 0;
        int score = 0;
        int life = 3;

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                PictureBox pictures = (PictureBox)panel1.Controls[i];
                if (pictures.Name == "fire")
                {
                    if (pictures.Location.X > panel1.Width)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pictures.Left += 10;
                    }
                    for (int j = 0; j < panel1.Controls.Count; j++)
                    {
                        PictureBox aliensPB = (PictureBox)panel1.Controls[j];
                        if (aliensPB.Name=="Alien")
                        {
                            if (pictures.Location.X + pictures.Width > aliensPB.Location.X  )
                            {
                                if (pictures.Location.Y + pictures.Height > aliensPB.Location.Y  && pictures.Location.Y + pictures.Height < aliensPB.Location.Y + aliensPB.Height)
                                {
                                    panel1.Controls.RemoveAt(j);
                                    panel1.Controls.Remove(pictures);
                                    score++;
                                    label2.Text = "Score: " + score.ToString();
                                }
                            }
                        }
                    }
                }

                else if (pictures.Name == "Alien")
                {
                    if (pictures.Location.X <= 0)
                    {
                        panel1.Controls.RemoveAt(i);
                    }

                    else
                    {
                        pictures.Left -= 10;
                        if (pBSpaceShip.Location.X + pBSpaceShip.Width > pictures.Location.X  && pBSpaceShip.Location.X < pictures.Location.X)
                        {
                            if (pBSpaceShip.Location.Y < pictures.Location.Y && pBSpaceShip.Location.Y + pBSpaceShip.Height > pictures.Location.Y)
                            {
                                panel1.Controls.Remove(pictures);
                                life--;
                                label1.Text = "Life: "+ life;
                                if (life == 0)
                                {
                                    axWindowsMediaPlayer2.URL = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\sounds\explode.mp3";
                                    axWindowsMediaPlayer2.Ctlcontrols.play();
                                    timer1.Stop();
                                    axWindowsMediaPlayer2.Ctlcontrols.play();
                                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                                    panel2.Visible = true;
                                    label3.Text = "Score: " + Environment.NewLine+score;
                                }
                                
                            }
                        }
                    }

                   

                }

                else if (pictures.Name == "Stars")
                {
                    if (pictures.Location.X <= 0)
                    {
                        panel1.Controls.RemoveAt(i);
                    }

                    else
                    {
                        pictures.Left -= 3;
                    }
                }
            }
            if (counter == 25)
            {
                CreateAliens();
                counter = 0;
            }
            else if (counter == 1)
            { Stars(); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            axWindowsMediaPlayer1.URL = @"C:\Users\Hawic\source\repos\Star Shooter\Star Shooter\Resources\sounds\background.mp3";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms[0] == this)
            { Application.Restart(); }
        }
    }
}
