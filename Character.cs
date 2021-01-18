using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Drawing

/*
 * podaci
 * grafika (picturebox, addimage, addimages)
 * pucanje (isHit)
 * zivot (isDead, revive)
 * svojstva
*/


namespace Beskonačni_Toranj
{

    //klasa koja predstavlja lika, bio on player, neprijatelj ili glavni neprijatelj
    class Character : IImageControl
    {

        //koordinate lika na ekranu
        protected int x, y;
        //originalnekoordinate
        protected int originalX, originalY;
        //velicina slike na ekranu
        protected int width, height;
        //broj zivota lika
        protected int life;
        //originalni(pocetni) broj zivota lika
        protected int originalLife;
        //zastavica koja govori da li je player ziv ili ne
        protected bool alive;
        //flag koji govori je li character visible;
        protected bool visible;
        //boja tracer objekta za testiranje
        protected Color tracer;

        //predstavlja picturebox koji u windows formi predstavlja lika
        protected System.Windows.Forms.PictureBox figure;
        protected List<Bitmap> pictures;

        public Character() {
            tracer = new Color();

            alive = true;
            visible = true;

            //life ne postavljam jer je to custom
        }

        public Character(int l) : this(){
            life = l;
            originalLife = l;
        }

        //-------------------------------------GRAFIKA--------------------------------

        //dodavanje pictureboxa koji je stvoren u windows formi i predstavljat ce lika
        public virtual void addPictureBox(System.Windows.Forms.PictureBox figure)
        {
            this.figure = figure;
        }

        //mice picturebox
        public virtual void removePictureBox()
        {
            figure = null;
        }

        public virtual void addImage(List<Bitmap> pictures)
        {
            this.pictures = pictures;
        }

        public virtual void addImage(Bitmap picture)
        {
            if (this.pictures == null)
            {
                this.pictures = new List<Bitmap>();
            }
            this.pictures.Add(picture);
        }

        //-------------------------------zastave----------------------------------
        public virtual void isHit(){
            life--;
            if (life <= 0) { 
                alive = false;
                setVisibility(false);
            }


        }

        public bool isDead()
        {
            if (alive) return false;
            return true;
        }
        //------------------------------RESETI------------------
        
        public void revive() {
            if (!alive) {
                life = originalLife;
                alive = true;
                }
            setVisibility(true);
        }
        

         public void reset_position()
           {

            x = originalX;
            y = originalY;

            figure.Location = new Point(originalX, originalY);

            }


        //funkcija koja se koristi pri kompletnom resetu 
        public virtual void reset() {
            tracer = new Color();
            revive();
            reset_position();

              //slika i ostale stvari se ne mijenjaju, tako da tjt
                }
       
        public virtual void setVisibility(bool b)
        {
            
            visible = b;


            if (b == false) { figure.Location = new Point(-100, y); } else { figure.Location = new Point(x, y); }

        }
        
        //funkcija koja postavlja x,y,height,width
        protected virtual void copyFigureInformation()
        { 
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
        public int Life
        {
            set { life = value; }
            get { return life; }
        }
        public bool Alive
        {
            set { alive = value; }
            get { return alive; }
        }

        public bool Visible 
        {
            set { visible = value; }
            get{ return visible;  }
        
        }
    }
}
