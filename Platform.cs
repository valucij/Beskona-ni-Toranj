using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{   
    //klasa predstavlja platforme na koje player moze skakati; nasljeduje klasu IImageControl
    class Platform : IImageControl
    {
        //sluzi samo kao tocka vodilja kako treba nacrtati sliku
        private System.Windows.Forms.PictureBox platform;
        //sluzi kako bi pozadina slike platforme bila transparentna
        private Color tracer;
        //slika platforme, ono sto se crta
        private Bitmap image;
        //informacije o tome gdje se nalazi platforma s obzirom na formu
        private int x, y;
        //informacije o velicini platforme
        private int width, height;
        //originalne informacije, informacije o platformi pri stvaranju
        private int originalX, originalY;
        private int originalWidth, originalHeight;
        //pointer na enemya koji mozebitno sjedi na platformi
       Enemy enemy;
        //pointer na bossa koji mozda sjedi na platformi;
       Boss boss;

        //konstruktor
        public Platform()
        {
            tracer = new Color();
            enemy = null;
            boss = null;
        }

        public Platform(System.Windows.Forms.PictureBox figure, Bitmap image)
        {
            tracer = new Color();
            platform = figure;
            addImage(image);
            enemy = null;
            boss = null;
        }

        //dodjeljuje klasi odgovarajuci picture box, kako bi lakse bilo crtati sliku platforme bez nagadanja gdje
        //bi se trebala nalaziti u formi
        public void addPictureBox(System.Windows.Forms.PictureBox figure )
        {
            platform = figure;
        }

        //micanje dodjeljenog picture boxa
        public void removePictureBox()
        {
            platform = null;
        }

        //crtanje platforme; poziva se iz Form1.Form1_Paint
        public void platformPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);
        }

        //dodaje se slika platforme; odmah se radi da je pozadina transparetna
        public void addImage(Bitmap image)
        {
            this.image = image;
            tracer = image.GetPixel(1, 1);
            this.image.MakeTransparent(tracer);

            //uzimamo sve informacije iz pictureboxa, kako bi znali crtati
            platform.Visible = false;

            x = platform.Location.X;
            y = platform.Location.Y;

            height = platform.Height;
            width = platform.Width;

            originalHeight = height;
            originalWidth = width;
            originalX = x;
            originalY = y;
        }

        public int X
        {
            get { return x; }
            set { x = value; platform.Location = new Point(value, y); }
        }

        public int Y
        {
            get { return y; }
            set { y = value; platform.Location = new Point(x, value); }
        }

        public int OriginalX
        {
            set { originalX = value; }
            get { return originalX; }
        }

        public int OriginalY
        {
            set { originalY = value; }
            get { return originalY; }
        }

        public int OriginalHeight
        {
            set { originalHeight = value; }
            get { return originalHeight; }
        }
        public int OriginalWidth
        {
            set { originalWidth= value; }
            get { return originalWidth; }
        }

        //funkcija resetira platformu na pocetnu poziciju
        public void restart()
        {
            x = originalX;
            y = originalY;
            width = originalWidth;
            height = originalHeight;
            //jako bitno je i resetirati picturebox koji je dodjeljen platformi, jer 
            //po njemu se sve orijentira 
            platform.Location = new Point(originalX, originalY);

        }
    }
}
