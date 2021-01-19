using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * podaci
 * logistika (drop x2, IsPickedUp, reset, changeLocation )
 * grafika (paint, picturebox, addImage)
 * svojstva
*/

namespace Beskonačni_Toranj
{
    class Coin : IImageControl
    {
        //koordinate lika na ekranu
        private int x, y;
        //velicina slike na ekranu
        private int width, height;
        //broj bodova koji coin donosi
        private int coinvalue;
        //zastavica koja govori da li je coin dropped ili ne
        private bool dropped;
        Color tracer;


        //predstavlja picturebox koji u windows formi predstavlja novcic
        protected System.Windows.Forms.PictureBox figure;
        //slika coin-a
        Bitmap image;

        //konstruktor
        public Coin() {
            tracer = new Color();
            dropped = false;
            coinvalue = 1;

            x = -100;
            y = -100;
        }

        //konstruktor koji generira novcic vece vrijednosti
        public Coin(int cv)
        {
            dropped = false;
            coinvalue = cv;

            x = -100;
           y = -100;
        }

        //ovaj konstruktor mi probs ne treba, ali neka ga je
        Coin(int x_wheretodrop, int y_wheretodrop) : this() {
            x = x_wheretodrop;
            y = y_wheretodrop;
        }

        //-------------------------------LOGISTIKA NOVCICA (DROP, ISPICKEDUP, RESET)



        public virtual void Tick(object sender, EventArgs e, Form1 form)
        {
            //coin sam po sebi nista ne radi, on se sam returna ako player naleti na njega
        }

        //funkcija koja droppa novcic na danu lokaciju
        public void drop(int x_wheretodrop, int y_wheretodrop) 
        {
           // Console.WriteLine("DROPPED COIN");
            dropped = true;
            x = x_wheretodrop;
            y = y_wheretodrop;

            figure.Visible = false;
            figure.Location = new Point(x, y);
        }

        //funkcija koja resetira (un-dropa) novcic
        public void reset() {
            //ako nije droppan novcic, vrati se
            if (!dropped) return;

            //ako je droppan novcic
            dropped = false;
            figure.Visible = false;
            figure.Location = new Point(-100, y);
        }

        //mijenja lokaciju novcica
        public void setLocation(int x_wheretodrop, int y_wheretodrop) 
        {
            x = x_wheretodrop;
            y = y_wheretodrop;
        }

        //------------------------------------------------------.GRAFIKA.-------------------------------------------
        //pomice dolje za platformspeed
        public void MoveDown(int PlatformSpeed)
        {
            Y += PlatformSpeed;

            if (Y > 490)
            {
               Y = -410;
                this.reset();
            }
        }

        //crtacoin
        public void paint(object sender, PaintEventArgs e)
        {
            tracer = image.GetPixel(1, 1);
            image.MakeTransparent(tracer);


            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);
        }

        //dodavanje pictureboxa koji je stvoren u windows formi i predstavljat ce lika
        public void addPictureBox(System.Windows.Forms.PictureBox figure)
        {
            this.figure = figure;
            copyFigureInformation();
        }

        //mice picturebox
        public void removePictureBox()
        {
            figure = null;
        }

        public void addImage(Bitmap picture)
        {
            image=picture;
            copyFigureInformation();
        }

        //funkcija koja postavlja x,y,height,width
        protected void copyFigureInformation()
        {
            //neka je isprva nevidljiva figura (dakle pri startu, ne replayu)
            figure.Visible = false;

            //uzmi informacije iz pictureboxa, kako bi znali nacrtati enemya
            x = figure.Location.X;
            y = figure.Location.Y;
            width = figure.Width;
            height = figure.Height;

            //ovaj dio koda je relevantan ako cu y pomicati prema gore kad dolje postavljam :)
            figure.Location = new Point(x, y);
        }

        //------------------------------SVOJSTVA--------------------------------------
        public int X
        {
            get { return x; }
            set { x = value; figure.Location = new Point(value, y); }
        }
        public int Y
        {
            get { return y; }
            set { y = value; figure.Location = new Point(x, value); }
        }


        public int Width
        {
            set { width = value; }
            get { return width; }
        }
        public int Height
        {
            set { height = value; }
            get { return height; }
        }
        public int CoinValue
        {
            set { coinvalue = value; }
            get { return coinvalue; }
        }
        
        public bool Dropped
        {
            set { dropped = value; }
            get { return dropped; }
        }
        
    }
}
