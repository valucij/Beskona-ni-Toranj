using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    public partial class Form1 : Form
    {
        Player player;
        Platform ground;
        Platform platform_1;
        public Form1()
        {
            InitializeComponent();
          
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            player = new Player();
            player.addPictureBox(playerPictureBox);
            player.addImage(Properties.Resources.stand);

            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            ground = new Platform();
            ground.addPictureBox(groundPictureBox);
            ground.addImage(Properties.Resources.b_fotfor_lush2);

            platform_1 = new Platform();
            platform_1.addPictureBox(platformPictureBox_1);//mora ici prije postavljanja image
            platform_1.addImage(Properties.Resources.b_fotfor_lush2);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player.keyDown(sender, e);    
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            player.keyUp(sender, e);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            player.paint(sender, e);
            platform_1.platformPaint(sender, e);
            ground.platformPaint(sender, e);
           
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            player.timerTick(sender, e, this);
            Invalidate();

        }
    }
}
