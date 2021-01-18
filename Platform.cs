using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{   
    //klasa predstavlja platforme na koje player moze skakati; nasljeduje klasu IImageControl
    class Platform : IImageControl
    {
        //sluzi samo kao tocka vodilja kako treba nacrtati sliku
        private System.Windows.Forms.PictureBox platform;
        //sluzi kako bi pozadina slike platforme bila transparentna
        private Color tracer;
        //slika platforme, ono sto se crta
        private Bitmap image;
        //informacije o tome gdje se nalazi platforma s obzirom na formu
        private int x, y;
        //informacije o velicini platforme
        private int width, height;
        //originalne informacije, informacije o platformi pri stvaranju
        private int originalX, originalY;
        private int originalWidth, originalHeight;
        private int originalPlatformType;
        //pointer na enemya koji mozebitno sjedi na platformi
       Enemy enemy;
        //pointer na bossa koji mozda sjedi na platformi;
       Boss boss;
        //pointer na coin
        Coin coin;

        //FLAG KOJI OZNACAVA TIP PLATFORME, DA SE NE ZEZAMO S DODATNIM PROVJERAMA
        //0 = PLATFORMA BEZ ICEGA
        //1 = PLATFORMA S ENEMYEM KOJI EVENTUALNO AKO GA SE UPUCA DROPPA COIN
        //2 = PLATFORMA S BOSSOM KOJI EVENTUALNO KAD GA SE UPUCA DROPPA COIN
        //3 = PLATFORMA S COINOM KOJI JE OSTAO NAKON KAJ SMO PROPUCALI NEKOGA
        private int platformType;

        //konstruktor koji stvara obicnu platformu 
        public Platform() {
            tracer = new Color();
            enemy = null;
            boss = null;
            coin = null;
           

            platformType = 0;
            originalPlatformType = 0;

            //da mi ne baca exception
            x = 0;
            y = 0;
            height = 1;
            width = 2;

        }

        //konstruktor koji stvara platformu sa grafikom
        public Platform(System.Windows.Forms.PictureBox figure, Bitmap image)
        {
            tracer = new Color();
            platform = figure;
            addImage(image);
            enemy = null;
            boss = null;
            coin = null;

            platformType = 0;
            originalPlatformType = 0;
        }

        //konstruktor koji stvara platformu

        //-------------------------------------.LOGISTIKA PLATFORME.--------------------------
        //funkcija koja updatea platformType
        public void Tick_X(object sender, EventArgs e, Form1 form)
        {
            //ako nema nicega
            if (platformType == 0) return;

            //ako je enemy na platformi 
            if (platformType == 1) {

                //updateaj enemya
                enemy.Tick(sender, e, form);

                //ako je neprijatelja ubio player, droppaj coin
                if (enemy.isDead())
                {
                    coin.drop(enemy.X, enemy.Y);
                    platformType = 3;
                }
            }

            //ako je boss na platformi
            if (platformType == 2)
            {

                //updateaj bossa
                boss.Tick(sender, e, form);

                //ako je boss mrtav, droppaj coin
                if (boss.isDead())
                 {
                    coin.drop(boss.X, boss.Y);
                    platformType = 3;
                  }
            }

            //ako je coin na platformi (mozda je i droppan u gornjem if)
            if (platformType == 3) {
                //updateaj coin
                coin.Tick(sender, e, form);

                if (coin.isPickedUp()) {
                    platformType_restart();
                }
               
            };

            //ako je izvan ekrana, samo ju lupi na dno i resetiraj platfromtypw
            if (Y > 490) {
                Y = -410;
                platformType_restart();
            }

        }

        //funkcija koja pomice platformu
        public void MoveDown(int platformSpeed) {
            Y += platformSpeed;

            if (Y > 490)   Y = -410;

            //ako nema nicega
            if (platformType == 0) return;

            //ako je enemy na platformi
            if (platformType == 1)  enemy.MoveDown(platformSpeed); 

            //ako je boss na platformi
            if (platformType ==2 ) boss.MoveDown(platformSpeed);

            //ako je enemy na platformi
            if (platformType == 3 ) coin.MoveDown(platformSpeed);
        }



        //funkcija resetira platformu na pocetnu poziciju
        public void restart()
        {
            x = originalX;
            y = originalY;
            width = originalWidth;
            height = originalHeight;

            //jako bitno je i resetirati picturebox koji je dodjeljen platformi,
            //jer po njemu se sve orijentira 
            platform.Location = new Point(originalX, originalY);

            //restarta content platforme 
            platformType_restart();

        }

        //funkcija koja vraca platformu na originalne vrijednosti
        public void platformType_restart()
        {
            if (originalPlatformType == 0) ClearPlatform();

            if (originalPlatformType == 1) {
                enemy.revive();
                enemy.setLocation(x, y, width);
                coin.reset();
                platformType = 1;
            }

            if (originalPlatformType == 2)
            {
                boss.revive();
                boss.setLocation(x, y, width);
                coin.reset();
                platformType = 2;
            }

        }
        //-------------------------------------ADD BOSS/COIN/ENEMY------------

        //jel vrste 0
        public bool IsPlatformEmpty() {
            if (platformType==0) return true;
            return false;
        }

        //dodaje enemya s coinom
        public void Add(Enemy e, Coin c)
        {
            if (!this.IsPlatformEmpty()) return; 
           
            enemy = e;
            coin = c;
            platformType = 1;
            originalPlatformType = 1;

            //postavi lokaciju
            enemy.setLocation(x, y, width);
        }

        //dodaje bossa s njegovim coinom
        public void Add(Boss b, Coin c) {
            if (!this.IsPlatformEmpty()) return;

            boss = b;
            coin = c;
            platformType = 2;
            originalPlatformType = 2;

            //postavi lokaciju
            boss.setLocation(x, y, width);
        }

        //vraca platformu na tip 0
        public void ClearPlatform() {
            enemy = null;
            boss = null;
            coin = null;

            platformType = 0;
            originalPlatformType = 0;
        }


        //--------------------------------------------GRAFIKA---------------------------------

        //dodjeljuje klasi odgovarajuci picture box, kako bi lakse bilo crtati sliku platforme bez nagadanja gdje
        //bi se trebala nalaziti u formi
        public void addPictureBox(System.Windows.Forms.PictureBox figure )
        {
            platform = figure;
        }

        //micanje dodjeljenog picture boxa
        public void removePictureBox()
        {
            platform = null;
        }

        //crtanje platforme; poziva se iz Form1.Form1_Paint
        public void platformPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, x, y, width, height);
        }

        //dodaje se slika platforme; odmah se radi da je pozadina transparetna
        public void addImage(Bitmap image)
        {
            this.image = image;
            tracer = image.GetPixel(1, 1);
            this.image.MakeTransparent(tracer);

            //uzimamo sve informacije iz pictureboxa, kako bi znali crtati
            platform.Visible = false;

            x = platform.Location.X;
            y = platform.Location.Y;

            height = platform.Height;
            width = platform.Width;

            originalHeight = height;
            originalWidth = width;
            originalX = x;
            originalY = y;
        }

        //--------------------------------------------SVOJSTVA PLATFORME--------------------

        public int X
        {
            get { return x; }
            set { x = value; platform.Location = new Point(value, y); }
        }

        public int Y
        {
            get { return y; }
            set { y = value; platform.Location = new Point(x, value); }
        }

        public int OriginalX
        {
            set { originalX = value; }
            get { return originalX; }
        }

        public int OriginalY
        {
            set { originalY = value; }
            get { return originalY; }
        }

        public int OriginalHeight
        {
            set { originalHeight = value; }
            get { return originalHeight; }
        }
        public int OriginalWidth
        {
            set { originalWidth= value; }
            get { return originalWidth; }
        }
    }
}
