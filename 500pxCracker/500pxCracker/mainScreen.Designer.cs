namespace _500pxCracker
{
    partial class mainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainScreen));
            this.menuPanel = new System.Windows.Forms.Panel();
            this.profileButton = new System.Windows.Forms.PictureBox();
            this.profilePanel = new System.Windows.Forms.Panel();
            this.followersPanel = new System.Windows.Forms.Panel();
            this.followersButton = new System.Windows.Forms.PictureBox();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.followersPic = new System.Windows.Forms.PictureBox();
            this.nonFollowersSearchText = new System.Windows.Forms.TextBox();
            this.likeButton = new System.Windows.Forms.PictureBox();
            this.logoPic = new System.Windows.Forms.PictureBox();
            this.likesPanel = new System.Windows.Forms.Panel();
            this.likesPic = new System.Windows.Forms.PictureBox();
            this.nonFolButton = new System.Windows.Forms.PictureBox();
            this.mutualFollowersSearchText = new System.Windows.Forms.TextBox();
            this.statsText = new System.Windows.Forms.TextBox();
            this.mutualFolButton = new System.Windows.Forms.PictureBox();
            this.statsButton = new System.Windows.Forms.PictureBox();
            this.topUsersText1 = new System.Windows.Forms.TextBox();
            this.topUsersButton = new System.Windows.Forms.PictureBox();
            this.numberOfTopUsersTextBox = new System.Windows.Forms.TextBox();
            this.topUsersText2 = new System.Windows.Forms.TextBox();
            this.likeText = new System.Windows.Forms.TextBox();
            this.photosText = new System.Windows.Forms.TextBox();
            this.freshPhotosNumberTextBox = new System.Windows.Forms.TextBox();
            this.photoTypeDropDown = new System.Windows.Forms.ComboBox();
            this.likeFreshButton = new System.Windows.Forms.PictureBox();
            this.likeText2 = new System.Windows.Forms.TextBox();
            this.likeLatestButton = new System.Windows.Forms.PictureBox();
            this.photosNumberTextBox = new System.Windows.Forms.TextBox();
            this.photosText2 = new System.Windows.Forms.TextBox();
            this.likeText3 = new System.Windows.Forms.TextBox();
            this.likePhotosButton = new System.Windows.Forms.PictureBox();
            this.exitButton = new System.Windows.Forms.PictureBox();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileButton)).BeginInit();
            this.profilePanel.SuspendLayout();
            this.followersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.followersButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.followersPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.likeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            this.likesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.likesPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonFolButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutualFolButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topUsersButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.likeFreshButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.likeLatestButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.likePhotosButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.White;
            this.menuPanel.Controls.Add(this.exitButton);
            this.menuPanel.Controls.Add(this.logoPic);
            this.menuPanel.Controls.Add(this.likeButton);
            this.menuPanel.Controls.Add(this.followersButton);
            this.menuPanel.Controls.Add(this.profileButton);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(100, 400);
            this.menuPanel.TabIndex = 0;
            // 
            // profileButton
            // 
            this.profileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.profileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profileButton.Image = ((System.Drawing.Image)(resources.GetObject("profileButton.Image")));
            this.profileButton.Location = new System.Drawing.Point(25, 130);
            this.profileButton.Name = "profileButton";
            this.profileButton.Size = new System.Drawing.Size(50, 50);
            this.profileButton.TabIndex = 1;
            this.profileButton.TabStop = false;
            this.profileButton.Click += new System.EventHandler(this.profileButton_Click);
            // 
            // profilePanel
            // 
            this.profilePanel.Controls.Add(this.statsButton);
            this.profilePanel.Controls.Add(this.statsText);
            this.profilePanel.Controls.Add(this.profilePic);
            this.profilePanel.Location = new System.Drawing.Point(445, 12);
            this.profilePanel.Name = "profilePanel";
            this.profilePanel.Size = new System.Drawing.Size(255, 168);
            this.profilePanel.TabIndex = 1;
            this.profilePanel.Visible = false;
            // 
            // followersPanel
            // 
            this.followersPanel.Controls.Add(this.topUsersText2);
            this.followersPanel.Controls.Add(this.numberOfTopUsersTextBox);
            this.followersPanel.Controls.Add(this.topUsersButton);
            this.followersPanel.Controls.Add(this.topUsersText1);
            this.followersPanel.Controls.Add(this.mutualFolButton);
            this.followersPanel.Controls.Add(this.mutualFollowersSearchText);
            this.followersPanel.Controls.Add(this.nonFolButton);
            this.followersPanel.Controls.Add(this.nonFollowersSearchText);
            this.followersPanel.Controls.Add(this.followersPic);
            this.followersPanel.Location = new System.Drawing.Point(135, 12);
            this.followersPanel.Name = "followersPanel";
            this.followersPanel.Size = new System.Drawing.Size(304, 168);
            this.followersPanel.TabIndex = 2;
            this.followersPanel.Visible = false;
            // 
            // followersButton
            // 
            this.followersButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.followersButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.followersButton.Image = ((System.Drawing.Image)(resources.GetObject("followersButton.Image")));
            this.followersButton.Location = new System.Drawing.Point(25, 186);
            this.followersButton.Name = "followersButton";
            this.followersButton.Size = new System.Drawing.Size(50, 50);
            this.followersButton.TabIndex = 2;
            this.followersButton.TabStop = false;
            this.followersButton.Click += new System.EventHandler(this.followersButton_Click);
            // 
            // profilePic
            // 
            this.profilePic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.profilePic.Enabled = false;
            this.profilePic.Image = ((System.Drawing.Image)(resources.GetObject("profilePic.Image")));
            this.profilePic.Location = new System.Drawing.Point(3, 3);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(50, 50);
            this.profilePic.TabIndex = 3;
            this.profilePic.TabStop = false;
            // 
            // followersPic
            // 
            this.followersPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.followersPic.Enabled = false;
            this.followersPic.Image = ((System.Drawing.Image)(resources.GetObject("followersPic.Image")));
            this.followersPic.Location = new System.Drawing.Point(3, 3);
            this.followersPic.Name = "followersPic";
            this.followersPic.Size = new System.Drawing.Size(50, 50);
            this.followersPic.TabIndex = 3;
            this.followersPic.TabStop = false;
            // 
            // nonFollowersSearchText
            // 
            this.nonFollowersSearchText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.nonFollowersSearchText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nonFollowersSearchText.Cursor = System.Windows.Forms.Cursors.Default;
            this.nonFollowersSearchText.Enabled = false;
            this.nonFollowersSearchText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.nonFollowersSearchText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.nonFollowersSearchText.Location = new System.Drawing.Point(3, 72);
            this.nonFollowersSearchText.Name = "nonFollowersSearchText";
            this.nonFollowersSearchText.Size = new System.Drawing.Size(240, 15);
            this.nonFollowersSearchText.TabIndex = 4;
            this.nonFollowersSearchText.Text = "Search for users I follow that do not follow me";
            // 
            // likeButton
            // 
            this.likeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.likeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.likeButton.Image = ((System.Drawing.Image)(resources.GetObject("likeButton.Image")));
            this.likeButton.Location = new System.Drawing.Point(25, 242);
            this.likeButton.Name = "likeButton";
            this.likeButton.Size = new System.Drawing.Size(50, 50);
            this.likeButton.TabIndex = 3;
            this.likeButton.TabStop = false;
            this.likeButton.Click += new System.EventHandler(this.likeButton_Click);
            // 
            // logoPic
            // 
            this.logoPic.BackColor = System.Drawing.Color.Transparent;
            this.logoPic.Enabled = false;
            this.logoPic.Image = ((System.Drawing.Image)(resources.GetObject("logoPic.Image")));
            this.logoPic.Location = new System.Drawing.Point(15, 15);
            this.logoPic.Name = "logoPic";
            this.logoPic.Size = new System.Drawing.Size(70, 18);
            this.logoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPic.TabIndex = 9;
            this.logoPic.TabStop = false;
            // 
            // likesPanel
            // 
            this.likesPanel.Controls.Add(this.likePhotosButton);
            this.likesPanel.Controls.Add(this.photosNumberTextBox);
            this.likesPanel.Controls.Add(this.photosText2);
            this.likesPanel.Controls.Add(this.likeText3);
            this.likesPanel.Controls.Add(this.likeLatestButton);
            this.likesPanel.Controls.Add(this.likeText2);
            this.likesPanel.Controls.Add(this.likeFreshButton);
            this.likesPanel.Controls.Add(this.photoTypeDropDown);
            this.likesPanel.Controls.Add(this.freshPhotosNumberTextBox);
            this.likesPanel.Controls.Add(this.photosText);
            this.likesPanel.Controls.Add(this.likeText);
            this.likesPanel.Controls.Add(this.likesPic);
            this.likesPanel.Location = new System.Drawing.Point(135, 186);
            this.likesPanel.Name = "likesPanel";
            this.likesPanel.Size = new System.Drawing.Size(565, 202);
            this.likesPanel.TabIndex = 4;
            this.likesPanel.Visible = false;
            // 
            // likesPic
            // 
            this.likesPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.likesPic.Enabled = false;
            this.likesPic.Image = ((System.Drawing.Image)(resources.GetObject("likesPic.Image")));
            this.likesPic.Location = new System.Drawing.Point(3, 3);
            this.likesPic.Name = "likesPic";
            this.likesPic.Size = new System.Drawing.Size(50, 50);
            this.likesPic.TabIndex = 3;
            this.likesPic.TabStop = false;
            // 
            // nonFolButton
            // 
            this.nonFolButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nonFolButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nonFolButton.Image = ((System.Drawing.Image)(resources.GetObject("nonFolButton.Image")));
            this.nonFolButton.Location = new System.Drawing.Point(267, 64);
            this.nonFolButton.Name = "nonFolButton";
            this.nonFolButton.Size = new System.Drawing.Size(25, 25);
            this.nonFolButton.TabIndex = 10;
            this.nonFolButton.TabStop = false;
            this.nonFolButton.Click += new System.EventHandler(this.nonFolButton_Click);
            // 
            // mutualFollowersSearchText
            // 
            this.mutualFollowersSearchText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.mutualFollowersSearchText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mutualFollowersSearchText.Cursor = System.Windows.Forms.Cursors.Default;
            this.mutualFollowersSearchText.Enabled = false;
            this.mutualFollowersSearchText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.mutualFollowersSearchText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.mutualFollowersSearchText.Location = new System.Drawing.Point(3, 103);
            this.mutualFollowersSearchText.Name = "mutualFollowersSearchText";
            this.mutualFollowersSearchText.Size = new System.Drawing.Size(240, 15);
            this.mutualFollowersSearchText.TabIndex = 11;
            this.mutualFollowersSearchText.Text = "Search for mutual followers";
            // 
            // statsText
            // 
            this.statsText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.statsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statsText.Cursor = System.Windows.Forms.Cursors.Default;
            this.statsText.Enabled = false;
            this.statsText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.statsText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.statsText.Location = new System.Drawing.Point(3, 70);
            this.statsText.Name = "statsText";
            this.statsText.Size = new System.Drawing.Size(120, 15);
            this.statsText.TabIndex = 13;
            this.statsText.Text = "Show me my statistics";
            // 
            // mutualFolButton
            // 
            this.mutualFolButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mutualFolButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mutualFolButton.Image = ((System.Drawing.Image)(resources.GetObject("mutualFolButton.Image")));
            this.mutualFolButton.Location = new System.Drawing.Point(267, 95);
            this.mutualFolButton.Name = "mutualFolButton";
            this.mutualFolButton.Size = new System.Drawing.Size(25, 25);
            this.mutualFolButton.TabIndex = 12;
            this.mutualFolButton.TabStop = false;
            this.mutualFolButton.Click += new System.EventHandler(this.mutualFolButton_Click);
            // 
            // statsButton
            // 
            this.statsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.statsButton.Image = ((System.Drawing.Image)(resources.GetObject("statsButton.Image")));
            this.statsButton.Location = new System.Drawing.Point(119, 64);
            this.statsButton.Name = "statsButton";
            this.statsButton.Size = new System.Drawing.Size(25, 25);
            this.statsButton.TabIndex = 13;
            this.statsButton.TabStop = false;
            this.statsButton.Click += new System.EventHandler(this.statsButton_Click);
            // 
            // topUsersText1
            // 
            this.topUsersText1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.topUsersText1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.topUsersText1.Cursor = System.Windows.Forms.Cursors.Default;
            this.topUsersText1.Enabled = false;
            this.topUsersText1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.topUsersText1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.topUsersText1.Location = new System.Drawing.Point(3, 133);
            this.topUsersText1.Name = "topUsersText1";
            this.topUsersText1.Size = new System.Drawing.Size(80, 15);
            this.topUsersText1.TabIndex = 13;
            this.topUsersText1.Text = "Search for top";
            // 
            // topUsersButton
            // 
            this.topUsersButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.topUsersButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.topUsersButton.Image = ((System.Drawing.Image)(resources.GetObject("topUsersButton.Image")));
            this.topUsersButton.Location = new System.Drawing.Point(267, 125);
            this.topUsersButton.Name = "topUsersButton";
            this.topUsersButton.Size = new System.Drawing.Size(25, 25);
            this.topUsersButton.TabIndex = 14;
            this.topUsersButton.TabStop = false;
            this.topUsersButton.Click += new System.EventHandler(this.topUsersButton_Click);
            // 
            // numberOfTopUsersTextBox
            // 
            this.numberOfTopUsersTextBox.BackColor = System.Drawing.Color.LightGray;
            this.numberOfTopUsersTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberOfTopUsersTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.numberOfTopUsersTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.numberOfTopUsersTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.numberOfTopUsersTextBox.Location = new System.Drawing.Point(79, 131);
            this.numberOfTopUsersTextBox.MaxLength = 2;
            this.numberOfTopUsersTextBox.Name = "numberOfTopUsersTextBox";
            this.numberOfTopUsersTextBox.Size = new System.Drawing.Size(25, 22);
            this.numberOfTopUsersTextBox.TabIndex = 15;
            this.numberOfTopUsersTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numberOfTopUsersTextBox.TextChanged += new System.EventHandler(this.numberOfTopUsersTextBox_TextChanged);
            // 
            // topUsersText2
            // 
            this.topUsersText2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.topUsersText2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.topUsersText2.Cursor = System.Windows.Forms.Cursors.Default;
            this.topUsersText2.Enabled = false;
            this.topUsersText2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.topUsersText2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.topUsersText2.Location = new System.Drawing.Point(110, 133);
            this.topUsersText2.Name = "topUsersText2";
            this.topUsersText2.Size = new System.Drawing.Size(150, 15);
            this.topUsersText2.TabIndex = 16;
            this.topUsersText2.Text = "liking users from the last day";
            // 
            // likeText
            // 
            this.likeText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.likeText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.likeText.Cursor = System.Windows.Forms.Cursors.Default;
            this.likeText.Enabled = false;
            this.likeText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.likeText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.likeText.Location = new System.Drawing.Point(3, 76);
            this.likeText.Name = "likeText";
            this.likeText.Size = new System.Drawing.Size(20, 15);
            this.likeText.TabIndex = 17;
            this.likeText.Text = "Like";
            // 
            // photosText
            // 
            this.photosText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.photosText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.photosText.Cursor = System.Windows.Forms.Cursors.Default;
            this.photosText.Enabled = false;
            this.photosText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.photosText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.photosText.Location = new System.Drawing.Point(61, 76);
            this.photosText.Name = "photosText";
            this.photosText.Size = new System.Drawing.Size(65, 15);
            this.photosText.TabIndex = 17;
            this.photosText.Text = "photos from";
            // 
            // freshPhotosNumberTextBox
            // 
            this.freshPhotosNumberTextBox.BackColor = System.Drawing.Color.LightGray;
            this.freshPhotosNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.freshPhotosNumberTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.freshPhotosNumberTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.freshPhotosNumberTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.freshPhotosNumberTextBox.Location = new System.Drawing.Point(27, 72);
            this.freshPhotosNumberTextBox.MaxLength = 4;
            this.freshPhotosNumberTextBox.Name = "freshPhotosNumberTextBox";
            this.freshPhotosNumberTextBox.Size = new System.Drawing.Size(30, 22);
            this.freshPhotosNumberTextBox.TabIndex = 17;
            this.freshPhotosNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.freshPhotosNumberTextBox.TextChanged += new System.EventHandler(this.freshPhotosNumberTextBox_TextChanged);
            // 
            // photoTypeDropDown
            // 
            this.photoTypeDropDown.BackColor = System.Drawing.Color.LightGray;
            this.photoTypeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.photoTypeDropDown.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.photoTypeDropDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.photoTypeDropDown.FormattingEnabled = true;
            this.photoTypeDropDown.Items.AddRange(new object[] {
            "Fresh",
            "Upcoming"});
            this.photoTypeDropDown.Location = new System.Drawing.Point(132, 72);
            this.photoTypeDropDown.Name = "photoTypeDropDown";
            this.photoTypeDropDown.Size = new System.Drawing.Size(121, 21);
            this.photoTypeDropDown.TabIndex = 18;
            // 
            // likeFreshButton
            // 
            this.likeFreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.likeFreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.likeFreshButton.Image = ((System.Drawing.Image)(resources.GetObject("likeFreshButton.Image")));
            this.likeFreshButton.Location = new System.Drawing.Point(279, 72);
            this.likeFreshButton.Name = "likeFreshButton";
            this.likeFreshButton.Size = new System.Drawing.Size(25, 25);
            this.likeFreshButton.TabIndex = 14;
            this.likeFreshButton.TabStop = false;
            this.likeFreshButton.Click += new System.EventHandler(this.likeFreshButton_Click);
            // 
            // likeText2
            // 
            this.likeText2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.likeText2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.likeText2.Cursor = System.Windows.Forms.Cursors.Default;
            this.likeText2.Enabled = false;
            this.likeText2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.likeText2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.likeText2.Location = new System.Drawing.Point(3, 107);
            this.likeText2.Name = "likeText2";
            this.likeText2.Size = new System.Drawing.Size(220, 15);
            this.likeText2.TabIndex = 17;
            this.likeText2.Text = "Like all lastest photos of the users I follow";
            // 
            // likeLatestButton
            // 
            this.likeLatestButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.likeLatestButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.likeLatestButton.Image = ((System.Drawing.Image)(resources.GetObject("likeLatestButton.Image")));
            this.likeLatestButton.Location = new System.Drawing.Point(279, 103);
            this.likeLatestButton.Name = "likeLatestButton";
            this.likeLatestButton.Size = new System.Drawing.Size(25, 25);
            this.likeLatestButton.TabIndex = 19;
            this.likeLatestButton.TabStop = false;
            this.likeLatestButton.Click += new System.EventHandler(this.likeLatestButton_Click);
            // 
            // photosNumberTextBox
            // 
            this.photosNumberTextBox.BackColor = System.Drawing.Color.LightGray;
            this.photosNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.photosNumberTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.photosNumberTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.photosNumberTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.photosNumberTextBox.Location = new System.Drawing.Point(27, 135);
            this.photosNumberTextBox.MaxLength = 4;
            this.photosNumberTextBox.Name = "photosNumberTextBox";
            this.photosNumberTextBox.Size = new System.Drawing.Size(30, 22);
            this.photosNumberTextBox.TabIndex = 20;
            this.photosNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.photosNumberTextBox.TextChanged += new System.EventHandler(this.photosNumberTextBox_TextChanged);
            // 
            // photosText2
            // 
            this.photosText2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.photosText2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.photosText2.Cursor = System.Windows.Forms.Cursors.Default;
            this.photosText2.Enabled = false;
            this.photosText2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.photosText2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.photosText2.Location = new System.Drawing.Point(63, 137);
            this.photosText2.Name = "photosText2";
            this.photosText2.Size = new System.Drawing.Size(210, 15);
            this.photosText2.TabIndex = 21;
            this.photosText2.Text = "photos from users who liked my photos";
            // 
            // likeText3
            // 
            this.likeText3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.likeText3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.likeText3.Cursor = System.Windows.Forms.Cursors.Default;
            this.likeText3.Enabled = false;
            this.likeText3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.likeText3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.likeText3.Location = new System.Drawing.Point(3, 139);
            this.likeText3.Name = "likeText3";
            this.likeText3.Size = new System.Drawing.Size(20, 15);
            this.likeText3.TabIndex = 22;
            this.likeText3.Text = "Like";
            // 
            // likePhotosButton
            // 
            this.likePhotosButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.likePhotosButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.likePhotosButton.Image = ((System.Drawing.Image)(resources.GetObject("likePhotosButton.Image")));
            this.likePhotosButton.Location = new System.Drawing.Point(279, 132);
            this.likePhotosButton.Name = "likePhotosButton";
            this.likePhotosButton.Size = new System.Drawing.Size(25, 25);
            this.likePhotosButton.TabIndex = 23;
            this.likePhotosButton.TabStop = false;
            this.likePhotosButton.Click += new System.EventHandler(this.likePhotosButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.Image = ((System.Drawing.Image)(resources.GetObject("exitButton.Image")));
            this.exitButton.Location = new System.Drawing.Point(25, 298);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(50, 50);
            this.exitButton.TabIndex = 10;
            this.exitButton.TabStop = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // mainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.ClientSize = new System.Drawing.Size(712, 400);
            this.Controls.Add(this.likesPanel);
            this.Controls.Add(this.followersPanel);
            this.Controls.Add(this.profilePanel);
            this.Controls.Add(this.menuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mainScreen";
            this.menuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profileButton)).EndInit();
            this.profilePanel.ResumeLayout(false);
            this.profilePanel.PerformLayout();
            this.followersPanel.ResumeLayout(false);
            this.followersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.followersButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.followersPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.likeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            this.likesPanel.ResumeLayout(false);
            this.likesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.likesPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonFolButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutualFolButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topUsersButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.likeFreshButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.likeLatestButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.likePhotosButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.PictureBox profileButton;
        private System.Windows.Forms.Panel profilePanel;
        private System.Windows.Forms.Panel followersPanel;
        private System.Windows.Forms.PictureBox followersButton;
        private System.Windows.Forms.PictureBox profilePic;
        private System.Windows.Forms.PictureBox followersPic;
        private System.Windows.Forms.TextBox nonFollowersSearchText;
        private System.Windows.Forms.PictureBox likeButton;
        private System.Windows.Forms.PictureBox logoPic;
        private System.Windows.Forms.Panel likesPanel;
        private System.Windows.Forms.PictureBox likesPic;
        private System.Windows.Forms.TextBox mutualFollowersSearchText;
        private System.Windows.Forms.PictureBox nonFolButton;
        private System.Windows.Forms.TextBox statsText;
        private System.Windows.Forms.PictureBox statsButton;
        private System.Windows.Forms.PictureBox mutualFolButton;
        private System.Windows.Forms.PictureBox topUsersButton;
        private System.Windows.Forms.TextBox topUsersText1;
        private System.Windows.Forms.TextBox numberOfTopUsersTextBox;
        private System.Windows.Forms.TextBox topUsersText2;
        private System.Windows.Forms.TextBox freshPhotosNumberTextBox;
        private System.Windows.Forms.TextBox photosText;
        private System.Windows.Forms.TextBox likeText;
        private System.Windows.Forms.ComboBox photoTypeDropDown;
        private System.Windows.Forms.PictureBox likeFreshButton;
        private System.Windows.Forms.TextBox likeText2;
        private System.Windows.Forms.TextBox photosNumberTextBox;
        private System.Windows.Forms.TextBox photosText2;
        private System.Windows.Forms.TextBox likeText3;
        private System.Windows.Forms.PictureBox likeLatestButton;
        private System.Windows.Forms.PictureBox likePhotosButton;
        private System.Windows.Forms.PictureBox exitButton;
    }
}