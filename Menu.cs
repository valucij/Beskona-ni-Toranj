using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class Menu : IImageControl
    {
        List<MenuButton> buttons;

        public Menu()
        {
            buttons = new List<MenuButton>();
        }
        public void addPictureBox(PictureBox figure)
        {
            buttons.Add(new MenuButton(figure));
        }

        public void addPictureBoxAndImage(PictureBox figure, Bitmap image)
        {
            buttons.Add(new MenuButton(figure, image));
        }

        public void removePictureBox()
        {
            buttons = null;
        }

        public void menuPaint(object sender, PaintEventArgs e)
        {
            foreach (MenuButton b in buttons)
            {
                b.buttonPaint(sender, e);
            }
        }

        public void addImage(Bitmap image)
        {
            buttons[buttons.Count - 1].addImage(image);
        }

        public void Visible(bool visible)
        {
            foreach (MenuButton b in buttons)
            {
                b.Visible(visible);
            }

        }

       
    }
}
