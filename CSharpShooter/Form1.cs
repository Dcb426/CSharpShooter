using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;

namespace CSharpShooter
{

    public partial class Form1 : Form
    {
        bool goleft;
        bool goright;
        bool goup;
        bool godown;
        int speed = 2;
        int score = 0;
        bool isPressed;
        int totalEnemies = 16;
        int currentEnemies = 15;
        int playerSpeed = 9;
        int lives = 5;
        int number_of_bullets = 30;
        List<Control> Enemies = new List<Control>();
        public Form1()
        {
            InitializeComponent();
            ModifyProgressBarColor.SetState(PlayerLifeBar, 2);
            playSimpleSound();
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "invaders" || x.Tag == "invaders2")
                {
                    Enemies.Add(x);
                }
            }
            startGame();

        }
        private void playSample()
        {
            SoundPlayer sample = new SoundPlayer(@"C:\Users\user1\Documents\Visual Studio 2015\Projects\CSharpShooter\CSharpShooter\Resources\ufoshoot.wav");
            sample.Play();
        }
        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\user1\Documents\Visual Studio 2015\Projects\CSharpShooter\CSharpShooter\Resources\Mega_Hyper_Ultrastorm.wav");
            simpleSound.Play();
        }
        private void StopSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\user1\Documents\Visual Studio 2015\Projects\CSharpShooter\CSharpShooter\Resources\Mega_Hyper_Ultrastorm.wav");
            simpleSound.Stop();
        }
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if(e.KeyCode == Keys.Space && !isPressed && number_of_bullets!= 0)
            {
                isPressed = true;
                makeBullet();
                SystemSounds.Beep.Play();
                number_of_bullets--;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if(isPressed)
            {
                isPressed = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (goleft && player.Left >= 20)
            {
                player.Left -= playerSpeed;
                
            }
            else if(goright && player.Left <= 650)
            {
                player.Left += playerSpeed;
                
            }
            else if(goup)
            {
                player.Top -= playerSpeed;
            }
            else if (godown)
            {
                player.Top += playerSpeed;
            }
            if (PlayerLifeBar.Value == 180)
            {
                StopSimpleSound();
                gameOver();
                player.Dispose();
                MessageBox.Show("You Have Been Destroyed");
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "invaders")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {

                        StopSimpleSound();
                        gameOver();
                        MessageBox.Show("You Have Been Captured");
                    }
                    ((PictureBox)x).Left += speed;
                    if (((PictureBox)x).Left > 720)
                    {
 
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;
                        ((PictureBox)x).Left = -50;

                    }
                }
                if (x is PictureBox && x.Tag == "invaders2")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {

                        StopSimpleSound();
                        gameOver();
                        MessageBox.Show("You Have Been Captured");
                    }
                    ((PictureBox)x).Left -= speed+1;
                    if (((PictureBox)x).Left < 0)
                    {


                        ((PictureBox)x).Top += ((PictureBox)x).Height + 40;
                        ((PictureBox)x).Left = 720;

                    }
                }
            }
            foreach(Control y in this.Controls)
            {
                if(y is PictureBox && y.Tag == "bullet")
                {
                    y.Top -= 20;
                    if(((PictureBox)y).Top < this.Height - 490)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "bullet2")
                {
                    y.Top += 15;
                    if (((PictureBox)y).Top < this.Height - 490)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }
            foreach (Control i in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && i.Tag == "invaders")
                    {
                        if (j is PictureBox && j.Tag == "bullet")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                score += 100;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                                currentEnemies--;
                                Enemies.Remove(i);
                                SystemSounds.Exclamation.Play();
                            }
                        }
                    }
                    if (i is PictureBox && i.Tag == "invaders2")
                    {
                        if (j is PictureBox && j.Tag == "bullet")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                score += 100;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                                currentEnemies--;
                                Enemies.Remove(i);
                                SystemSounds.Exclamation.Play();
                            }
                        }
                    }
                }
            }
            foreach (Control i in this.Controls)
            {
                foreach(Control j in this.Controls)
                {
                    if(i is PictureBox && i.Tag == "bullet")
                    {
                        if(j is PictureBox && j.Tag == "bullet2")
                        {
                            if(i.Bounds.IntersectsWith(j.Bounds))
                            {
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                    if (i is PictureBox && i.Tag == "bullet")
                    {
                        if (j is PictureBox && j.Tag == "bullet2")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                }
            }
            foreach (Control i in this.Controls)
            {
                if (i is PictureBox && i.Tag == "bullet2")
                {
                    if (((PictureBox)i).Bounds.IntersectsWith(player.Bounds))
                    {
                       
                        PlayerLifeBar.Increment(25);
                        this.Controls.Remove(i);
                    }
                }

            }
            label1.Text = "Score : " + score;
            label2.Text = "Ammo : " + number_of_bullets;
            if ((score / 100) >= totalEnemies - 1)
            {
                gameOver();
                StopSimpleSound();
                MessageBox.Show("You Have Saved The Galaxy!!");
            }
        }

        private async void makeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.M484BulletCollection1;
            bullet.Size = new Size(10, 20);
            bullet.Tag = "bullet";
            bullet.Left = player.Left + player.Width / 2;
            bullet.Top = player.Top - 20;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }
        private void makeBullet2(List<Control> X, int index)
        {
            PictureBox bullet2 = new PictureBox();
            bullet2.Image = Properties.Resources.enemyshoot;
            bullet2.Size = new Size(10, 20);
            bullet2.Tag = "bullet2";
            bullet2.Left = X[index].Left + X[index].Width / 2;
            bullet2.Top = X[index].Top + 20;
            this.Controls.Add(bullet2);
            bullet2.BringToFront();
        }
        private void gameOver()
        {
            timer1.Stop();
            timer2.Stop();
            label1.Text += " Game Over";
            button1.Visible = true;
        }
        private void startGame()
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int mon = rnd.Next(0, currentEnemies);
            makeBullet2(Enemies, mon);
            playSample();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();

        }
    }
}
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }