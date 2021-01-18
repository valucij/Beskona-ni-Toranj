using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * podaci
 * konstruktor
 * logistika boss-a (ticker, restart)
 * kontrola grafike (paint, AddImage...)
 * stvari vezane uz pucanje
*/

namespace Beskonačni_Toranj
{
    class Boss : Enemy
    {
        //originalne koordinate
        private int originalX, originalY;
        //brzina boss-a
        private int bossSpeed;
        //metak od neprijatelja
        ProjectilShotByBoss projectil;
        //jel se boss krece lijevo ili desno
        private bool left, right;

        //nasljedjuje konstruktor
        public Boss():base()
        {
            //stvari koje samo boss ima
            projectil = new ProjectilShotByBoss();
            bossSpeed = 3;
            left = true;
            right = false;
        }

        //nasljedjuje konstruktor sa brojem zivota
        public Boss(int l) : base(l)
        {
            //stvari koje samo boss ima
            projectil = new ProjectilShotByBoss();
            bossSpeed = 3;
            left = true;
            right = false;
        }

        //-----------------------------------------------. LOGISTIKA BOSSA .-------------------------------------
        //pomice dolje za platformspeed
        public virtual void MoveDown(int PlatformSpeed)
        {
            Y += PlatformSpeed;
            projectil.MoveDown(PlatformSpeed);
        }

        public override void Tick(object sender, EventArgs e, Form1 form)
        {

            projectil.Tick(sender, e, form);
            if (projectil.hasHit(form))
            {
                form.playerIsHit();
            }

            if (left)
            {
                figure.Left -= bossSpeed;
                x -= bossSpeed;
            }
            else {
                figure.Left += bossSpeed;
                x += bossSpeed;
            }

            if (x > 670)
            {
                right = false;
                left = true;

                projectil.pucajlijevo(x, y);      
            }

            if (x < 50)
            {
                right = true;
                left = false;

                projectil.pucajdesno(x, y);
            }

            //uzimamo informaciju if pictureboxa kako bi znali nacrtati bossa
            x = figure.Location.X;
            y = figure.Location.Y;
            width = figure.Width;
            height = figure.Height;

        }
       

        public void restart()
        {
            tracer = new Color();
           // projectil = new ProjectilShotByBoss(); MAKNUTI OVO, OVO NE!
            bossSpeed = 3;
            alive = true;

            left = true;
            right = false;

           
            returnOriginalFigurePostion();

        }

        //--------------------------------------------------.KONTROLA SLIKE/FIGURE-A.-------------------------------------------------

        public void paint(object sender, PaintEventArgs e)
        {
            // Bitmap walkFrame = returnFrame();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);

            projectil.paint(sender, e);

        }

        //funkcija koja daje image neprijatelju
        public override void addImage(Bitmap image)
        {
            this.image = image;
            copyFigureInformation();
        }


        //funckija koja kopira 
        protected virtual void copyFigureInformation()
        {
            figure.Visible = false;

            x = figure.Location.X;
            y = figure.Location.Y + 20; //+20 je da se slika lijepo prikazuje

            height = figure.Height;
            width = figure.Width;

            originalX = figure.Location.X;
            originalY = figure.Location.Y + 20; //+20 je da se slika lijepo prikazuje

            figure.Location = new Point(x, originalY);
        }

        private void returnOriginalFigurePostion()
        {
            figure.Visible = false;

            x = originalX;
            y = originalY;

            figure.Location = new Point(originalX, originalY);
            
        }
        
  

        //-------------------------------------STVARI VEZANE UZ PUCANJE/PROJEKTIL-------------
        public void addProjectilImage(Bitmap image)
        {
            projectil.addImage(image);
        }

        public void addProjectilImage(List<Bitmap> image)
        {
            projectil.addImage(image);
        }

        public void addProjectilPictureBox(PictureBox picturebox)
        {
            projectil.addPictureBox(picturebox);
        }

        public void removeProjectilPictureBox()
        {
            projectil.removePictureBox();
        }


        //------------------------------SVOJSTVA------------------
        public int X
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
