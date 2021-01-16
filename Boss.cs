using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class Boss : Enemy
    {
        //brzina boss-a
        private int bossSpeed;
        //boja tracer objekta za testiranje
        Color tracer;
        //metak od neprijatelja
        ProjectilShotByBoss projectil;
        //jel na lijevoj ili desnoj strani ekrana sece
        private bool leftSide, rightSide;
        //jel se boss krece lijevo ili desno
        private bool left, right;

        Bitmap image;
        //potrebno za animaciju bossa
        /*
        //slike boss-a (lista je da generiramo kretanje)
        List<Bitmap> images;
        //counter koji broji koji frame da prikaze
        private int frameCounter;
        */

        private int originalX, originalY;


        public Boss()
        {
            tracer = new Color();
            projectil = new ProjectilShotByBoss();
            bossSpeed = 3;
            //leftSide = false;
           // rightSide = true;
            alive = true;

            left = true;
            right = false;

           /* images = new List<Bitmap>();
            frameCounter = 0;*/

            //mozda ne treba 
            //x = 571;
          //  y = 447;
            //figure.Location = new Point(x, y);
        }

        //funkcija koja updatea stanje projektila
       /* public void bossTickProjectil(object sender, EventArgs e, Form1 form)
        {
            //funkcija koja updatea projektil
            projectil.projectilTick(sender, e, form);

            //ako je projektil udario o charactera, vraca se informacija formi
            if(projectil.hasHit(form)) form.playerIsHit();
        }

        public void bossTickSides(object sender, EventArgs e, Form1 form)
        {
            if (leftSide)
            {
                rightSide = true;
                leftSide = false;
                projectil.Left = false;
                projectil.Right = true;
                
                changePosition();
            }
            else {
                leftSide = true;
                rightSide = false;
                projectil.Left = true;
                projectil.Right = false;
                
                changePosition();
            }

        }*/

        public void bossTickMovement(object sender, EventArgs e, Form1 form)
        {

            projectil.projectilTick(sender, e, form);
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

            /* if (rightSide && x > 700)
             {
                 right = false;
                 left = true;
             }
             else if (rightSide && x < 400)
             {
                 right = true;
                 left = false;
             }
             else if (leftSide && x < 20)
             {
                 right = true;
                 left = false;
             }
             else if (leftSide && x > 250)
             {
                 right = false;
                 left = true;
             }

             figure.Location = new Point(x, y);*/
        }

        public void paint(object sender, PaintEventArgs e)
        {
           // Bitmap walkFrame = returnFrame();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);

            projectil.paint(sender, e);

        }

        public override void addImage(Bitmap image)
        {
            this.image = image;
            copyFigureInformation();
        }

        //potrebno za animaciju bossa
        /*
        public override void addImage(List<Bitmap> images)
        {
            this.images = images;
            copyFigureInformation();

        }

        public override void addImage(Bitmap image)
        {
            this.images.Add(image);
            copyFigureInformation();
        }*/

        private void copyFigureInformation()
        {
            figure.Visible = false;

            x = figure.Location.X;
            y = figure.Location.Y + 20;

            height = figure.Height /*+ 70*/;
            width = figure.Width/* + 70*/;

            originalX = figure.Location.X;
            originalY = figure.Location.Y + 20;

            figure.Location = new Point(x, originalY);
        }
        /*
        private void updateFigureInformation()
        {
            figure.Location = new Point(x, y);
        }*/

        //potrebno za animaciju bossa
        /*
        private Bitmap returnFrame()
        {
            Bitmap returnValue = images[0];

            if (frameCounter < images.Count)
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
        }*/

        public void restart()
        {
            tracer = new Color();
           // projectil = new ProjectilShotByBoss(); MAKNUTI OVO, OVO NE!
            bossSpeed = 3;
            leftSide = false;
            rightSide = true;
            alive = true;

            left = true;
            right = false;

            /*images = new List<Bitmap>();
            frameCounter = 0;*/

            //ista pocetna pozicija
            /* x = 571;
             y = 447;*/
            //figure.Location = new Point(x, y);
            returnOriginalFigurePostion();

        }

        private void returnOriginalFigurePostion()
        {
            figure.Visible = false;

            x = originalX;
            y = originalY;

            figure.Location = new Point(originalX, originalY);
            
        }
        
        
      /*  private void changePosition()
        {
            if (leftSide)
            {
                x = 112;
                y = 441;
            }
            else
            {
                x = 571;
                y = 441;
            }

            figure.Location = new Point(x, y);

        }*/

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

      

    }
}
