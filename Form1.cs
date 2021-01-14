﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    public partial class Form1 : Form
    {
        //player kojeg korisnik moze pokretati
        Player player;
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
        //zastavice koje pokazuju u kojem je modeu igra; da li je to meni, da li je to igra, ili da
        //li je igra zavrsila
        bool menuFlag;
        bool gameFlag;
        bool endgameFlag;

        Menu menu;

        Platform ground;
        private bool groundFlag;


        Boss boss;
        Platform bossPlatform;
        private bool bossFlag;
        public Form1()
        {
            InitializeComponent();
          
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //ucitavanje slika za playera
            List<Bitmap> playerImages = new List<Bitmap>();
            playerImages.Add(Properties.Resources.stand);
            playerImages.Add(Properties.Resources.walkLeft_1);
            playerImages.Add(Properties.Resources.walkLeft_2);
            playerImages.Add(Properties.Resources.walkLeft_3);
            playerImages.Add(Properties.Resources.walkRight_1);
            playerImages.Add(Properties.Resources.walkRight_2);
            playerImages.Add(Properties.Resources.walkRight_3);
            //inicijalizacija playera, postavljanje pictureboxa i slika
            player = new Player();
            player.addPictureBox(playerPictureBox);
            player.addImage(Properties.Resources.stand);
            player.addImage(playerImages);

            //postavljanje pozadinske slike
            //this.BackgroundImage = Properties.Resources.background;
            //this.BackgroundImageLayout = ImageLayout.Stretch;

            this.BackColor = Color.Black;

            //inicijalizacija i postavljanje platformi
            platformSpeed = 1;

            allPlatforms = new List<Platform>();

            ground = new Platform();
            ground.addPictureBox(groundPictureBox);
            ground.addImage(Properties.Resources.b_fotfor_lush2);
            groundFlag = true;

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

            visiblePlatforms = new List<Platform>();
            visiblePlatforms.Add(allPlatforms[0]);
            visiblePlatforms.Add(allPlatforms[1]);
            visiblePlatforms.Add(allPlatforms[2]);
            visiblePlatforms.Add(allPlatforms[3]);
            visiblePlatforms.Add(allPlatforms[4]);
            visiblePlatforms.Add(allPlatforms[5]);

            putInVisiblePlatforms = 6;
            platformMoving = false;

            menuFlag = true;
            gameFlag = false;
            endgameFlag = false;

            menu = new Menu();
            menu.addPictureBoxAndImage(menuButton_1, Properties.Resources.startImage);
            menu.addPictureBoxAndImage(menuButton_2, Properties.Resources.quitImage);

            copyAllPlatforms = new List<Platform>(allPlatforms);
            copyVisiblePlatforms = new List<Platform>(visiblePlatforms);

            player.addProjectilPictureBox(projectilPictureBox);
            player.addProjectilImage(Properties.Resources.projectil_1);
            player.addProjectilImage(Properties.Resources.projectil_2);
            player.addProjectilImage(Properties.Resources.projectil_3);
            player.addProjectilImage(Properties.Resources.projectil_4);
            player.addProjectilImage(Properties.Resources.projectil_5);

            bossPlatform = new Platform();
            bossFlag = false;
            boss = new Boss();

            boss.addPictureBox(bossPictureBox);
            boss.addProjectilPictureBox(bossProjectilPictureBox);
            //dodati jos slike

        }

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

        //funckija restartira igru, restartira playera i platforme
        private void restartGame()
        {
             player.restart();
             platformRestart();
            bossFlag = false;
        }

        //funckija restarta platforme na njihov pocetni polozaj
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

            putInVisiblePlatforms = 6;
            platformMoving = false;
        }

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
                        else {
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            player.keyPress(sender, e);
        }

        public void bossIsHit()
        {
            boss.Life -= 1;
        }

        private void bossTickProjectil(object sender, EventArgs e)
        {
            boss.bossTickProjectil(sender, e, this);
        }

        private void bossTickSides(object sender, EventArgs e)
        {
            boss.bossTickSides(sender, e, this);
        }

        private void bossTickMovement(object sender, EventArgs e)
        {
            boss.bossTickMovement(sender, e, this);
        }
    }
}
