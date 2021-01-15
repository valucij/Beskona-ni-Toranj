using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/* struktura forme
 * >podaci
 * >konstruktor
 * >funkcije za crtanje
 * >obrada pritiska tipke/klika miša
 * >glavni ticker za igru 
 * >ticker za bossa
 * >restart
 * >pucanje (isHit funkcije)
 */

namespace Beskonačni_Toranj
{
    public partial class Form1 : Form
    {
        //zastavice koje pokazuju u kojem je modeu igra; da li je to meni, da li je to igra, ili da
        //li je igra zavrsila
        bool menuFlag;
        bool gameFlag;
        bool endgameFlag;


        //player kojeg korisnik moze pokretati
        Player player;
        //Enemy obican
        Enemy enemy;
        //Neprijatelj protiv kojeg se igrac bori
        Boss boss;
        //lista koja drzi sve platforme
        List<Platform> allPlatforms;
        //lista svih platformi koje ce biti potrebne za restart igre
        List<Platform> copyAllPlatforms;
        //lista sadrzi platforme koje su samo u pojedinom trenutku vidljive na ekranu
        List<Platform> visiblePlatforms;
        //lista svih vidljivih platformi koje ce biti potrebne za restart igre
        List<Platform> copyVisiblePlatforms;
        //brzina kojom se krecu platforme
        int platformSpeed;
        //counter koji pazi koja se sljedeca platforma stavlja u one koje se vide na ekranu
        int putInVisiblePlatforms;
        //zastavica koja pokazuje da li se platforme micu ili ne
        bool platformMoving;
        //zastavica koja pokazuje je li tlo u ekranu
        private bool groundFlag;
        //platforma na kojoj se nalazu boss
        Platform bossPlatform;
        //objekt klase menu
        Menu menu;
        //objekt klase Platform za tlo
        Platform ground;
        //zastavica koja pokazuje je li boss u ekranu
        private bool bossFlag;

        //KONSTRUKTOR
        public Form1()
        {
            InitializeComponent();

            //zastavice za tok igre postavlja, stavlja nas an pocetni menu
            menuFlag = true;
            gameFlag = false;
            endgameFlag = false;

            //konstruiram meni
            menu = new Menu();

            //inicijalizacija playera
            player = new Player();

            //inicijaliziram bossa, njegovu platformu i flag
            boss = new Boss();
            bossPlatform = new Platform();
            bossFlag = false;

            //inicijalizacija tla (klase platforma)
            ground = new Platform();
            groundFlag = true;

            //inicijaliziram brzinu igre (tj brzinu kojom platforme idu)
            platformSpeed = 1;
            //nevidljivih platforma je 6 
            putInVisiblePlatforms = 6;
            //da li se platforme micu
            platformMoving = false;

            //inicijalizacija i postavljanje platformi
            allPlatforms = new List<Platform>();
           

            //allPlatforms.Add(new Platform(groundPictureBox, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(ground);
            allPlatforms.Add(new Platform(platformPictureBox_1, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(new Platform(platformPictureBox_2, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(new Platform(platformPictureBox_3, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(new Platform(platformPictureBox_4, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(new Platform(platformPictureBox_5, Properties.Resources.b_fotfor_lush2));

            //platforme su "iznad"
            platformPictureBox_6.Location = new Point(platformPictureBox_6.Location.X, -110);
            platformPictureBox_7.Location = new Point(platformPictureBox_7.Location.X, -210);
            platformPictureBox_8.Location = new Point(platformPictureBox_8.Location.X, -310);
            platformPictureBox_9.Location = new Point(platformPictureBox_9.Location.X, -410);

            //ne vide se ispocetka
            allPlatforms.Add(new Platform(platformPictureBox_6, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(new Platform(platformPictureBox_7, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(new Platform(platformPictureBox_8, Properties.Resources.b_fotfor_lush2));
            allPlatforms.Add(new Platform(platformPictureBox_9, Properties.Resources.b_fotfor_lush2));

            //dodaje platforme u listu vidljivih platformi
            visiblePlatforms = new List<Platform>();
            visiblePlatforms.Add(allPlatforms[0]);
            visiblePlatforms.Add(allPlatforms[1]);
            visiblePlatforms.Add(allPlatforms[2]);
            visiblePlatforms.Add(allPlatforms[3]);
            visiblePlatforms.Add(allPlatforms[4]);
            visiblePlatforms.Add(allPlatforms[5]);

            //kopije lista platformi
            copyAllPlatforms = new List<Platform>(allPlatforms);
            copyVisiblePlatforms = new List<Platform>(visiblePlatforms);

            //--------------------------------------dodavanje slika i grafike-----------------------
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //postavljanje boje pozadine
            this.BackColor = Color.Black;

            //postavljanje pozadinske slike
            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //dodajem slike za menu buttons 
            menu.addPictureBoxAndImage(menuButton_1, Properties.Resources.startImage);
            menu.addPictureBoxAndImage(menuButton_2, Properties.Resources.quitImage);

            //dodajem slike za tlo
            ground.addPictureBox(groundPictureBox);
            ground.addImage(Properties.Resources.b_fotfor_lush2);

            //ucitavanje slika za playera
            List<Bitmap> playerImages = new List<Bitmap>();
            playerImages.Add(Properties.Resources.stand);
            playerImages.Add(Properties.Resources.walkLeft_1);
            playerImages.Add(Properties.Resources.walkLeft_2);
            playerImages.Add(Properties.Resources.walkLeft_3);
            playerImages.Add(Properties.Resources.walkRight_1);
            playerImages.Add(Properties.Resources.walkRight_2);
            playerImages.Add(Properties.Resources.walkRight_3);

            //postavljanje slika za playera
            player.addPictureBox(playerPictureBox);
            player.addImage(Properties.Resources.stand);
            player.addImage(playerImages);

            //dodajem slike za bossa
            boss.addPictureBox(bossPictureBox);


            //dodajem slike za projektil playera
            player.addProjectilPictureBox(projectilPictureBox);

            player.addProjectilImage(Properties.Resources.projectil_1);
            player.addProjectilImage(Properties.Resources.projectil_2);
            player.addProjectilImage(Properties.Resources.projectil_3);
            player.addProjectilImage(Properties.Resources.projectil_4);
            player.addProjectilImage(Properties.Resources.projectil_5);

            //dodajem slike za projektil bossa
            boss.addProjectilPictureBox(bossProjectilPictureBox);

            player.addProjectilImage(Properties.Resources.projectil_1);
            player.addProjectilImage(Properties.Resources.projectil_2);
            player.addProjectilImage(Properties.Resources.projectil_3);
            player.addProjectilImage(Properties.Resources.projectil_4);
            player.addProjectilImage(Properties.Resources.projectil_5);


        }
        //------------------------------------------------CRTANJE---------------------------------------------------------------------------------------------------------------------------------------------------
        //crtanje objekata u formi; sve se salje u odgovarajuce funckije clanova forme (playeri i platforme)
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (menuFlag)
            {
                menu.menuPaint(sender, e);
            }
            else if (gameFlag)
            {
                player.paint(sender, e);
                foreach (Platform p in visiblePlatforms)
                {
                    p.platformPaint(sender, e);
                }
            }
            else if (endgameFlag)
            {
                menu.menuPaint(sender, e);
            }

        }

        //------------------------------------------------OBRADA PRITISKA TIPKI / KLIKA MIŠA-----------------------------------------------------------------------------------------------------------------
        //akcije koje se obavljaju kad se pritisne neka tipka; sve se salje u odgovarajuce funkcije kod playera
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameFlag)
            {
                player.keyDown(sender, e);
            }
            
        }

        //akcije koje se obavljaju kad se otpusti neka tipka; sve se salje u odgovarajuce funkcije kod playera
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (gameFlag)
            {
                player.keyUp(sender, e);
            }
            
        }

        //forma javlja playeru da je pritisnut key za pucanje 
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            player.keyPress(sender, e);
        }

        //akcije koje se obavljaju kad se klikne na gumb za start u menuu; klik je dopusten
        //samo ako se nalazimo u menu modeu
        private void startClick(object sender, EventArgs e)
        {
            if (menuFlag)
            {
                menuFlag = false;
                gameFlag = true;
                endgameFlag = false;
                menu.Visible(false);
            }
        }


        //akcije koje se obavljaju kad se klikne na gumb za quit u menuu; klik je dopusten
        //samo ako se nalazimo u menu modeu; aplikacije se gasi
        private void quitClick(object sender, EventArgs e)
        {
            if (menuFlag)
            {
                this.Close();
            }
        }

        // Projectil p;
        private void Form1_Click(object sender, EventArgs e)
        {

            if (gameFlag)
            {
                //Console.WriteLine("tu");
                // player.click(sender, e, this, MousePosition.X, MousePosition.Y);
            }

        }

        //---------------------------------------------------GLAVNI TICKER za igru-------------------------------------------------------------------------------------

        //akcije koje se obavljaju kako vrijeme tece; sve se salje u odgovarajuce funkcije clanova forme (playera i platforme)
        //na kraju se poziva funkcija koja crta objekte 
        private void gameTimer_Tick(object sender, EventArgs e)
        {

            if (!player.Alive)
            {
                endgameFlag = true;

                // menuFlag = false; OVO TREBA ODKOMENTIRATI KAD SE NAPRAVI ENDGAME MODE
                menuFlag = true;//OVO OBRISATI KAD SE NAPRAVI ENDGAME MODE
                menu.Visible(true);//OVO OBRISATI KAD SE NAPRAVI ENDGAME MODE
                gameFlag = false;
                restartGame();
            }

            if (gameFlag)
            {
                player.timerTick(sender, e, this);
                platformTick();
            }
            //mozda i tu treba provjeriti je li player ziv?
            Invalidate();//poziva Form1_Paint metodu

        }

        //---------------------------------------------TICKER za platformu-------------------------------------------------------------------------------------------
        private void platformTick()
        {

            //provjeri koja je platforma "izasla" iz prozora, makni ju ih vidljivih platformi,
            //i stavi novu platformu u prozor, tj medu vidljive platforme
            for (int i = 0; i < allPlatforms.Count; i++)
            {

                if (allPlatforms[i].Y > 490)
                {
                    visiblePlatforms.Remove(allPlatforms[i]);
                    allPlatforms[i].Y = -410;//postavi kao zadnju koja ce doci prema dolje

                    if (i + 1 == allPlatforms.Count)
                    {
                        if (groundFlag)
                        {
                            visiblePlatforms.Add(allPlatforms[1]);//na 0 se nalazi ground
                        }
                        else
                        {
                            visiblePlatforms.Add(allPlatforms[0]);//izbacen je ground
                        }

                    }
                    else
                    {
                        visiblePlatforms.Add(allPlatforms[putInVisiblePlatforms]);

                        if (putInVisiblePlatforms + 1 == allPlatforms.Count)
                        {
                            putInVisiblePlatforms = 0;//oke je jer se remova ground
                        }
                        else
                        {
                            putInVisiblePlatforms++;
                        }
                    }
                    if (groundFlag)
                    {
                        allPlatforms.Remove(ground);
                        groundFlag = false;
                        putInVisiblePlatforms--;
                    }

                }
            }

            //pomakni sve platforme prema "dolje"
            foreach (Platform p in allPlatforms)
            {
                if (player.Y < 200 && !platformMoving)
                {
                    platformMoving = true;
                }
                if (platformMoving)
                {
                    p.Y += platformSpeed;
                }

            }
        }


        //-------------------------------------------------TICKERI za bossa--------------------------------------------------------------------------------------------------------------------

        private void bossTickProjectilFunction(object sender, EventArgs e)
        {
            boss.bossTickProjectil(sender, e, this);
        }

        private void bossTickSides(object sender, EventArgs e)
        {
            boss.bossTickSides(sender, e, this);
        }

        private void bossTickMovementFunction(object sender, EventArgs e)
        {
            boss.bossTickMovement(sender, e, this);
        }


    //----------------------------------------------------------RESTART funkcije--------------------------------------------------------------------------------------------------------------------------

    //funkcija restartira igru, restartira playera i platforme
    private void restartGame()
        {
            //restartam charactere
             player.restart();
             boss.restart();

            //restartam igru
            platformRestart();

            bossFlag = false;
        }

        //funkcija restarta platforme na njihov pocetni polozaj
        private void platformRestart()
        {
            List<Platform> tmp = new List<Platform>();
            tmp.Add(ground);

            foreach (Platform p in allPlatforms)
            {
                tmp.Add(p);
            }

            allPlatforms = tmp;
            groundFlag = true;


            foreach (Platform p in allPlatforms)
            {
                p.restart();
            }

            //ponovno postavljanje pocetnih vidljivih platformi
            visiblePlatforms = null;
            visiblePlatforms = new List<Platform>();
            visiblePlatforms.Add(allPlatforms[0]);
            visiblePlatforms.Add(allPlatforms[1]);
            visiblePlatforms.Add(allPlatforms[2]);
            visiblePlatforms.Add(allPlatforms[3]);
            visiblePlatforms.Add(allPlatforms[4]);
            visiblePlatforms.Add(allPlatforms[5]);
            visiblePlatforms.Add(allPlatforms[6]);
            visiblePlatforms.Add(allPlatforms[7]);

            putInVisiblePlatforms = 6;
            platformMoving = false;
        }


        //---------------------------------PUCANJE-----------------------------------------------------------------------------------------------------------------------------

        public void playerIsHit() 
        {
            player.isHit();
        }

        public void bossIsHit()
        {
            boss.isHit();
        }

        public void enemyIsHit() 
        {

            enemy.isHit();
        
        }

        private void bossProjectilPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
