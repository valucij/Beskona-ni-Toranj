using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    //klasa koja predstavlja gumb koji se nalazi u menu; ovo je vise klasa koja samo sluzi za crtanje
    //slike koje dodjeljena gumbu; naravno isto nasljeduje IImageControl kako bi se
    //gumbu mogao dodjeliti picturebox i kako bi se lakse mogao napraviti event klikanja
    class MenuButton : IImageControl
    {
        //informacije o polozaju gumba
        private int x;
        private int y;
        //informacije o velicini gumba
        private int width;
        private int height;
        //sluzi za transparentnost pozadine slike
        private Color tracer;
        //slika gumba
        Bitmap image;
        //picturebox dodjeljen ovom gumbu
        PictureBox button;

        //konstruktor
        public MenuButton()
        {
            tracer = new Color();
        }
        //konstruktor gdje odmah postavljamo sliku i dodjeljujemo picture box
        public MenuButton(PictureBox figure, Bitmap image)
        {
            tracer = new Color();
            button = figure;
            addImage(image);
        }
        //konstruktor gdje odmah dodjeljujemo picturebox
        public MenuButton(PictureBox figure)
        {
            tracer = new Color();
            button = figure;
        }
        //dodjeljujemo picture box
        public void addPictureBox(PictureBox figure)
        {
            button = figure;
        }
        //micemo picture box
        public void removePictureBox()
        {
            button = null;
        }
        //crtamo gumb, poziva se iz Form1.Form1_Paint
        internal void buttonPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);

        }
        //mijenja da li se picturebox gumba vidi ili ne; jako bitno za event klikanja
        //ako je nevidljiv, ne moze se kliknuti na njega
        internal void Visible(bool visible)
        {
            button.Visible = visible;
        }
        //dodaje se slika gumbu
        public void addImage(Bitmap image)
        {
            this.image = image;
            tracer = image.GetPixel(1, 1);
            this.image.MakeTransparent(tracer);

            //ako se ovo odkomentira, nece se moci kliknuti na gumb
            //ali postoji posebna funkcija za to i na to se pazi izvan ove klase
            //button.Visible = false;
            button.Image = image;
            button.SizeMode = PictureBoxSizeMode.StretchImage;

            x = button.Location.X;
            y = button.Location.Y;

            height = button.Height;
            width = button.Width;
        }
    }
}
