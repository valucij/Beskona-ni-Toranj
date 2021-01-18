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

            //Ako nije doseglo 490 jos, samo mijenjamo na invisible kako se
            //udaljenost platforma-enemy ne bi mijenjala
            if (Y > 420) visible = false;

            if (Y > 490) { 
                Y = -410;
                this.revive();
            }
        }


        public virtual void Tick(object sender, EventArgs e, Form1 form)
        {

            if (!alive) {
                form.dropEnemycoin();
            }
        }


        public virtual void restart()
        {
            //TO DO
        }

        public virtual void setLocation(int platform_x, int platform_y, int platform_width) {
            x = platform_x+platform_width/2;
            y = platform_y-70;

            figure.Location = new Point(x, y);

            leftlimit_x = platform_x;
            rightlimit_x = platform_x + platform_width;
        }

        public virtual void setLocation(int platform_x, int platform_y)
        {
            X = platform_x;
            Y = platform_y;
        }

        //---------------------------------------------. GRAFIKA ENEMYA .-------------------------------
        public virtual void paint(object sender, PaintEventArgs e)
        {
            // Bitmap walkFrame = returnFrame();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);
        }


            //funkcija koja daje image neprijatelju
        public override void addImage(Bitmap image)
        {
            this.image = image;
            copyFigureInformation();
        }


        //funkcija koja postavlja x,y,height,width
        protected virtual void copyFigureInformation()
        {
            //za pocetak je figure nevidljiv
            figure.Visible = false;

            //uzmi informacije iz pictureboxa, kako bi znali nacrtati enemya
            x = figure.Location.X;
            y = figure.Location.Y;
            width = figure.Width;
            height = figure.Height;

            //ovaj dio koda je relevantan ako cu y pomicati prema gore kad dolje postavljam :)
            figure.Location = new Point(x, y);
        }

        //-----------------------------------------SVOJSTVA-----------------
        public override int X
        {
            get { return x; }
            set { x = value; figure.Location = new Point(value, figure.Location.Y); }
        }
        public override int Y
        {
            get { return y; }
            set { y = value; figure.Location = new Point(x, value); }
        }


    }
}
