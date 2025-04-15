namespace SurvivorCursorGame
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
            this.picMain = new System.Windows.Forms.PictureBox();
            this.tmrMoveBall = new System.Windows.Forms.Timer(this.components);
            this.tmrShoot = new System.Windows.Forms.Timer(this.components);
            this.tmrMoveGun = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.picMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMain.Location = new System.Drawing.Point(0, 0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(958, 608);
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            this.picMain.Click += new System.EventHandler(this.picMain_Click);
            this.picMain.Paint += new System.Windows.Forms.PaintEventHandler(this.picMain_Paint);
            this.picMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseMove);
            // 
            // tmrMoveBall
            // 
            this.tmrMoveBall.Interval = 30;
            this.tmrMoveBall.Tick += new System.EventHandler(this.tmrMoveBall_Tick);
            // 
            // tmrShoot
            // 
            this.tmrShoot.Interval = 500;
            this.tmrShoot.Tick += new System.EventHandler(this.tmrShoot_Tick);
            // 
            // tmrMoveGun
            // 
            this.tmrMoveGun.Interval = 1;
            this.tmrMoveGun.Tick += new System.EventHandler(this.tmrMoveGun_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 608);
            this.Controls.Add(this.picMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.Timer tmrMoveBall;
        private System.Windows.Forms.Timer tmrShoot;
        private System.Windows.Forms.Timer tmrMoveGun;
    }
}

