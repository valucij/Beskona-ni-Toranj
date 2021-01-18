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
        //konstruktor
        public ProjectilShotByPlayer() : base() {
            x = 0;
            y = 0;
        }


        //vraća je li character pogođen, poziva se iz player.tick ili boss.tick
        public override bool hasHit(Form1 form)
        {
            //ako metak nije ispucan uopce, vraca false jer nista ne moze biti pogodjeno
            if (!fired) return false;

            //ako je metak ispucan
            foreach (Control c in form.Controls)
            {
                //ako pogodi neprijatelja/bossa baca true
                if ((string)c.Tag == "boss" && !form.bossIsDead() && form.bossIsVisible())
                {
                    if (figure.Bounds.IntersectsWith(c.Bounds))
                    {
                        Console.WriteLine("I hit the " + (string)c.Tag);
                        form.bossIsHit();
                        this.reset();
                        return true;
                    }
                }

                if ((string)c.Tag == "enemy" && !form.enemyIsDead() && form.EnemyIsVisible())
                {
                    if (figure.Bounds.IntersectsWith(c.Bounds))
                    {
                        Console.WriteLine("I hit the " + (string)c.Tag);
                        form.enemyIsHit();
                        this.reset();
                        return true;
                    }
                }

                //ako pogodi tlo, vraca false
                if (((string)c.Tag == "platform" || (string)c.Tag == "ground") && figure.Bounds.IntersectsWith(c.Bounds))
                {
                    this.reset();
                    return false;
                }

            }
            return false;
        }



    }
}
