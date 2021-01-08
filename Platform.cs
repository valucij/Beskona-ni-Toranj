using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class Platform : IImageControl
    {
        //sluzi samo kao tocka vodilja kako treba nacrtati sliku
        private System.Windows.Forms.PictureBox platform;
        private Color tracer;
        private Bitmap image;
        private int x, y;
        private int width, height;
      
        public Platform()
        {
            tracer = new Color();
        }

        public void addPictureBox(System.Windows.Forms.PictureBox figure)
        {
            platform = figure;
        }

        public void removePictureBox()
        {
            platform = null;
        }

       public void platformPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);
        }

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
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
