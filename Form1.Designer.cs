namespace Beskonačni_Toranj
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.platformPictureBox_1 = new System.Windows.Forms.PictureBox();
            this.groundPictureBox = new System.Windows.Forms.PictureBox();
            this.playerPictureBox = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.platformPictureBox_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groundPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // platformPictureBox_1
            // 
            this.platformPictureBox_1.Location = new System.Drawing.Point(298, 413);
            this.platformPictureBox_1.Name = "platformPictureBox_1";
            this.platformPictureBox_1.Size = new System.Drawing.Size(153, 64);
            this.platformPictureBox_1.TabIndex = 2;
            this.platformPictureBox_1.TabStop = false;
            this.platformPictureBox_1.Tag = "platform";
            // 
            // groundPictureBox
            // 
            this.groundPictureBox.Location = new System.Drawing.Point(-13, 594);
            this.groundPictureBox.Name = "groundPictureBox";
            this.groundPictureBox.Size = new System.Drawing.Size(1027, 88);
            this.groundPictureBox.TabIndex = 1;
            this.groundPictureBox.TabStop = false;
            this.groundPictureBox.Tag = "ground";
            // 
            // playerPictureBox
            // 
            this.playerPictureBox.BackColor = System.Drawing.Color.Black;
            this.playerPictureBox.Location = new System.Drawing.Point(103, 422);
            this.playerPictureBox.Name = "playerPictureBox";
            this.playerPictureBox.Size = new System.Drawing.Size(55, 69);
            this.playerPictureBox.TabIndex = 0;
            this.playerPictureBox.TabStop = false;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 649);
            this.Controls.Add(this.platformPictureBox_1);
            this.Controls.Add(this.groundPictureBox);
            this.Controls.Add(this.playerPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.platformPictureBox_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groundPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox playerPictureBox;
        private System.Windows.Forms.PictureBox groundPictureBox;
        private System.Windows.Forms.PictureBox platformPictureBox_1;
        private System.Windows.Forms.Timer gameTimer;
    }
}

