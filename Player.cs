using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    //klasa nasljeduje klasu lika, i predstavlja playera
    class Player : Character
    {
        //ime playera
        private string name;
        //broj bodova koje ima player
        private int score;
        //brzina kojom se krece player
        private int jumpSpeed;
        //gravity koji dijeluje na player
        private int gravity;

        private bool goingLeft;
        private bool goingRight;
        private bool jumping;

        //konstruktor
        public Player()
        {
            name = "Jojo";
            score = 0;
            jumpSpeed = 7;
            gravity = 5;
            goingLeft = false;
            goingRight = false;
            jumping = false;
        }

        public int Score
        {
            set { score = value; }
            get { return score; }
        }

        public int Speed
        {
            set { jumpSpeed = value; }
            get { return jumpSpeed; }
        }

        public int Gravity
        {
            set { gravity = value; }
            get { return gravity; }
        }

        public string Name
        {
            get { return name; }
        }

        //funkcija koja obavlja sto se dogada s playerom kad se tipka pritisnuta, poziva se is form.keyDown
        public void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                goingLeft = true;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                goingRight = true;

            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                jumping = true;
            }
        }
        //funckija koja obavlja sto se dogada s playerom kad je tipka pustena, poziva se iz form.keyUp
        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                goingLeft = false;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                goingRight = false;

            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                jumping = false;
            }
        }
        //mozda treba? za animaciju kretanja, mozda dodati i kod ostalih charactera
        public void paint(object sender, PaintEventArgs e)
        { 
        }

        //timer tick mozda treba? mozda dodati i kod ostalih charactera
        public void timerTick(object sender, EventArgs e)
        { 

        }
    }
}
