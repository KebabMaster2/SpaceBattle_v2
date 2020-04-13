using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceBattle
{
    class Spaceship : PictureBox
    {
        private int imageCount = 1;
        
        Timer timerAnimate;
        public string EngineStatus { get; set; } = "on";
        public Spaceship()
        {
            InitializeSpaceship();
            InitializeTimerAnimate();
        }

        private void InitializeSpaceship()
        {
            this.BackColor = Color.Transparent;
            this.Height = 80;
            this.Width = 40;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Properties.Resources.rocket_on_1;
        }
        private void InitializeTimerAnimate()
        {
            timerAnimate = new Timer();
            timerAnimate.Tick += new EventHandler(TimerAnimate_Tick);
            timerAnimate.Interval = 100;
            timerAnimate.Start();
        }

        private void TimerAnimate_Tick(object sender, EventArgs e)
        {
            string imageName = "rocket_" + EngineStatus + "_" + imageCount;
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
            imageCount += 1;
            if (imageCount > 4)
            {
                imageCount = 1;
            }
        }
        public void EngineOn()
        {
            EngineStatus = "on";
        }
        public void EngineOff()
        {
            EngineStatus = "off";
        }
    }
}
