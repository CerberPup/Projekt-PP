namespace _500pxCracker
{
    partial class loginScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginScreen));
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.pswdTextBox = new System.Windows.Forms.TextBox();
            this.logoPic = new System.Windows.Forms.PictureBox();
            this.warningTextbox = new System.Windows.Forms.TextBox();
            this.logInButton = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logInButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(189, 151);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(100, 20);
            this.loginTextBox.TabIndex = 0;
            this.loginTextBox.TextChanged += new System.EventHandler(this.loginTextBox_TextChanged);
            // 
            // pswdTextBox
            // 
            this.pswdTextBox.Location = new System.Drawing.Point(189, 177);
            this.pswdTextBox.Name = "pswdTextBox";
            this.pswdTextBox.Size = new System.Drawing.Size(100, 20);
            this.pswdTextBox.TabIndex = 1;
            this.pswdTextBox.UseSystemPasswordChar = true;
            this.pswdTextBox.TextChanged += new System.EventHandler(this.pswdTextBox_TextChanged);
            // 
            // logoPic
            // 
            this.logoPic.BackColor = System.Drawing.Color.Transparent;
            this.logoPic.Enabled = false;
            this.logoPic.Image = ((System.Drawing.Image)(resources.GetObject("logoPic.Image")));
            this.logoPic.Location = new System.Drawing.Point(117, 77);
            this.logoPic.Name = "logoPic";
            this.logoPic.Size = new System.Drawing.Size(253, 59);
            this.logoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPic.TabIndex = 8;
            this.logoPic.TabStop = false;
            // 
            // warningTextbox
            // 
            this.warningTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.warningTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.warningTextbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.warningTextbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.warningTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.warningTextbox.Location = new System.Drawing.Point(117, 234);
            this.warningTextbox.Name = "warningTextbox";
            this.warningTextbox.Size = new System.Drawing.Size(253, 16);
            this.warningTextbox.TabIndex = 10;
            this.warningTextbox.Text = "Please provide both login and password!";
            this.warningTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.warningTextbox.Visible = false;
            // 
            // logInButton
            // 
            this.logInButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.logInButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logInButton.Image = ((System.Drawing.Image)(resources.GetObject("logInButton.Image")));
            this.logInButton.Location = new System.Drawing.Point(225, 203);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(25, 25);
            this.logInButton.TabIndex = 14;
            this.logInButton.TabStop = false;
            this.logInButton.Click += new System.EventHandler(this.logInButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(465, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // loginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.ClientSize = new System.Drawing.Size(492, 282);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.warningTextbox);
            this.Controls.Add(this.logoPic);
            this.Controls.Add(this.pswdTextBox);
            this.Controls.Add(this.loginTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "loginScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "500px cracker";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Load += new System.EventHandler(this.loginScreen_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loginScreen_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logInButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox pswdTextBox;
        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.PictureBox logoPic;
        private System.Windows.Forms.TextBox warningTextbox;
        private System.Windows.Forms.PictureBox logInButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

