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
        private PictureBox figure;
        private int x, y;
        private int width, height;
        private int projectilSpeed;
        private Color tracer;
        private List<Bitmap> images;
        private int frameCounter;
        private bool fired;
        
        private int targetX, targetY;

        public Projectil()
        {
            images = new List<Bitmap>();
            tracer = new Color();
            projectilSpeed = 2;
            frameCounter = 0;
            fired = false;
        }

        public void addPictureBox(PictureBox figure)
        {
            this.figure = figure;
        }

        public void removePictureBox()
        {
            this.figure = null;
        }

        public int X
        {
            set { x = value; }
            get { return x; }
        }

        public int Y
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

        public int TargetX
        {
            set { targetX = value; }
            get { return targetX; }
        }

        public int TragetY
        {
            set { targetY = value; }
            get { return targetY; }
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

        public void paint(object sender, PaintEventArgs e)
        {
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
            height = figure.Height;
            width = figure.Width;
        }

        public void projectilTick(object sender, EventArgs e, Form1 form)
        { 
            //napisati kretanje
        }

        public void projectilClick(object sender, EventArgs e, Form1 form, int mousePostionX, int mousePositionY)
        {
            
        }

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
