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
        //brzina boss-a
        private int bossSpeed;
        //metak od neprijatelja
        ProjectilShotByBoss projectil;
        //jel se boss krece lijevo ili desno
        private bool left;


        //nasljedjuje konstruktor
        public Boss():base()
        {
            //stvari koje samo boss ima
            projectil = new ProjectilShotByBoss();
            bossSpeed = 3;
            left = true;
        }

        //nasljedjuje konstruktor sa brojem zivota
        public Boss(int l) : base(l)
        {
            //stvari koje samo boss ima
            projectil = new ProjectilShotByBoss();
            bossSpeed = 3;
            left = true;
        }

        //-----------------------------------------------. LOGISTIKA BOSSA .-------------------------------------
        //pomice dolje za platformspeed
        public override void MoveDown(int PlatformSpeed)
        {
            Y += PlatformSpeed;
            projectil.MoveDown(PlatformSpeed);

            if (Y > 490)
            {
                Y = -410;
                this.revive();
                this.resetProjectile();
            }
        }
    

    public override void Tick(object sender, EventArgs e, Form1 form)
        {
            //prvo chekiramo bossov projektil, da li je mozda ubio playera (javlja se playeru kroy formu)
            projectil.Tick(sender, e, form);
            if (projectil.hasHit(form))
            {
                form.playerIsHit();
            }

            //sada pomicemo bossa
            if (left)
            {
                figure.Left -= bossSpeed;
                X -= bossSpeed;
            }
            else {
                figure.Left += bossSpeed;
                X += bossSpeed;
            }

            if (X > rightlimit_x)
            {
                left = true;

                projectil.pucajlijevo(X, Y);      
            }

            if (X < leftlimit_x)
            {
                left = false;

                projectil.pucajdesno(X, Y);
            }

        }
       
        //funkcija za restart igre
        public override void restart()
        {
            //elemente boss-a restartam
            bossSpeed = 3;
            left = true;
            resetProjectile();

            //restartam nasljedjene elemente
            tracer = new Color();
            revive();
            reset_position();

            figure.Visible = false;
        }


        public void resetProjectile() {
            projectil.reset();
        }

        
        //--------------------------------------------------.KONTROLA SLIKE/FIGURE-A.-------------------------------------------------

        public override void paint(object sender, PaintEventArgs e)
        {
            figure.Visible = false;
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
        protected override void copyFigureInformation()
        {

            x = figure.Location.X;
            y = figure.Location.Y + 20; //+20 je da se slika lijepo prikazuje

            height = figure.Height;
            width = figure.Width;

            originalX = figure.Location.X;
            originalY = figure.Location.Y + 20; //+20 je da se slika lijepo prikazuje

            figure.Location = new Point(x, originalY);
        }



        //-------------------------------------STVARI VEZANE UZ PUCANJE/PROJEKTIL-------------

        public override void isHit()
        {
            life--;
            if (life <= 0)
            {
                alive = false;
                setVisibility(false);
                resetProjectile();
            }


        }

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


    }
}
