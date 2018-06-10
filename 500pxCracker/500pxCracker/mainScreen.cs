using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _500pxCracker
{
    public partial class mainScreen : Form
    {
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private bool isPythonRunning;
        private List<string> SelectedUsers = new List<string>();
        private enum FollowersComboBoxState
        {
            All = 0,
            OnlyFollowers,
            OnlyFollowing,
            Followers,
            Following,
            Mutuals

        };
        public mainScreen()
        {
            InitializeComponent();
            { 
                profileButton.MouseEnter += OnMouseEnterprofileButton;
                profileButton.MouseLeave += OnMouseLeaveprofileButton;
                followersButton.MouseEnter += OnMouseEnterfollowersButton;
                followersButton.MouseLeave += OnMouseLeavefollowersButton;
                likeButton.MouseEnter += OnMouseEnterlikeButton;
                likeButton.MouseLeave += OnMouseLeavelikeButton;
                updateDBButton.MouseEnter += OnMouseEnterupdateDBButton;
                updateDBButton.MouseLeave += OnMouseLeaveupdateDBButton;
                exitButton.MouseEnter += OnMouseEnterexitButton;
                exitButton.MouseLeave += OnMouseLeaveexitButton;
                killingPythonButton.MouseEnter += OnMouseEnterkillingPythonButton;
                killingPythonButton.MouseLeave += OnMouseLeavekillingPythonButton;

            
                closeButton.MouseEnter += OnMouseEntercloseButton;
                closeButton.MouseLeave += OnMouseLeavecloseButton;
                mutualFolButton.MouseEnter += OnMouseEntermutualFolButton;
                mutualFolButton.MouseLeave += OnMouseLeavemutualFolButton;
                selectAllUsersButton.MouseEnter += OnMouseEnterselectAllUsersButton;
                selectAllUsersButton.MouseLeave += OnMouseLeaveselectAllUsersButton;
                deselectAllUsersButton.MouseEnter += OnMouseEnterdeselectAllUsersButton;
                deselectAllUsersButton.MouseLeave += OnMouseLeavedeselectAllUsersButton;
                invertSelectionButton.MouseEnter += OnMouseEnterinvertSelectionButton;
                invertSelectionButton.MouseLeave += OnMouseLeaveinvertSelectionButton;
                selectRandomButton.MouseEnter += OnMouseEnterselectRandomButton;
                selectRandomButton.MouseLeave += OnMouseLeaveselectRandomButton;
                unfollowButton.MouseEnter += OnMouseEnterunfollowButton;
                unfollowButton.MouseLeave += OnMouseLeaveunfollowButton;
                followButton.MouseEnter += OnMouseEnterfollowButton;
                followButton.MouseLeave += OnMouseLeavefollowButton;

                statsButton.MouseEnter += OnMouseEnterstatsButton;
                statsButton.MouseLeave += OnMouseLeavestatsButton;
                likeFreshButton.MouseEnter += OnMouseEnterlikeFreshButton;
                likeFreshButton.MouseLeave += OnMouseLeavelikeFreshButton;
                likeLatestButton.MouseEnter += OnMouseEnterlikeLatestButton;
                likeLatestButton.MouseLeave += OnMouseLeavelikeLatestButton;
                likePhotosButton.MouseEnter += OnMouseEnterlikePhotosButton;
                likePhotosButton.MouseLeave += OnMouseLeavelikePhotosButton;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts
        }
        
        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 10; // you can rename this variable if you like

        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        //Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        //Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }
        /*
        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }
        */

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                //if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                //else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                //else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                //else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                //else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                //else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }


    private void OnMouseEnterprofileButton(object sender, EventArgs e)
        {
            profileButton.BackColor = Color.FromArgb(196, 196, 196);
        }

        private void OnMouseLeaveprofileButton(object sender, EventArgs e)
        {
            profileButton.BackColor = Color.White;
        }

        private void OnMouseEnterfollowersButton(object sender, EventArgs e)
        {
            followersButton.BackColor = Color.FromArgb(196, 196, 196);
        }

        private void OnMouseLeavefollowersButton(object sender, EventArgs e)
        {
            followersButton.BackColor = Color.White;
        }

        private void OnMouseEnterlikeButton(object sender, EventArgs e)
        {
            likeButton.BackColor = Color.FromArgb(196, 196, 196);
        }

        private void OnMouseLeavelikeButton(object sender, EventArgs e)
        {
            likeButton.BackColor = Color.White;
        }
        private void OnMouseEnterupdateDBButton(object sender, EventArgs e)
        {
            updateDBButton.BackColor = Color.FromArgb(196, 196, 196);
        }

        private void OnMouseLeaveupdateDBButton(object sender, EventArgs e)
        {
            updateDBButton.BackColor = Color.White;
        }

        private void OnMouseEnterexitButton(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.FromArgb(196, 196, 196);
        }

        private void OnMouseLeaveexitButton(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.White;
        }

        private void OnMouseEnterkillingPythonButton(object sender, EventArgs e)
        {
            killingPythonButton.BackColor = Color.FromArgb(196, 196, 196);
        }

        private void OnMouseLeavekillingPythonButton(object sender, EventArgs e)
        {
            killingPythonButton.BackColor = Color.White;
        }

        //--------------------------------------------------------------------------------------

        private void OnMouseEntercloseButton(object sender, EventArgs e)
        {
            closeButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavecloseButton(object sender, EventArgs e)
        {
            closeButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEntermutualFolButton(object sender, EventArgs e)
        {
            mutualFolButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavemutualFolButton(object sender, EventArgs e)
        {
            mutualFolButton.BackColor = Color.FromArgb(255, 205, 205);
        }
        
        private void OnMouseEnterselectAllUsersButton(object sender, EventArgs e)
        {
            selectAllUsersButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeaveselectAllUsersButton(object sender, EventArgs e)
        {
            selectAllUsersButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterdeselectAllUsersButton(object sender, EventArgs e)
        {
            deselectAllUsersButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavedeselectAllUsersButton(object sender, EventArgs e)
        {
            deselectAllUsersButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterinvertSelectionButton(object sender, EventArgs e)
        {
            invertSelectionButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeaveinvertSelectionButton(object sender, EventArgs e)
        {
            invertSelectionButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterselectRandomButton(object sender, EventArgs e)
        {
            selectRandomButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeaveselectRandomButton(object sender, EventArgs e)
        {
            selectRandomButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterunfollowButton(object sender, EventArgs e)
        {
            unfollowButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeaveunfollowButton(object sender, EventArgs e)
        {
            unfollowButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterfollowButton(object sender, EventArgs e)
        {
            followButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavefollowButton(object sender, EventArgs e)
        {
            followButton.BackColor = Color.FromArgb(255, 205, 205);
        }
        //----------------------------------------------------------------------------------------

        private void OnMouseEnterstatsButton(object sender, EventArgs e)
        {
            statsButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavestatsButton(object sender, EventArgs e)
        {
            statsButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterlikeFreshButton(object sender, EventArgs e)
        {
            likeFreshButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavelikeFreshButton(object sender, EventArgs e)
        {
            likeFreshButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterlikeLatestButton(object sender, EventArgs e)
        {
            likeLatestButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavelikeLatestButton(object sender, EventArgs e)
        {
            likeLatestButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void OnMouseEnterlikePhotosButton(object sender, EventArgs e)
        {
            likePhotosButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavelikePhotosButton(object sender, EventArgs e)
        {
            likePhotosButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        //-----------------------------------------------------------------------------------------

        private void mainScreen_Load(object sender, EventArgs e)
        {
            followersComboBox.SelectedIndex = 0;
            timeDropDown.SelectedIndex = 0;
            //this.Size = new Size(771, 454);
            //this.StartPosition = FormStartPosition.CenterParent;

            //followers
            followersPanel.Size = new Size(617, 433);
            followersPanel.Location = new Point(119, 12);

            followersSearchPanel.Size = new Size(580, 344);
            followersSearchPanel.Location = new Point(21, 86);

            //likes
            likesPanel.Location = new Point(119, 12);
            likesPanel.Size = new Size(460, 185);

            //profile
            profilePanel.Location = new Point(119, 12);
            profilePanel.Size = new Size(440, 185);

            photoTypeDropDown.SelectedIndex = 0;
            timeDropDown.SelectedIndex = 0;
            followersComboBox.SelectedIndex = 0;
        }

        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        private void SetPythonRunning(bool val)
        {
            isPythonRunning = val;
            PythonLabel.Visible = val;
            pythonRunningPic.Visible = val;
            killingPythonButton.Visible = val;
            if (val == false)
                MessageBox.Show("Python successfully ended");
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            if(!profilePanel.Visible)
                profilePanel.Visible = true;
            if (followersPanel.Visible)
                followersPanel.Visible = false;
            if (likesPanel.Visible)
                likesPanel.Visible = false;
        }

        private void followersButton_Click(object sender, EventArgs e)
        {
            if(!followersPanel.Visible)
                followersPanel.Visible = true;
            if (profilePanel.Visible)
                profilePanel.Visible = false;
            if (likesPanel.Visible)
                likesPanel.Visible = false;
            followersComboBox_SelectedIndexChanged(this, new EventArgs());
        }

        private void likeButton_Click(object sender, EventArgs e)
        {
            if (!likesPanel.Visible)
                likesPanel.Visible = true;
            if (followersPanel.Visible)
                followersPanel.Visible = false;
            if (profilePanel.Visible)
                profilePanel.Visible = false;
        }

        private void freshPhotosNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(freshPhotosNumberTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                freshPhotosNumberTextBox.Text = freshPhotosNumberTextBox.Text.Remove(freshPhotosNumberTextBox.Text.Length - 1);
            }
        }

        private void photosNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(photosNumberTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                photosNumberTextBox.Text = photosNumberTextBox.Text.Remove(photosNumberTextBox.Text.Length - 1);
            }
        }
        

        //likes panel ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void LikeFresh(int SelectedIndex, int Number)
        {
            CurrentUser.Get().LikeFresh(SelectedIndex, Number);
            MessageBox.Show("Successfully liked the photos!");
        }
        private void likeFreshButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                if (photoTypeDropDown.SelectedIndex > -1 && freshPhotosNumberTextBox.Text.Length != 0)
                {
                    PythonWorker.RunWorkerAsync("LikeFresh-"+ photoTypeDropDown.SelectedIndex+"-"+ int.Parse(freshPhotosNumberTextBox.Text));
                }
                else
                    MessageBox.Show("Please provide all required information!");
            }
        }

        private void LikeLatestPhotos()
        {
            dataGetter.GetFollowersandFollowings();
            CurrentUser.Get().LikeLatestPhotos();

            MessageBox.Show("Successfully liked all the photos!");
        }
        private void likeLatestButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                PythonWorker.RunWorkerAsync("LikeLatestPhotos");
            }
        }

        private void likePhotosButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                if (photosNumberTextBox.Text.Length != 0)
                {
                    CurrentUser.Get().LikeLikingMe(int.Parse(photosNumberTextBox.Text));

                    MessageBox.Show("Successfully liked all the photos");
                    photosNumberTextBox.Text = "";
                }
                else
                    MessageBox.Show("Please provide all required information!");
            }
        }


        private void mutualFolButton_Click(object sender, EventArgs e)
        {
                if (followersComboBox.SelectedIndex > -1)
                {
                    if (!followersSearchPanel.Visible)
                        followersSearchPanel.Visible = true;
                    followersComboBox_SelectedIndexChanged(this, new EventArgs());

                //PythonWorker.RunWorkerAsync("MutualFollow");
            }
            else
                    MessageBox.Show("Please choose one of the available options!");
        }
        

        private void selectRandomButton_Click(object sender, EventArgs e)
        {
            if (usersToBeSelectedNumber.Text.Length != 0)
            {
                int numberProvided = 0;
                Int32.TryParse(usersToBeSelectedNumber.Text, out numberProvided);
                if (numberProvided > usersListView.Items.Count)
                {
                    MessageBox.Show("The number you have provided is larger than the number of users on the list!");
                    usersToBeSelectedNumber.Text = "";
                }
                else
                {
                    deselectAllUsersButton_Click(sender, e);
                    Random random = new Random();
                    while (numberProvided != 0)
                    {
                        int randomInt = random.Next(usersListView.Items.Count);
                        if (!usersListView.Items[randomInt].Checked)
                        {
                            usersListView.Items[randomInt].Checked = true;
                            numberProvided--;
                        }
                    }
                }
            }
            else
                MessageBox.Show("Please provide a number!");
        }
        private void usersToBeSelectedNumber_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(usersToBeSelectedNumber.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                usersToBeSelectedNumber.Text = usersToBeSelectedNumber.Text.Remove(usersToBeSelectedNumber.Text.Length - 1);
            }
        }

        private void selectAllUsersButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < usersListView.Items.Count; i++)
            {
                usersListView.Items[i].Checked = true;
            }
        }

        private void deselectAllUsersButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < usersListView.Items.Count; i++)
            {
                usersListView.Items[i].Checked = false;
            }
        }

        private void invertSelectionButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < usersListView.Items.Count; i++)
            {
                usersListView.Items[i].Checked = !usersListView.Items[i].Checked;
            }
        }
        private void UnFollow()
        {
            CurrentUser current = CurrentUser.Get();
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + LocalizationData.DbDir + "scrapper.db" + "; Version = 3; UseUTF16Encoding = True;");
            m_dbConnection.Open();
            string where = " where ";
            foreach (string user in SelectedUsers)
            {
                where += "fullname = '"+ user+"' or ";
            }
            where = where.Remove(where.Length - 4, 4);
            SQLiteCommand command = new SQLiteCommand("select * from Users "+where, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                current.Unfollow(reader["name"].ToString());
            }
            command = new SQLiteCommand("Update Users set following_since = ''"+where, m_dbConnection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DELETE from Users where follower_since='' AND following_since=''", m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        private void unfollowButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                CurrentUser current = CurrentUser.Get();
                foreach (ListViewItem item in usersListView.CheckedItems)
                {
                    SelectedUsers.Add(item.Text);
                }
                PythonWorker.RunWorkerAsync("UnFollow");
            }
            //nonFollowersListBox.Items.Remove(user);
        }
        private void Follow()
        {
            CurrentUser current = CurrentUser.Get();
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + LocalizationData.DbDir + "scrapper.db" + "; Version = 3; UseUTF16Encoding = True;");
            m_dbConnection.Open();
            string where = " where ";
            foreach (string user in SelectedUsers)
            {
                where += "fullname = '" + user + "' or ";
            }
            where = where.Remove(where.Length - 4, 4);
            SQLiteCommand command = new SQLiteCommand("select * from Users " + where, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                current.Follow(reader["name"].ToString());
            }
            command = new SQLiteCommand("Update Users set following_since = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"'" + where, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        private void followButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                CurrentUser current = CurrentUser.Get();
                foreach (ListViewItem item in usersListView.CheckedItems)
                {
                    SelectedUsers.Add(item.Text);
                }
                PythonWorker.RunWorkerAsync("Follow");
            }
        }

        //profile panel ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void statsButton_Click(object sender, EventArgs e)
        {
            if (timeDropDown.SelectedIndex > -1)
            {
                if(!statsPanel.Visible)
                    statsPanel.Visible = true;
                statsText2.Text = "times last " + timeDropDown.SelectedItem.ToString();

                DateTime since = DateTime.Today;
                switch(timeDropDown.SelectedIndex)
                {
                    case 0:
                        since -= new TimeSpan(1, 0, 0, 0);
                        break;
                    case 1:
                        since -= new TimeSpan(7, 0, 0, 0);
                        break;
                    case 2:
                        int month = since.Month - 1;
                        int year = since.Year;
                        if (month == 0)
                        {
                            month = 12;
                            year--;
                        }
                        since -= new TimeSpan(DateTime.DaysInMonth(year, month),0,0,0);
                        break;
                    case 3:
                        since -= new TimeSpan(DateTime.IsLeapYear(since.Year-1)?1:0 +365, 0, 0, 0);
                        break;
                }
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + LocalizationData.DbDir + "scrapper.db" + "; Version = 3; UseUTF16Encoding = True;");
                m_dbConnection.Open();
                SQLiteCommand command = new SQLiteCommand("select * from Likes", m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count += DateTime.Parse(reader["liked"].ToString()).CompareTo(since) > -1 ? 1 : 0;
                }
                statsLikes.Text = count.ToString();
            }
            else
                MessageBox.Show("Please choose one of the available options!");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out? ", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                loginScreen frm2 = new loginScreen();
                frm2.FormClosed += new FormClosedEventHandler(frm2_FormClosed);
                frm2.Show();
                this.Hide();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }
        }

        private void MouseDownDrag(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            //+ log out
            this.Close();
        }
        private void UpdateDB()
        {
            dataGetter.GetDb();
        }
        private void updateDBButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to download DataBase? ", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    PythonWorker.RunWorkerAsync("UpdateDB");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
            }
        }

        private void followersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (followersSearchPanel.Visible && !isPythonRunning)
            {
                List<ListViewItem> listViewItems = new List<ListViewItem>();
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + LocalizationData.DbDir + "scrapper.db" + "; Version = 3; UseUTF16Encoding = True;");
                m_dbConnection.Open();
                string sql = "select * from Users";
                switch ((FollowersComboBoxState)followersComboBox.SelectedIndex)
                {
                    case FollowersComboBoxState.All:
                        sql = "select * from Users";
                        break;
                    case FollowersComboBoxState.OnlyFollowers:
                        sql = "select * from Users WHERE follower_since!='' AND following_since=''";
                        break;
                    case FollowersComboBoxState.OnlyFollowing:
                        sql = "select * from Users WHERE following_since!='' AND follower_since=''";
                        break;
                    case FollowersComboBoxState.Followers:
                        sql = "select * from Users WHERE follower_since!=''";
                        break;
                    case FollowersComboBoxState.Following:
                        sql = "select * from Users WHERE following_since!=''";
                        break;
                    case FollowersComboBoxState.Mutuals:
                        sql = "select * from Users WHERE follower_since!='' AND following_since!=''";
                        break;
                    default:
                        return;
                }
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["fullname"].ToString());
                    item.SubItems.Add(reader["following_since"].ToString());
                    item.SubItems.Add(reader["follower_since"].ToString());
                    listViewItems.Add(item);
                }
                m_dbConnection.Close();
                //listViewItems.Sort();// Crash przy jakims userze
                usersListView.Items.Clear();
                usersListView.Items.AddRange(listViewItems.ToArray());
            }
        }

        private void PythonLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Still running :)");
        }

        private void PythonWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            PythonWorker.ReportProgress(1);
            string input = e.Argument as string;
            Regex r = new Regex(@"\w+");
            Match match = r.Match(input);
            string FunctionToRun = match.Value;
            match = match.NextMatch();
            List<int> arg = new List<int>();
            while (match.Success)
            {
                int tmp = 0;
                if (int.TryParse(match.Value, out tmp)) {
                    arg.Add(tmp);
                }
                match = match.NextMatch();
                
            }

            switch (FunctionToRun)
            {
                case "UpdateDB":
                    UpdateDB();
                    break;
                case "LikeFresh":
                    LikeFresh(arg[0], arg[1]);
                    break;
                case "UnFollow":
                    UnFollow();
                    break;
                case "Follow":
                    Follow();
                    break;
                case "LikeLatestPhotos":
                    LikeLatestPhotos();
                    break;

                default:
                    break;
            }
            PythonWorker.ReportProgress(0);
        }

        private void PythonWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            SetPythonRunning(e.ProgressPercentage == 1);
            if (e.ProgressPercentage == 0) {
                CurrentUser current = CurrentUser.Get();
                followersComboBox_SelectedIndexChanged(this, new EventArgs());
                SelectedUsers.Clear();
            }
        }

        private void updateDBButton_Click_1(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to download DataBase? ", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    PythonWorker.RunWorkerAsync("UpdateDB");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
            }
        }


        private void killingPythonButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to stop Python? ", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //tu prosze ubic pythona
            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }
        }
    }
}
