using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    //klasa predstavlja menu; sadrzi gumbe; nasljeduje IImageControl
    class Menu : IImageControl
    {
        //lista gumba u menu
        List<MenuButton> buttons;
        //konstruktor
        public Menu()
        {
            buttons = new List<MenuButton>();
        }
        //dodaj picturebox; funckija zapravo dodaje novi gumb koji ce biti asociran sa 
        //pictureboxom koji je poslan kao argument
        public void addPictureBox(PictureBox figure)
        {
            buttons.Add(new MenuButton(figure));
        }
        //dodaje picturebox i sliku; funkcija zapravo dodaje novi gumb koji ce biti asociran sa
        //pictureboxom koji je poslan kao argument i njegova slika ce biti slika koja je
        //poslana kao argument
        public void addPictureBoxAndImage(PictureBox figure, Bitmap image)
        {
            buttons.Add(new MenuButton(figure, image));
        }
        //mice gumbe
        public void removePictureBox()
        {
            buttons = null;
        }
        //crta sve gumbove menua, tj crta sav menu
        public void menuPaint(object sender, PaintEventArgs e)
        {
            foreach (MenuButton b in buttons)
            {
                b.buttonPaint(sender, e);
            }
        }
        //dodaje sliku gumbu koji je zadnji dodan
        public void addImage(Bitmap image)
        {
            buttons[buttons.Count - 1].addImage(image);
        }
        //postavlja vidljivost svih gumbiju u ovom menuu 
        public void Visible(bool visible)
        {
            foreach (MenuButton b in buttons)
            {
                b.Visible(visible);
            }

        }

       
    }
}
