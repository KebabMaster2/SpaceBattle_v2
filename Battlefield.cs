using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceBattle
{
    public partial class Battlefield : Form
    {
        bool moveLeft = false;
        bool moveRight = false;
        bool gameOver = false;
        bool bulletFired = false;

        Spaceship spaceship;
        Bullet bullet;
        Timer mainTimer;
      
        public Battlefield()
        {
            InitializeComponent();
            InitializeBattlefield();
            InitializeMainTimer();
        }

        private void InitializeBattlefield()
        {
            spaceship = new Spaceship();
            spaceship.Left = ClientRectangle.Width - (ClientRectangle.Width / 2 + spaceship.Width / 2);
            spaceship.Top = 300;
            this.Controls.Add(spaceship);
        }
        private void InitializeMainTimer()
        {
            mainTimer = new Timer();
            mainTimer.Tick += new EventHandler(MainTimer_Tick);
            mainTimer.Interval = 10;
            mainTimer.Start();
        }
        private void MainTimer_Tick(object sender, EventArgs e)
        {

        }

        private void FireBullet()
        {
            bullet = new Bullet();
            bullet.Top = spaceship.Top;
            bullet.Left = spaceship.Left + (spaceship.Width / 2 - bullet.Width / 2);
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void Battlefield_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space && !bulletFired)
            {
                FireBullet();
                bulletFired = true;
            }
            else if(e.KeyCode == Keys.A || e.KeyCode==Keys.Left)
            {
                spaceship.Left -= 10;
                moveLeft = true;
            }
            else if(e.KeyCode == Keys.D || e.KeyCode==Keys.Right)
            {
                spaceship.Left += 10;
                moveRight = true;
            }
        }

        private void Battlefield_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                FireBullet();
            }
        }

        private void Battlefield_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                bulletFired = false;
            }
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                moveLeft=false;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight=false;
            }
        }
    }
}
