using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class Boss : Character
    {
        private int x, y;
        private int width, height;
        private int life;
        private int bossSpeed;
        Color tracer;
        Projectil projectil;

        private bool fired;
        private bool leftSide, rightSide;
        private bool left, right;

        List<Bitmap> images;
        private int frameCounter;

        public Boss()
        {
            tracer = new Color();
            projectil = new Projectil();
            fired = false;
            bossSpeed = 3;
            leftSide = false;
            rightSide = true;

            images = new List<Bitmap>();
            frameCounter = 0;

            //mozda ne treba 
            x = 571;
            y = 447;
            //figure.Location = new Point(x, y);
        }

        public int X
        {
            set { x = value; figure.Location = new Point(value, figure.Location.Y); }
            get { return x; }
        }
        
        public int Y
        {
            set { y = value; figure.Location = new Point(figure.Location.X, value); }
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

        public void bossTickProjectil(object sender, EventArgs e, Form1 form)
        {
            projectil.Fired = true;
            projectil.projectilTick(sender, e, form);
            projectil.isHit(form);
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

        }

        public void bossTickMovement(object sender, EventArgs e, Form1 form)
        {
            if (left)
            {
                x -= bossSpeed;
            }
            else {
                x += bossSpeed;
            }

            if (rightSide && x > 700)
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

            figure.Location = new Point(x, y); 
        }

        public void paint(object sender, PaintEventArgs e)
        {
            Bitmap walkFrame = returnFrame();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(walkFrame, x, y, width, height);

            projectil.paint(sender, e);

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
            figure.Visible = false;
            height = figure.Height /*+ 70*/;
            width = figure.Width/* + 70*/;
        }

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
        }

        private void changePosition()
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
