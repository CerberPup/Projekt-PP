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
            this.closeButton = new System.Windows.Forms.Button();
            this.warningTextbox = new System.Windows.Forms.TextBox();
            this.OKbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            this.SuspendLayout();
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
            // logoPic
            // 
            this.logoPic.BackColor = System.Drawing.Color.Transparent;
            this.logoPic.Enabled = false;
            this.logoPic.Image = ((System.Drawing.Image)(resources.GetObject("logoPic.Image")));
            this.logoPic.Location = new System.Drawing.Point(117, 59);
            this.logoPic.Name = "logoPic";
            this.logoPic.Size = new System.Drawing.Size(253, 59);
            this.logoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPic.TabIndex = 8;
            this.logoPic.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closeButton.Image = ((System.Drawing.Image)(resources.GetObject("closeButton.Image")));
            this.closeButton.Location = new System.Drawing.Point(466, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(23, 22);
            this.closeButton.TabIndex = 9;
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // warningTextbox
            // 
            this.warningTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.warningTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.warningTextbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.warningTextbox.Font = new System.Drawing.Font("Corbel", 9.25F);
            this.warningTextbox.Location = new System.Drawing.Point(117, 245);
            this.warningTextbox.Name = "warningTextbox";
            this.warningTextbox.Size = new System.Drawing.Size(253, 16);
            this.warningTextbox.TabIndex = 10;
            this.warningTextbox.Text = "Please provide both login and password!";
            this.warningTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.warningTextbox.Visible = false;
            // 
            // OKbutton
            // 
            this.OKbutton.BackColor = System.Drawing.Color.Transparent;
            this.OKbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OKbutton.Image = ((System.Drawing.Image)(resources.GetObject("OKbutton.Image")));
            this.OKbutton.Location = new System.Drawing.Point(207, 181);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(69, 58);
            this.OKbutton.TabIndex = 11;
            this.OKbutton.UseVisualStyleBackColor = false;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click_1);
            // 
            // loginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.ClientSize = new System.Drawing.Size(492, 282);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.warningTextbox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.logoPic);
            this.Controls.Add(this.pswdTextBox);
            this.Controls.Add(this.loginTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "loginScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "500px cracker";
            this.Load += new System.EventHandler(this.loginScreen_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loginScreen_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox pswdTextBox;
        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.PictureBox logoPic;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox warningTextbox;
        private System.Windows.Forms.Button OKbutton;
    }
}

