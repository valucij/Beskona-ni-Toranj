using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    //klasa nasljeduje klasu lika, i predstavlja playera
    class Player : Character
    {
        //ime playera
        private string name;
        //broj bodova koje ima player
        private int score;
        //brzina kojom se krece player
        private int jumpSpeed;
        //gravity koji dijeluje na player
        private int force;
        private int figureSpeed;

        private bool goingLeft;
        private bool goingRight;
        private bool jumping;

        private int x, y;
        private int width, height;
        private Color tracer;

        Bitmap image;

        //konstruktor
        public Player()
        {
            name = "Jojo";
            score = 0;
            jumpSpeed = 10;
            figureSpeed = 18;
            force = 8;
            goingLeft = false;
            goingRight = false;
            jumping = false;
            tracer = new Color();
        }

        public int Score
        {
            set { score = value; }
            get { return score; }
        }

        public int Speed
        {
            set { jumpSpeed = value; }
            get { return jumpSpeed; }
        }

        public int Gravity
        {
            set { force = value; }
            get { return force; }
        }

        public string Name
        {
            get { return name; }
        }

        //funkcija koja obavlja sto se dogada s playerom kad se tipka pritisnuta, poziva se is form.keyDown
        public void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                goingLeft = true;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                goingRight = true;

            }
            else if ( (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && !jumping)
            {
                jumping = true;
            }
        }
        //funckija koja obavlja sto se dogada s playerom kad je tipka pustena, poziva se iz form.keyUp
        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                goingLeft = false;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                goingRight = false;

            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                jumping = false;
            }
        }
        //mozda treba? za animaciju kretanja, mozda dodati i kod ostalih charactera
        public void paint(object sender, PaintEventArgs e)
        {
            //Console.WriteLine("Paint");
             e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
             e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
             e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
             e.Graphics.DrawImage(image, x, y, width, height);
           
        }

        //timer tick mozda treba? mozda dodati i kod ostalih charactera
        public void timerTick(object sender, EventArgs e, Form1 form)
        {

           // Console.WriteLine("TICK");

            figure.Top += jumpSpeed;


            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (jumping)
            {
                force -= 1;
                jumpSpeed = -12;
            }
            else {
                jumpSpeed = 12;
            }

            if (goingLeft && figure.Left > 100)
            {
                figure.Left -= figureSpeed;
            }
            else if (goingRight && figure.Left + (figure.Width + 100) < form.ClientSize.Width)
            {
                figure.Left += figureSpeed;
            }

            foreach (Control x in form.Controls)
            {
                if (x.Tag == "platform" || x.Tag == "ground") 
                {
                    if (figure.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8;
                        figure.Top = x.Top - figure.Height;
                        jumpSpeed = 0;
                    }
                }
            }

            x = figure.Location.X;
            y = figure.Location.Y;
            width = figure.Width;
            height = figure.Height;
            
        }

        public void addImage(Bitmap image)
        {
            this.image = image;
            tracer = image.GetPixel(1, 1);
            this.image.MakeTransparent(tracer);

            //uzimamo sve informacije iz pictureboxa, kako bi znali crtati
            figure.Visible = false;

            x = figure.Location.X;
            y = figure.Location.Y;

            height = figure.Height;
            width = figure.Width;
        }
    }
}
