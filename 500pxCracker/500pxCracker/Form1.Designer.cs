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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginScreen));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.pswdTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.OKButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.warningTextbox = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButton)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(189, 134);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(100, 20);
            this.loginTextBox.TabIndex = 0;
            this.loginTextBox.TextChanged += new System.EventHandler(this.loginTextBox_TextChanged);
            // 
            // pswdTextBox
            // 
            this.pswdTextBox.Location = new System.Drawing.Point(189, 160);
            this.pswdTextBox.Name = "pswdTextBox";
            this.pswdTextBox.Size = new System.Drawing.Size(100, 20);
            this.pswdTextBox.TabIndex = 1;
            this.pswdTextBox.UseSystemPasswordChar = true;
            this.pswdTextBox.TextChanged += new System.EventHandler(this.pswdTextBox_TextChanged);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.Image = ((System.Drawing.Image)(resources.GetObject("closeButton.Image")));
            this.closeButton.ImageActive = null;
            this.closeButton.InitialImage = ((System.Drawing.Image)(resources.GetObject("closeButton.InitialImage")));
            this.closeButton.Location = new System.Drawing.Point(461, 1);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(29, 31);
            this.closeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.closeButton.TabIndex = 4;
            this.closeButton.TabStop = false;
            this.closeButton.Zoom = 10;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Enabled = false;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(144, 57);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(199, 71);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 5;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.Color.Transparent;
            this.OKButton.Image = ((System.Drawing.Image)(resources.GetObject("OKButton.Image")));
            this.OKButton.ImageActive = null;
            this.OKButton.InitialImage = ((System.Drawing.Image)(resources.GetObject("OKButton.InitialImage")));
            this.OKButton.Location = new System.Drawing.Point(189, 187);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 22);
            this.OKButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OKButton.TabIndex = 6;
            this.OKButton.TabStop = false;
            this.OKButton.Zoom = 10;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // warningTextbox
            // 
            this.warningTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.warningTextbox.BorderColor = System.Drawing.Color.Gray;
            this.warningTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.warningTextbox.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.warningTextbox.ForeColor = System.Drawing.Color.Gray;
            this.warningTextbox.Location = new System.Drawing.Point(136, 215);
            this.warningTextbox.Name = "warningTextbox";
            this.warningTextbox.Size = new System.Drawing.Size(227, 16);
            this.warningTextbox.TabIndex = 7;
            this.warningTextbox.Text = "Please provide both login and password!";
            this.warningTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.warningTextbox.Visible = false;
            // 
            // loginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.ClientSize = new System.Drawing.Size(492, 261);
            this.Controls.Add(this.warningTextbox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.bunifuImageButton1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.pswdTextBox);
            this.Controls.Add(this.loginTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "loginScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "500px cracker";
            this.Load += new System.EventHandler(this.loginScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.TextBox pswdTextBox;
        private System.Windows.Forms.TextBox loginTextBox;
        private Bunifu.Framework.UI.BunifuImageButton closeButton;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private Bunifu.Framework.UI.BunifuImageButton OKButton;
        private WindowsFormsControlLibrary1.BunifuCustomTextbox warningTextbox;
    }
}

