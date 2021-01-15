using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class ProjectilShotByPlayer:Projectil
    {
        //vraća je li character pogođen, poziva se iz player.tick ili boss.tick
        public override bool isHit(Form1 form)
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

            }
            return false;
        }



    }
}
