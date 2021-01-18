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
        //flag koji govori je li character visible;
        private bool visible;

        //predstavlja picturebox koji u windows formi predstavlja novcic
        protected System.Windows.Forms.PictureBox figure;
        //slika coin-a
        Bitmap image;

        //konstruktor
        public Coin() {
            dropped = false;
            coinvalue = 1;
            visible = false;
        }

        //konstruktor koji generira novcic vece vrijednosti
        public Coin(int cv)
        {
            dropped = false;
            coinvalue = cv;
            visible = false;
        }

        //ovaj konstruktor mi probs ne treba, ali neka ga je
        Coin(int x_wheretodrop, int y_wheretodrop) : this() {
            x = x_wheretodrop;
            y = y_wheretodrop;
        }

        //-------------------------------LOGISTIKA NOVCICA (DROP, ISPICKEDUP, RESET)



        public virtual void Tick(object sender, EventArgs e, Form1 form)
        {
            //pokusava pick upati coin, postavlja zastavice i javlja formi ako je pickupan
            this.Try_PickUpCoin(form);
        }

        //funkcija koja droppa novcic na danu lokaciju
        public void drop(int x_wheretodrop, int y_wheretodrop) 
        {
            //ako je novcic vec droppan, return
            if (dropped) return;

            //ako novcic nije droppan
            dropped = true;
            visible = true;
            x = x_wheretodrop;
            y = y_wheretodrop;
        }

        //funkcija koja droppa novcic
        public void drop()
        {

            //ako je novcic vec droppan, return
            if (dropped) return;

            //ako novcic nije droppan
            dropped = true;
            visible = true;

        }

        //funkcija koja vraca bool je li novcic pokupljen
        public bool isPickedUp(Form1 form)
        {
            //ako novcic nije droppan, vrati false
            if (!dropped) return false;

            //ako je novcic droppan
            foreach (Control c in form.Controls)
            {
                //ako dodirne playera, vraca formi informaciju
                if ((string)c.Tag == "player" && figure.Bounds.IntersectsWith(c.Bounds))
                {
                    return true;
                }

            }

            //inace vrati false
            return false;
        }

        public void Try_PickUpCoin(Form1 form) {

            if (dropped && this.isPickedUp(form)) {

                //javlja formi da je coin picked up
                form.hasPickedUp(coinvalue);

                //resetira coin, undroppa ga
                this.reset();
            }
        
        
        }

        //funkcija koja resetira (un-dropa) novcic
        public void reset() {

            //ako nije droppan novcic, vrati se
            if (!dropped) return;

            //ako je droppan novcic
            dropped = false;
            visible = false;
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
            y += PlatformSpeed;
            if (Y > 490)
            {
                Y = -410;
                this.reset();
            }
        }

        //crtacoin
        public void paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);
        }

        //dodavanje pictureboxa koji je stvoren u windows formi i predstavljat ce lika
        public void addPictureBox(System.Windows.Forms.PictureBox figure)
        {
            this.figure = figure;
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


        //------------------------------SVOJSTVA--------------------------------------
        public virtual int X
        {
            set { x = value; }
            get { return x; }
        }

        public virtual int Y
        {
            set { y = value; }
            get { return y; }
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

        public bool Visible
        {
            set { visible = value; }
            get { return visible; }

        }
    }
}
