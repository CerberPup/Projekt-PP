using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
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

        public mainScreen()
        {
            InitializeComponent();
        }

        private void mainScreen_Load(object sender, EventArgs e)
        {
            dataGetter.UpdateDb();
            followersComboBox.SelectedIndex = 0;
            //followers
            followersSearchPanel.Size = new Size(580, 281);
            topUsersPanel.Size = new Size(440, 185);

            followersSearchPanel.Location = new Point(21, 118);
            topUsersPanel.Location = new Point(30, 160);

            //likes
            likesPanel.Location = new Point(119, 12);
            likesPanel.Size = new Size(460, 185);

            //profile
            profilePanel.Location = new Point(119, 12);
            profilePanel.Size = new Size(440, 185);
        }

        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        private bool isPythonRunning;
        private void SetPythonRunning(bool val)
        {
            isPythonRunning = val;
            PythonLabel.Visible = val;
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

        private void numberOfTopUsersTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(numberOfTopUsersTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                numberOfTopUsersTextBox.Text = numberOfTopUsersTextBox.Text.Remove(numberOfTopUsersTextBox.Text.Length - 1);
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

        private void MutualFollow()
        {
            dataGetter.GetFollowersandFollowings();
            dataGetter.UpdateDb();
        }

        private void mutualFolButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                if (followersComboBox.SelectedIndex > -1)
                {
                    if (!followersSearchPanel.Visible)
                        followersSearchPanel.Visible = true;
                    if (topUsersPanel.Visible)
                        topUsersPanel.Visible = false;

                    PythonWorker.RunWorkerAsync("MutualFollow");
                }
                else
                    MessageBox.Show("Please choose one of the available options!");
            }
        }

        private void topUsersButton_Click(object sender, EventArgs e)
        {
            if (numberOfTopUsersTextBox.Text.Length != 0)
            {
                if (followersSearchPanel.Visible)
                    followersSearchPanel.Visible = false;
                if (!topUsersPanel.Visible)
                    topUsersPanel.Visible = true;

                int numberProvided = 0;
                Int32.TryParse(numberOfTopUsersTextBox.Text, out numberProvided);
                for(int i = 0; i < numberProvided - 1; i++)
                {
                    //set items of topUsersListBox
                }
            }
            else
                MessageBox.Show("Please provide all required information!");
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
            foreach (string user in usersListView.SelectedItems)
            {
                current.Unfollow(current.GetUserByFullName(user));
            }
        }

        private void unfollowButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                PythonWorker.RunWorkerAsync("UnFollow");
            }
            //nonFollowersListBox.Items.Remove(user);
        }
        private void Follow()
        {
            CurrentUser current = CurrentUser.Get();
            foreach (string user in usersListView.SelectedItems)
            {
                current.Follow(current.GetUserByFullName(user));
            }
        }
        private void followButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
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
                var likes = CurrentUser.Get().GetLastLikes(since);
                int count = 0;
                foreach(var user in likes)
                {
                    count += user.Value;
                }
                statsLikes.Text = count.ToString();

                timeDropDown.SelectedIndex = -1;
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
                string followedsince = "";
                string followingsince = "";
                if (followersComboBox.SelectedIndex == 0)//all
                {
                    foreach (User u in CurrentUser.Get()._Following)
                    {
                        ListViewItem item = new ListViewItem(u._FullName);
                        followedsince = u._FollowedSince.HasValue == true ? u._FollowedSince.Value.ToShortDateString() : "---";
                        followingsince = u._StartedFollowing.HasValue == true ? u._StartedFollowing.Value.ToShortDateString() : "---";
                        if (followedsince == "---" && followingsince == "---")
                        {
                            followingsince = "No DB Data";
                            followedsince = followingsince;
                        }
                        item.SubItems.Add(followedsince);
                        item.SubItems.Add(followingsince);
                        listViewItems.Add(item);
                    }
                    foreach (User u in CurrentUser.Get()._Followers)
                    {
                        if (CurrentUser.Get()._Following.Find(x => x._Id == u._Id) == null)
                        {
                            ListViewItem item = new ListViewItem(u._FullName);
                            followedsince = u._FollowedSince.HasValue == true ? u._FollowedSince.Value.ToShortDateString() : "---";
                            followingsince = u._StartedFollowing.HasValue == true ? u._StartedFollowing.Value.ToShortDateString() : "---";
                            if (followedsince == "---" && followingsince == "---")
                            {
                                followingsince = "No DB Data";
                                followedsince = followingsince;
                            }
                            item.SubItems.Add(followedsince);
                            item.SubItems.Add(followingsince);
                            listViewItems.Add(item);
                        }
                    }
                }

                if (followersComboBox.SelectedIndex == 1)//followers
                {
                    foreach (User u in CurrentUser.Get()._Followers)
                    {
                        ListViewItem item = new ListViewItem(u._FullName);
                        followedsince = u._FollowedSince.HasValue == true ? u._FollowedSince.Value.ToShortDateString() : "---";
                        followingsince = u._StartedFollowing.HasValue == true ? u._StartedFollowing.Value.ToShortDateString() : "---";
                        if (followedsince == "---" && followingsince == "---")
                        {
                            followingsince = "No DB Data";
                            followedsince = followingsince;
                        }
                        item.SubItems.Add(followedsince);
                        item.SubItems.Add(followingsince);
                        listViewItems.Add(item);
                    }
                }

                if (followersComboBox.SelectedIndex == 2)//followings
                {
                    foreach (User u in CurrentUser.Get()._Following)
                    {
                        ListViewItem item = new ListViewItem(u._FullName);
                        followedsince = u._FollowedSince.HasValue == true ? u._FollowedSince.Value.ToShortDateString() : "---";
                        followingsince = u._StartedFollowing.HasValue == true ? u._StartedFollowing.Value.ToShortDateString() : "---";
                        if (followedsince == "---" && followingsince == "---")
                        {
                            followingsince = "No DB Data";
                            followedsince = followingsince;
                        }
                        item.SubItems.Add(followedsince);
                        item.SubItems.Add(followingsince);
                        listViewItems.Add(item);
                    }
                }

                if (followersComboBox.SelectedIndex == 3)//mutual
                {
                    foreach (User u in CurrentUser.Get().MutualFollow())
                    {
                        ListViewItem item = new ListViewItem(u._FullName);
                        followedsince = u._FollowedSince.HasValue == true ? u._FollowedSince.Value.ToShortDateString() : "---";
                        followingsince = u._StartedFollowing.HasValue == true ? u._StartedFollowing.Value.ToShortDateString() : "---";
                        if (followedsince == "---" && followingsince == "---")
                        {
                            followingsince = "No DB Data";
                            followedsince = followingsince;
                        }
                        item.SubItems.Add(followedsince);
                        item.SubItems.Add(followingsince);
                        listViewItems.Add(item);
                    }
                }
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
                case "MutualFollow":
                    MutualFollow();
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
            followersComboBox_SelectedIndexChanged(this, new EventArgs());
        }
    }
}
