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

        //nasljedjuje konstruktor
        public Enemy() : base() { }

        //nasljedjuje konstruktor sa brojem zivota
        public Enemy(int l) : base(l) { }



        //-----------------------------------------------. LOGISTIKA ENEMYA .-------------------------------------
        //pomice dolje za platformspeed
        public virtual void MoveDown(int PlatformSpeed) {
            Y += PlatformSpeed;
        }


        public virtual void Tick(object sender, EventArgs e, Form1 form)
        {

            if (!alive) {
                form.dropEnemycoin();
            }
        }


        public void restart()
        {
            //TO DO
        }

        //---------------------------------------------. GRAFIKA ENEMYA .-------------------------------
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
