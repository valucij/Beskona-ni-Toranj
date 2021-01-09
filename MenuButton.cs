using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class MenuButton : IImageControl
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private Color tracer;
        Bitmap image;
        PictureBox button;

        public MenuButton()
        {
            tracer = new Color();
        }

        public MenuButton(PictureBox figure, Bitmap image)
        {
            tracer = new Color();
            button = figure;
            addImage(image);
        }

        public MenuButton(PictureBox figure)
        {
            tracer = new Color();
            button = figure;
        }

        public void addPictureBox(PictureBox figure)
        {
            button = figure;
        }

        public void removePictureBox()
        {
            button = null;
        }

        internal void buttonPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);

        }

        internal void Visible(bool visible)
        {
            button.Visible = visible;
        }

        public void addImage(Bitmap image)
        {
            this.image = image;
            tracer = image.GetPixel(1, 1);
            this.image.MakeTransparent(tracer);

            //ako se ovo odkomentira, nece se moci kliknuti na gumb
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
