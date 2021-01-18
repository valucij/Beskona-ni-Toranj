﻿using Microsoft.VisualBasic;
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
 * >funkcije za novcic
 * >stvari generirane form designerom
 */

//DOLJE::KOPIJA FORME S NESTAJUCIM PLATFOTMAMA

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
        //Coin koji se generira kada pobijedimo enemya
        Coin enemycoin;
        //Neprijatelj protiv kojeg se igrac bori
        Boss boss;
        //Coin koji se generira kada pobijedimo bossa, ovaj vrijedi vise bodova
        Coin bosscoin;

        //lista koja drzi sve platforme
        List<Platform> allPlatforms;
        //lista svih platformi koje ce biti potrebne za restart igre
        List<Platform> copyAllPlatforms;
        //objekt klase Platform za tlo
        Platform ground;

        //brzina kojom se krecu platforme
        int platformSpeed;
        //zastavica koja pokazuje da li se platforme micu ili ne
        bool platformMoving;
        //objekt klase menu
        Menu menu;

        private string playerName;

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

            //inicijaliziram Enemya s 1 zivotom
            enemy = new Enemy(1);

            //inicijaliziram bossa s 2 zivota
            boss = new Boss(2);

            //inicijaliziram novcice
            enemycoin = new Coin();
            bosscoin = new Coin(2);

            //inicijalizacija tla (klase platforma)
            ground = new Platform();

            //inicijaliziram brzinu igre (tj brzinu kojom platforme idu)
            platformSpeed =1;
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


            //kopije lista platformi
            copyAllPlatforms = new List<Platform>(allPlatforms);

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
            menu.addPictureBoxAndImage(menuButton_3, Properties.Resources.playerImage);
            menu.addPictureBoxAndImage(menuButton_4, Properties.Resources.highscoreImage);

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
            boss.addImage(Properties.Resources.bossPicture);

            //dodajem slike za enemya
            enemy.addPictureBox(enemyPictureBox);

            //dodajem slike za bosscoin
            bosscoin.addPictureBox(bosscoinPictureBox);

            //dodajem slike za enemycoin
            enemycoin.addPictureBox(enemycoinPictureBox);
            enemycoin.addImage(Properties.Resources.coin_star);

          //dodajem slike za projektil playera
             player.addProjectilPictureBox(projectilPictureBox);

            player.addProjectilImage(Properties.Resources.projectil_1);
            player.addProjectilImage(Properties.Resources.projectil_2);
            player.addProjectilImage(Properties.Resources.projectil_3);
            player.addProjectilImage(Properties.Resources.projectil_4);
            player.addProjectilImage(Properties.Resources.projectil_5);

            //dodajem slike za projektil bossa
            boss.addProjectilPictureBox(bossProjectilPictureBox);

            boss.addProjectilImage(Properties.Resources.projectil_1);
            boss.addProjectilImage(Properties.Resources.projectil_2);
            boss.addProjectilImage(Properties.Resources.projectil_3);
            boss.addProjectilImage(Properties.Resources.projectil_4);
            boss.addProjectilImage(Properties.Resources.projectil_5);


            //ovo obrisati, ovo je samo za testiranje, mijenjamo polazaj playera, samo za testiranje bossa
            player.X = 282;
            player.Y = 400;
            //ovdje zavrsava ono sto treba obrisati

            playerName = "default";

            //-------------------------------dodajem platformama boss i coin
            //VAZNO JE DA SE OVO RADI NAKON STO SVI OBJEKTI IMAJU PICTUREBOXOVE UCITANE
            allPlatforms[5].Add(boss, bosscoin);
            allPlatforms[8].Add(enemy, enemycoin);
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
                foreach (Platform p in allPlatforms)
                {
                    p.platformPaint(sender, e);
                }
                enemy.paint(sender, e);
                if (player.Score>20)boss.paint(sender, e);
                if (enemycoin.Dropped) enemycoin.paint(sender, e);
                if (bosscoin.Dropped) bosscoin.paint(sender, e);
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

        //akcije koje se obavljaju kad se klikne na gumb za unos imena playera u menuu; klik je dopusten
        //samo ako se nalazimo u menu modeu; otvara se prozor gdje unosimo ime lika
        private void playerClick(object sender, EventArgs e)
        {
            //Console.WriteLine("player name prije unosa: " + playerName);
            if (menuFlag)
            {
                string playerName = Interaction.InputBox("Unesite svoje ime!", "Player name input", "Player", 100, 100);
              //  Console.WriteLine(playerName);
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
                player.Tick(sender, e, this);
                platformTick();
            }


            Invalidate();//poziva Form1_Paint metodu

        }

        //---------------------------------------------TICKER za platformu-------------------------------------------------------------------------------------------
        private void platformTick()
        {
            if (player.offGround == true) {
                allPlatforms.Remove(ground);
            }

            //pomakni sve platforme prema "dolje"
            foreach (Platform p in allPlatforms)
            {
                //za prvo put: ako igrac skoci i platforme se jos ne micu, samo droppaj ground iz allPlatforms
                //i pocni kretati platforme
                if (player.Y < 200 && !player.offGround)
                { 
                    player.offGround = true;
                    platformMoving = true;
                }

                //ako micemo platformu, pomakni ju dolje
                if (platformMoving)  p.MoveDown(platformSpeed);

            }
        }

    //----------------------------------------------------------RESTART funkcije--------------------------------------------------------------------------------------------------------------------------

    //funkcija restartira igru, restartira playera i platforme
    private void restartGame()
        {
            //restartam charactere
             player.restart();

            //restartam igru
            platformRestart();

        }

        //funkcija restarta platforme na njihov pocetni polozaj
        private void platformRestart()
        {
            //stvaram novu listu svih platformi sa tlom
            List<Platform> tmp = new List<Platform>();
            tmp.Add(ground);

            foreach (Platform p in allPlatforms)
            {
                tmp.Add(p);
            }

            allPlatforms = tmp;

            //restartam svaku platformu
            foreach (Platform p in allPlatforms)
            {
                p.restart();
            }

            //postavljanje zastavice
            platformMoving = false;
            player.offGround = false;
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

        //-------------------------------------------NOVCIC FUNKCIJE-------------------------------

        public void hasPickedUp(int coinvalue) {
            player.PickUp(coinvalue);

        }

        public void dropBosscoin(){
            if (boss.isDead()) bosscoin.drop(boss.X, boss.Y);
        }

        public void dropEnemycoin()
        {
            if (enemy.isDead()) enemycoin.drop(enemy.X, enemy.Y);
        }

        //-------------------------------------------STVARI GENERIRANE FORM DESIGNEROM-------------
        private void highscoreClick(object sender, EventArgs e)
        {
            //treba napisati jos
        }

 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}

