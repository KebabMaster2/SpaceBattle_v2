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

        private Spaceship spaceship;
        private Timer mainTimer;
        private Timer enemyTimer = null;
        private Random rand = new Random();
      
        public Battlefield()
        {
            InitializeComponent();
            InitializeBattlefield();
            InitializeMainTimer();
            InitializeEnemyTimer();
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
            if (moveLeft && spaceship.Left>0)
            {
                spaceship.Left -= 2;
            }
            if (moveRight && spaceship.Left+spaceship.Width<ClientRectangle.Width)
            {
                spaceship.Left += 2;
            }
            EnemyBulletCollision();
            EnemySpaceshipCollision();

        }
        private void InitializeEnemyTimer()
        {
            enemyTimer = new Timer();
            enemyTimer.Tick += new EventHandler(EnemyTimer_Tick);
            enemyTimer.Interval = 1500;
            enemyTimer.Start();
        }
        private void EnemyTimer_Tick(object sender, EventArgs e)
        {
            Enemy enemy = new Enemy(rand.Next(1, 6), this);
            enemy.Left = rand.Next(0, this.ClientRectangle.Width - enemy.Width);
            this.Controls.Add(enemy);
        }

        private void Battlefield_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space && !bulletFired)
            {
                spaceship.Fire(this);
                bulletFired = true;
            }
            else if(e.KeyCode == Keys.A || e.KeyCode==Keys.Left)
            {
                moveLeft = true;
            }
            else if(e.KeyCode == Keys.D || e.KeyCode==Keys.Right)
            {
                moveRight = true;
            }
            else if (e.KeyCode == Keys.O)
            {
                if (spaceship.EngineStatus == "off")
                {
                    spaceship.EngineOn();
                }
                else if (spaceship.EngineStatus == "on")
                {
                    spaceship.EngineOff();
                }

            }

        }
        private void EnemyBulletCollision()
        {
            foreach(Control c in this.Controls)
            {
                if((string)c.Tag == "enemy")
                {
                    foreach(Control b in this.Controls)
                    {
                        if ((string)b.Tag == "bullet")
                        {
                            if (c.Bounds.IntersectsWith(b.Bounds))
                            {
                                c.Dispose();
                                b.Dispose();
                            }                        }
                    }
                }
            }
        }
        private void EnemySpaceshipCollision()
        {
            foreach(Control c in this.Controls)
            {
                if ((string)c.Tag == "enemy")
                {
                    if (c.Bounds.IntersectsWith(spaceship.Bounds))
                    {
                        c.Dispose();
                        spaceship.Dispose();
                        GameOver();
                    }
                }
            }
            //GameOver();
        }
        private void GameOver()
        {
            mainTimer.Stop();
            MessageBox.Show("Game over");
        }

        private void Battlefield_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                spaceship.Fire(this);
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
