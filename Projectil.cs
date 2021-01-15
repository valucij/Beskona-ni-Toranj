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
        protected int x, y;
        protected int width, height;
        protected int projectilSpeed;
        protected Color tracer;
        protected List<Bitmap> images;
        protected int frameCounter;
        //zastavica unutar ove klase kako bi se kontroliralo crtanje projektila
        protected bool fired;
        protected bool left, right;

        public Projectil()
        {
            images = new List<Bitmap>();
            tracer = new Color();
            projectilSpeed = 15;
            frameCounter = 0;
            fired = false;
            left = false;
            right = false;
          
        }

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


        //------------------------------------------------svojstva--------------------------------------------------
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


  
        public void paint(object sender, PaintEventArgs e)
        {
            if (!fired)
            {
                return;
            }

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
            figure.Visible = false;
            height = figure.Height + 70;
            width = figure.Width + 70;
        }

        public void projectilTick(object sender, EventArgs e, Form1 form)
        {

            if (!fired)
            {
                return;
            }

            if (right)
            {
                x += projectilSpeed;
            }else if (left)
            {        
                x -= projectilSpeed;
            }


                figure.Location = new Point(x, y);

                if (x + width > form.ClientSize.Width || x < 0)
                {
                    left = false;
                    right = false;
                    fired = false;
                }

                if (y + height > form.ClientSize.Height || y < 0)
                {
                    left = false;
                    right = false;
                    fired = false;
                }

        }


        //vraća je li character pogođen, poziva se iz player.tick ili boss.tick
        public virtual bool isHit(Form1 form)
        {
            if (!fired)
            {
                return false;
            }
            foreach (Control c in form.Controls)
            {
                if ((string)c.Tag == "enemy" || (string)c.Tag == "boss" /*|| c.Tag == "player"*/)
                {
                    if (figure.Bounds.IntersectsWith(c.Bounds) && fired)
                    {
                        right = false;
                        left = false;
                        fired = false;
                        return true;
                    }
                }

                if ((string)c.Tag == "platform" || (string)c.Tag == "ground")
                {
                    if (figure.Bounds.IntersectsWith(c.Bounds) && fired)
                    {
                        right = false;
                        left = false;
                        fired = false;
                        return false;//vraca false, jer iako je pogodeno, nije neprijatelj pogoden, a to nas jedino zanima
                    }
                    
                }

                if ((string)c.Tag == "player")
                {
                    if (figure.Bounds.IntersectsWith(c.Bounds) && fired)
                    {
                        right = false;
                        left = false;
                        fired = false;
                        return false;//vraca false
                    }
                }
               
            }
            return false;
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
    }
}
