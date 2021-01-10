using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    //klasa nasljeduje klasu lika, i predstavlja playera kojim korisnik moze upravljati
    class Player : Character
    {
        //ime playera; nebitno za igru
        private string name;
        //broj bodova koje ima player
        private int score;
        //brzina kojom player skace (kad skace)
        private int jumpSpeed;
        //gravity koji dijeluje na player, tj vise neka "snaga skakanja"
        private int force;
        //brzina kojom se krece lik dok hoda
        private int figureSpeed;

        //zastavice koje pokazuju da li player hoda ulijevo, udesno, ili da li skace
        private bool goingLeft;
        private bool goingRight;
        private bool jumping;

        //informacije oko toga gdje se player nalazi u prostoru
        private int x, y;
        //informacije oko velicine playera
        private int width, height;
        //element koji nam sluzi kako bi rubove slika playera ucinili transparentnim
        private Color tracer;

        //nepotrebno; buduci da imamo vise slike playera koje cine animaciju playera dok hoda
        Bitmap image;
        
        //counter za slike ako player hoda ulijevo
        int leftWalkFrameCounter;
        //counter za slike ako player hoda udesno
        int rightWalkFrameCounter;
        //slika playera dok stoji
        Bitmap stand;
        //slike playera dok hoda ulijevo
        List<Bitmap> leftWalks;
        //slike playera dok hoda udesno
        List<Bitmap> rightWalks;
        //zastavica koja govori da li je player ziv ili ne
        bool alive;
        //originalne informacije
        private int originalX;
        private int originalY;

        
        //konstruktor
        public Player()
        {
            name = "Jojo";
            score = 0;
            jumpSpeed = 10;
            figureSpeed = 10;
            force = 8;
            goingLeft = false;
            goingRight = false;
            jumping = false;
            tracer = new Color();
            leftWalkFrameCounter = 0;
            rightWalkFrameCounter = 0;
            leftWalks = new List<Bitmap>();
            rightWalks = new List<Bitmap>();
            alive = true;
        }

        public int Score
        {
            set { score = value; }
            get { return score; }
        }

        public int JumpSpeed
        {
            set { jumpSpeed = value; }
            get { return jumpSpeed; }
        }

        public int Force
        {
            set { force = value; }
            get { return force; }
        }

        public string Name
        {
            get { return name; }
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

        public bool Alive
        {
            set { alive = value; }
            get { return alive; }
        }
        //funkcija koja obavlja sto se dogada s playerom kad se tipka pritisnuta, poziva se is Form1.Form1_KeyDown
        //za kretanje ulijevo -> A / strelica ulijevo
        //za kretanje udesno -> D / strelica udesno
        //za skakanje -> W / strelica gore
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
            else if ( (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && !jumping)
            {
                jumping = true;
            }
        }
        //funckija koja obavlja sto se dogada s playerom kad je tipka pustena, poziva se iz Form1.Form1_KeyUp
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
        //funkcija koja crta playera; poziva se iz Form1.Form1_Paint; na pocetku funckije se odreduje koji ce se
        //walking frame crtati, tj koja ce se slika koja predstavlja playera crtati. To je odredeno time da li
        //se player krece prema desno, lijevo ili da li skace/stoji
        //Napisane su Smoothingmode, interpolationmode i pixeloffsetmode kako bi se sto lijepse resizeala slika
        //napomena, ne crta se picturebox, vec se crtaju slike playera
        public void paint(object sender, PaintEventArgs e)
        {
            Bitmap walkFrame = returnCurrentWalkFrame();
            
             e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
             e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
             e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
             e.Graphics.DrawImage(walkFrame, x, y, width, height);
           
        }

        //akcije koje se obavljaju kako vrijeme tece; tu se obavlja pokretanje playera, detekcija da li player dotice 
        //platforme i ostalo; sve se obavlja na pictureboxu koji je stvoren u formi, tek se onda to prebacuje u informacije
        //koje se ticu bas playera; kad se god u komentarima sljedecim pojavljuje rijec "player" misli se zapravo na 
        //picture box;
        public void timerTick(object sender, EventArgs e, Form1 form)
        {

            if(figure.Location.Y > 500)
            {
                alive = false;
                //return;
            }
            //pomakni playera gore ili dolje, ovisno o jumpSpeedu
            figure.Top += jumpSpeed;

            //zaustavi skakanje ako je dosao na kraj skakanja (skakanje je true, ali vise "nema snage skakanja")
            if (jumping && force < 0)
            {
                jumping = false;
            }
            //ako je u procesu skakanja, smanjuj force ("snagu skakanja"), tj polako zaustavljaj skakanje;
            //i je jumpSpeed manje od 0
            if (jumping)
            {
                force -= 1;
                jumpSpeed = -12;
            }
            else {
                jumpSpeed = 12;//ako jednostavno nije u procesu skakanja
            }

            //dokle player moze ici ulijevo, neka ide ulijevo
            if (goingLeft && figure.Left > 10)
            {
                figure.Left -= figureSpeed;
            }//dokle player moze ici udesno, neka ide udesno
            else if (goingRight && figure.Left + (figure.Width + 10) < form.ClientSize.Width)
            {
                figure.Left += figureSpeed;
            }

            foreach (Control x in form.Controls)
            {
                if (x.Tag == "platform" || x.Tag == "ground") 
                {
                   
                    if (figure.Bounds.IntersectsWith(x.Bounds) && !jumping && x.Top > figure.Top)//detekcija da li player stoji na platformi
                    {
                        force = 8;
                        figure.Top = x.Top - figure.Height + 26;
                        jumpSpeed = 0;
                        score++;
                    }
                }
            }
            //uzmi informacije iz pictureboxa, kako bi znali nacrtati playera
            x = figure.Location.X;
            y = figure.Location.Y;
            width = figure.Width;
            height = figure.Height;
            
        }
        //dodaj sliku playera; za playera nepotreba funkcija ako se zeli napraviti animacija kretanja
        public void addImage(Bitmap image)
        {
            this.image = image;
            tracer = image.GetPixel(1, 1);
            this.image.MakeTransparent(tracer);

            //uzimamo sve informacije iz pictureboxa, kako bi znali crtati; pritom moramo staviti da je picturebox 
            //nevidljiv kako se on ne bi crtao u formi
            figure.Visible = false;

            x = figure.Location.X;
            y = figure.Location.Y;

            height = figure.Height;
            width = figure.Width;

            originalX = x;
            originalY = y;
        }

        //dodaj razne slike za playera, kako bi mogli napraviti animaciju;
        //slike treba rasporediti u slike za stajanje, hodanje ulijevo i udesno
        public void addImage(List<Bitmap> images)
        {
            stand = images[0];
            leftWalks.Add(images[1]);
            leftWalks.Add(images[2]);
            leftWalks.Add(images[3]);
            rightWalks.Add(images[4]);
            rightWalks.Add(images[5]);
            rightWalks.Add(images[6]);
        }

        //vrati odgovarajucu sliku/walk frame za playera s obzirom na to da li player stoji/skace
        // da li hoda ulijevo ili udesno
        private Bitmap returnCurrentWalkFrame()
        {
            Bitmap returnValue;
            //ako hoda ulijevo, vrati sljedecu sliku za hodanje ulijevo i povecaj counter
            if (goingLeft && leftWalkFrameCounter < leftWalks.Count)
            {
                returnValue = leftWalks[leftWalkFrameCounter];
                leftWalkFrameCounter++;
            }
            else if (goingLeft)//resetiraj counter ako si dosao do zadnje slike za kretanje ulijevo
            {
                leftWalkFrameCounter = 0;
                returnValue = leftWalks[leftWalkFrameCounter];
            }
            else if (goingRight && rightWalkFrameCounter < rightWalks.Count)//analogno za hodanje udesno
            {
                returnValue = rightWalks[rightWalkFrameCounter];
                rightWalkFrameCounter++;
            }
            else if (goingRight)
            {
                rightWalkFrameCounter = 0;
                returnValue = rightWalks[rightWalkFrameCounter];
            }
            else //ako stoji/skace, jednostavno vrati tu sliku za stajanje
            {
                returnValue = stand;
            }

            //ucini da pozadina slike bude transparentna, tako da se vidi samo player, ne i pozadina slike
            tracer = returnValue.GetPixel(1, 1);
            returnValue.MakeTransparent(tracer);
            
            return returnValue;
        }
        //funkcija resetira playera; postavlja sve informacije na one originalne
        public void restart()
        {
            score = 0;
            jumpSpeed = 10;
            figureSpeed = 10;
            force = 8;
            goingLeft = false;
            goingRight = false;
            jumping = false;
            leftWalkFrameCounter = 0;
            rightWalkFrameCounter = 0;       
            alive = true;
            figure.Location = new Point(100, 350);

            x = originalX;
            y = originalY;

        }

    }
}
