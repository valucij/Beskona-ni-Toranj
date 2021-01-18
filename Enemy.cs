using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class Enemy : Character
    {
        //slika enemy-a 
        protected Bitmap image;

        //lijevi i desni limit moje platforme 
        protected int leftlimit_x;
        protected int rightlimit_x;

        //nasljedjuje konstruktor
        public Enemy() : base() {
            leftlimit_x = 50;
            rightlimit_x = 670;
        }

        //nasljedjuje konstruktor sa brojem zivota
        public Enemy(int l) : base(l) {
            leftlimit_x = 50;
            rightlimit_x = 670;
        }



        //-----------------------------------------------. LOGISTIKA ENEMYA .-------------------------------------
        //pomice dolje za platformspeed
        public virtual void MoveDown(int PlatformSpeed) {
            Y += PlatformSpeed;

            if (Y > 490) { 
                Y = -410;
                this.revive();
            }
        }


        public virtual void Tick(object sender, EventArgs e, Form1 form)
        {
            //enemy samo sjedi na platformi, tako da se tu zapravo nista ne dogadja
        }


        public virtual void restart()
        {
            revive();

        }

        public virtual void setLocation(int platform_x, int platform_y, int platform_width) {
            x = platform_x+platform_width/2-60;
            y = platform_y-70;

            figure.Location = new Point(x, y);

            leftlimit_x = platform_x-20;
            rightlimit_x = platform_x + platform_width-10;

            originalX = X;
            originalY = Y;
        }

        //---------------------------------------------. GRAFIKA ENEMYA .-------------------------------
        public virtual void paint(object sender, PaintEventArgs e)
        {
            // Bitmap walkFrame = returnFrame();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            //e.Graphics.DrawImage(image, x, y, width, height);

            //OVO ZAKOMENTIRATI
            // Create pen.
              Pen blackPen = new Pen(Color.FromArgb(5,0,0), 3);

            // Create rectangle.
            Rectangle rect = new Rectangle(x, y, 60, 60);

            // Draw rectangle to screen.
            e.Graphics.DrawRectangle(blackPen, rect);
        }


            //funkcija koja daje image neprijatelju
        public override void addImage(Bitmap image)
        {
            this.image = image;
            copyFigureInformation();
        }


        //-----------------------------------------SVOJSTVA-----------------
        public override int X
        {
            get { return x; }
            set { x = value; figure.Location = new Point(value, y); }
        }
        public override int Y
        {
            get { return y; }
            set { y = value; figure.Location = new Point(x, value); }
        }


    }
}
