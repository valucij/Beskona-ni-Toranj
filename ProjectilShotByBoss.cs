using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    class ProjectilShotByBoss:Projectil
    {
        public ProjectilShotByBoss() :base () {
            x = 0;
            y = 0;
        }

        public override bool hasHit(Form1 form)
        {
            //ako metak nije ispucan vrati false
            if (!fired)  return false;

            //ako je metak ispucan
            foreach (Control c in form.Controls)
            {
                //ako boss pogodi platformu ili tlo, vraca false
                if (((string)c.Tag == "platform" || (string)c.Tag == "ground") && figure.Bounds.IntersectsWith(c.Bounds))
                {

                        this.reset();
                        return false;
                }

                //ako boss pogodi playera, vraca true
                if ((string)c.Tag == "player" && figure.Bounds.IntersectsWith(c.Bounds))
                {
                        this.reset();
                        return false;
                }

            }

            //inace vrati false
            return false;
        }

    }
}
