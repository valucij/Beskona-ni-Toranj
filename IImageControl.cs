using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Beskonačni_Toranj
{
    interface IImageControl
    {
       
        //dodavanje pictureboxa koji je stvoren u windows formi i predstavljat ce lika
        void addPictureBox(System.Windows.Forms.PictureBox figure);

        //mice picturebox
        void removePictureBox();

    }
}
