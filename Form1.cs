using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beskonačni_Toranj
{
    public partial class Form1 : Form
    {
        //player kojeg korisnik moze pokretati
        Player player;
        //jedno glavno tlo
        Platform ground;
        //platforma
        Platform platform_1;
        //lista koja drzi sve platforme
        List<Platform> allPlatforms;
        //lista sadrzi platforme koje su samo u pojedinom trenutku vidljive na ekranu
        List<Platform> visiblePlatforms;
        //brzina kojom se krecu platforme
        int platformSpeed;
        //counter koji pazi koja se sljedeca platforma stavlja u one koje se vide na ekranu
        int putInVisiblePlatforms;
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
            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //postavljanje tla
            /*ground = new Platform();
            ground.addPictureBox(groundPictureBox);
            ground.addImage(Properties.Resources.b_fotfor_lush2);
            */
            //inicijalizacija i postavljanje platformi
            /* platform_1 = new Platform();
             platform_1.addPictureBox(platformPictureBox_1);//mora ici prije postavljanja image
             platform_1.addImage(Properties.Resources.b_fotfor_lush2);
            */
            platformSpeed = 1;

            allPlatforms = new List<Platform>();
            allPlatforms.Add(new Platform(groundPictureBox, Properties.Resources.b_fotfor_lush2));
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

        }

        //akcije koje se obavljaju kad se pritisne neka tipka; sve se salje u odgovarajuce funkcije kod playera
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player.keyDown(sender, e);    
        }
        //akcije koje se obavljaju kad se otpusti neka tipka; sve se salje u odgovarajuce funkcije kod playera
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            player.keyUp(sender, e);
        }
        //crtanje objekata u formi; sve se salje u odgovarajuce funckije clanova forme (playeri i platforme)
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            player.paint(sender, e);
            /*platform_1.platformPaint(sender, e);
            ground.platformPaint(sender, e);
           */
            foreach (Platform p in visiblePlatforms)
            {  
                p.platformPaint(sender, e);
            }
        }
        //akcije koje se obavljaju kako vrijeme tece; sve se salje u odgovarajuce funkcije clanova forme (playera i platforme)
        //na kraju se poziva funkcija koja crta objekte 
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            player.timerTick(sender, e, this);
            
            //provjeri koja je platforma "izasla" iz prozora, makni ju ih vidljivih platformi,
            //i stavi novu platformu u prozor, tj medu vidljive platforme

            for (int i = 0; i < allPlatforms.Count; i++)
            {
                //Console.WriteLine(allPlatforms[i].Y);
                if (allPlatforms[i].Y > 500)
                {
                    //Console.WriteLine(allPlatforms[i].Y);
                    visiblePlatforms.Remove(allPlatforms[i]);
                    allPlatforms[i].Y = -410;//postavi kao zadnju koja ce doci prema dolje
                    if (i + 1 == allPlatforms.Count)
                    {
                        
                        visiblePlatforms.Add(allPlatforms[1]);//na 0 se nalazi ground
                    }else 
                    {
                       // Console.WriteLine("ovdje");
                        visiblePlatforms.Add(allPlatforms[putInVisiblePlatforms]);

                        if (putInVisiblePlatforms + 1 == allPlatforms.Count)
                        {
                            
                            putInVisiblePlatforms = 1;
                        }else 
                        {
                           // Console.WriteLine("ovdje");
                            putInVisiblePlatforms++;
                        }
                    }
                    
                }
            }

            //pomakni sve platforme prema dolje
            foreach (Platform p in allPlatforms)
            {   
                p.Y += platformSpeed;
            }

            Invalidate();

        }
    }
}
