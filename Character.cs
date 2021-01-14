using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Drawing

namespace Beskonačni_Toranj
{
    //klasa koja predstavlja lika, bio on player, neprijatelj ili glavni neprijatelj
    class Character : IImageControl
    {
        //predstavlja picturebox koji u windows formi predstavlja lika
        protected System.Windows.Forms.PictureBox figure;
        protected List<Bitmap> pictures;
        //dodavanje pictureboxa koji je stvoren u windows formi i predstavljat ce lika
        public void addPictureBox(System.Windows.Forms.PictureBox figure)
        {
            this.figure = figure;
        }

        //mice picturebox
        public void removePictureBox()
        {
            figure = null;
        }

        public void addImage(List<Bitmap> pictures)
        {
            this.pictures = pictures;
        }

        public void addImage(Bitmap picture)
        {
            if (this.pictures == null)
            {
                this.pictures = new List<Bitmap>();
            }
            this.pictures.Add(picture);
        }

       

    }
}
