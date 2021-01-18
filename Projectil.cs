using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class Projectil : IImageControl
    {
        protected PictureBox figure;
        //sluze samo za crtanje
        protected int x, y;
        protected int width, height;
        protected int projectilSpeed;
        protected Color tracer;
        protected List<Bitmap> images;
        protected int frameCounter;
        //zastavica unutar ove klase kako bi se kontroliralo crtanje projektila
        protected bool fired;
        protected bool left, right;
         
        //konstruktor
        public Projectil()
        {
            images = new List<Bitmap>();
            tracer = new Color();
            projectilSpeed = 15;
            frameCounter = 0;
            fired = false;
            left = false;
            right = false;
            x = -100;
            y = -100;
        }

        //-----------------------. logistika projektila (Tick).----------------------------------
        //pomice dolje za platformspeed
        public virtual void MoveDown(int PlatformSpeed)
        {
            Y += PlatformSpeed;


            if (Y > 490)
            {
                Y = -410;
                this.reset();
            }
        }

        public void Tick(object sender, EventArgs e, Form1 form)
        {
            //ako nije metak ispucan, ne moram ga tick-ati
            if (!Fired) { X = -100; setVisibility(false);  return; }

            //Ako je metak ispucan, prvo ga pomakni
            if (right)
            {
                figure.Left += projectilSpeed;
            }
            else if (left)
            {
                figure.Left -= projectilSpeed;
            }

            //ako je metak izasao van granica ekrana
            if (x + width > form.ClientSize.Width || x < 0 || y + height > form.ClientSize.Height || y < 0)
            {
                this.reset();
                return;
            };

            //uzimamo te informacije iz figura da znamo nacrtati projectil
            x = figure.Location.X;
            y = figure.Location.Y;
            width = figure.Width;
            height = figure.Height;

        }

        //--------------------------------------------funkcije vezane uz pucanje---------------------------------

        //funkcija koja postavlja zastavice i koordinate za pucanje lijevo
        public void pucajlijevo(int shooter_x, int shooter_y)
        {
            left = true;
            right = false;
            setVisibility(true);
            figure.Location = new Point(shooter_x, shooter_y + 10);

            x = shooter_x;
            y = shooter_y + 10;

          //  Console.WriteLine("lijevo figure.location.x " + figure.Location.X);
        }

        //funkcija koja postavlja zastavice i koordinate za pucanje desno
        public void pucajdesno(int shooter_x, int shooter_y)
        {
            right = true;
            left = false;
            setVisibility(true);

            figure.Location = new Point(shooter_x, shooter_y + 10);

            x = shooter_x;
            y = shooter_y + 10;

            //Console.WriteLine("puca desno");
           // Console.WriteLine("desno figure.location.x " + figure.Location.X);
        }

        //funkcija koja postavlja zastavicu i koordinate za pucanje u kojem god smjeru da su vec zastavice postavljene
        public void pucaj(int shooter_x, int shooter_y) {
            setVisibility(true);

            figure.Location = new Point(shooter_x, shooter_y + 10);

            x = shooter_x;
            y = shooter_y + 10;

        }

        //funkcija koja vraca metak u cahuru (na pocetno stanje)
        public void reset() {
            frameCounter = 0;
            setVisibility(false);
            left = false;
            right = false;
            X = -100;
            Y = -100;
        }

        //-------------------------------------------------------isHit virtualna funkcija

        //vraća je li character pogođen, poziva se iz player.tick ili boss.tick
        public virtual bool hasHit(Form1 form)
        {
            return false;
        }

       
       
        //---------------------------------------------funkcije vezane uz slike-----------------------------------

        //funkcija koja dodaje PictureBox
        public void addPictureBox(PictureBox figure)
        {
            this.figure = figure;
        }

        //funkcija koja miče PictureBox
        public void removePictureBox()
        {
            this.figure = null;
        }

        
        //----------------------------------.GRAFIKA.----------------------------------------
        //crta metak
        public void paint(object sender, PaintEventArgs e)
        {
            //ako metak nije ispucan ne crtam ga
            if (!Fired)  return;

            Bitmap frame = returnNewFrame();
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(frame, x, y, width, height);
        }

        public void addImage(List<Bitmap> images)
        {
            this.images = images;
            copyFigureInformation();
            
        }

        public void addImage(Bitmap image)
        {
            this.images.Add(image);
            copyFigureInformation();
        }

        private void copyFigureInformation()
        {

            setVisibility(false);
            height = figure.Height + 70;
            width = figure.Width + 70;
        }

        public virtual void setVisibility(bool b)
        {
            Fired = b;
            if (b = true) { figure.Visible = true; } else { figure.Visible = false; }
        }

        //vrati novi BitMap frame (tj sliku projektila među frameovima)
        public Bitmap returnNewFrame()
        {
            Bitmap returnValue = images[0];

            if(frameCounter < images.Count)
            {
                returnValue = images[frameCounter];
                frameCounter++;
            }

            if (frameCounter == images.Count)
            {
                frameCounter = 0;
            }

            tracer = returnValue.GetPixel(1, 1);
            returnValue.MakeTransparent(tracer);

            return returnValue;
        
        }

        //------------------------------------------------svojstva--------------------------------------------------
        public int X
        {
            get { return x; }
            set { x = value; figure.Location = new Point(value, figure.Location.Y); }
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

        public int ProjectilSpeed
        {
            set { projectilSpeed = value; }
            get { return ProjectilSpeed; }
        }

        public bool Fired
        {
            set { fired = value; }
            get { return fired; }
        }

        public bool Left
        {
            set { left = value; }
            get { return left; }
        }

        public bool Right
        {
            set { right = value; }
            get { return right; }
        }
    }
}
