using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        public enum TimerIndex
        {
            UpdateDB = 0,
            LikeFresh,
            LikeUpcoming,
            LikeLatestPhotos,
            Size
        };

        public struct TimerProperites
        {
            public ManualResetEvent mrse;
            public bool autoReset;
            public bool shouldReset;
            private bool enabled;
            public long resetValue;
            public long timeleft;
            private bool isrunning;

            public bool Enabled { get => enabled; set => enabled = value; }

            public TimerProperites(bool EEnabled, long ResetValue, bool AutoReset = false)
            {
                mrse = new ManualResetEvent(false);
                autoReset = AutoReset;
                enabled = EEnabled;
                resetValue = ResetValue;
                timeleft = ResetValue;
                isrunning = false;
                shouldReset = false;

            }

            internal void SetValues(bool Enabled, long ResetValue, bool AutoReset = false)
            {
                autoReset = AutoReset;
                this.Enabled = Enabled;
                resetValue = ResetValue;
                timeleft = ResetValue;
            }
            internal void DecreseTimer()
            {
                while (!Enabled&&!shouldReset)
                    Thread.Sleep(1000);
                timeleft--;
            }
            internal void Toggle() => Enabled = !Enabled;

            internal void ShouldRestart(bool v1) => shouldReset = v1;

            internal void Reset() => timeleft = resetValue;
            internal bool IsRunning() => isrunning;
            internal void SetRunning(bool v1) => isrunning = v1;

            internal void RandomDelay(int v1, int v2)
            {
                Random rnd = new Random();
                timeleft = rnd.Next(v1, v2);
            }
        }

        public TimerProperites[] TimersProperties = new TimerProperites[(int)TimerIndex.Size];
        private List<Thread> Timers = new List<Thread>();
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
                timersPanelButton.MouseEnter += OnMouseEntertimersPanelButton;
                timersPanelButton.MouseLeave += OnMouseLeavetimersPanelButton;


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

                saveTimersButton.MouseEnter += OnMouseEntersaveTimersButton;
                saveTimersButton.MouseLeave += OnMouseLeavesaveTimersButton;
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

        new Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        //Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        new Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }

        public bool shutdown { get; private set; }

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

        #region Guziki
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

        private void OnMouseEntertimersPanelButton(object sender, EventArgs e)
        {
            timersPanelButton.BackColor = Color.FromArgb(196, 196, 196);
        }

        private void OnMouseLeavetimersPanelButton(object sender, EventArgs e)
        {
            timersPanelButton.BackColor = Color.White;
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

        private void OnMouseEntersaveTimersButton(object sender, EventArgs e)
        {
            saveTimersButton.BackColor = Color.FromArgb(255, 160, 160);
        }
        private void OnMouseLeavesaveTimersButton(object sender, EventArgs e)
        {
            saveTimersButton.BackColor = Color.FromArgb(255, 205, 205);
        }

        //-----------------------------------------------------------------------------------------
        #endregion
        
        private void mainScreen_Load(object sender, EventArgs e)
        {
            followersComboBox.SelectedIndex = 0;
            timeDropDown.SelectedIndex = 0;
            //this.Size = new Size(771, 454);
            //this.StartPosition = FormStartPosition.CenterParent;

            //followers
            followersPanel.Size = new Size(600, 504);
            followersPanel.Location = new Point(119, 12);

            followersSearchPanel.Size = new Size(580, 410);
            followersSearchPanel.Location = new Point(21, 86);

            usersListView.Size = new Size(359, 373);

            //likes
            likesPanel.Location = new Point(119, 12);
            likesPanel.Size = new Size(460, 185);

            //profile
            profilePanel.Location = new Point(119, 12);
            profilePanel.Size = new Size(440, 185);

            //timers
            timersPanel.Location = new Point(119, 12);
            timersPanel.Size = new Size(559, 390);

            DryftTimePicker.Format = DateTimePickerFormat.Custom;
            DryftTimePicker.CustomFormat = "HH:mm";
            DryftTimePicker.ShowUpDown = true;
            DBdateTimePicker.Format = DateTimePickerFormat.Custom;
            DBdateTimePicker.CustomFormat = "HH:mm";
            DBdateTimePicker.ShowUpDown = true;
            lastestDateTimePicker.Format = DateTimePickerFormat.Custom;
            lastestDateTimePicker.CustomFormat = "HH:mm";
            lastestDateTimePicker.ShowUpDown = true;
            upcomingDateTimePicker.Format = DateTimePickerFormat.Custom;
            upcomingDateTimePicker.CustomFormat = "HH:mm";
            upcomingDateTimePicker.ShowUpDown = true;
            freshDateTimePicker.Format = DateTimePickerFormat.Custom;
            freshDateTimePicker.CustomFormat = "HH:mm";
            freshDateTimePicker.ShowUpDown = true;

            //startPanel
            startPanel.Location = new Point(113, 62);
            startPanel.Size = new Size(648, 379);
            pictureBox1.Location = new Point(200, 140);


            photoTypeDropDown.SelectedIndex = 0;
            timeDropDown.SelectedIndex = 0;
            followersComboBox.SelectedIndex = 0;
            DBcomboBox.SelectedIndex = Properties.Settings.Default.UpdateDBCyclic ? 1 : 0;
            comboBox1.SelectedIndex = Properties.Settings.Default.FreshCyclic ? 1 : 0;
            comboBox2.SelectedIndex = Properties.Settings.Default.UpcomingCyclic ? 1 : 0;
            comboBox3.SelectedIndex = Properties.Settings.Default.LatestCyclic ? 1 : 0;
            DryftTimePicker.Value = Properties.Settings.Default.DryftTimePicker;
            PythonTimeDelay.Text = Properties.Settings.Default.PythonDelay;
            DBdateTimePicker.Value = Properties.Settings.Default.UpdateDBDateTime;
            freshDateTimePicker.Value = Properties.Settings.Default.FreshDateTime;
            upcomingDateTimePicker.Value = Properties.Settings.Default.UpcomingDateTime;
            lastestDateTimePicker.Value = Properties.Settings.Default.LastestDateTime;
            freshTimerTextBox.Text = Properties.Settings.Default.FreshNumber;
            upcomingTimerTextBox.Text = Properties.Settings.Default.UpcomingNumber;

            InitTimers();
        }
        /*private void OverrideTimer(TimerIndex timerIndex, double interval)
        {
            Timers[(int)timerIndex] = new Thread(interval);
            switch (timerIndex)
            {
                case TimerIndex.UpdateDB:
                    Timers[(int)timerIndex].Elapsed += TimerUpdateDB;
                    break;
                case TimerIndex.LikeFresh:
                    Timers[(int)timerIndex].Elapsed += TimerLikeFresh;
                    break;
                case TimerIndex.LikeUpcoming:
                    Timers[(int)timerIndex].Elapsed += TimerLikeUpcoming;
                    break;
                case TimerIndex.LikeLatestPhotos:
                    Timers[(int)timerIndex].Elapsed += TimerLikeLatestPhotos;
                    break;
            }
        }*/
        private void InitTimers()
        {
            for (int i = 0; i < (int)TimerIndex.Size; i++)
            {
                switch (i)
                {
                    case (int)TimerIndex.UpdateDB:
                        Timers.Add(new Thread(UpdateDBThread));
                        TimersProperties[i] = new TimerProperites(false,5000);
                        break;
                    case (int)TimerIndex.LikeFresh:
                        Timers.Add(new Thread(LikeFreshThread));
                        TimersProperties[i] = new TimerProperites(false, 5000);
                        break;
                    case (int)TimerIndex.LikeUpcoming:
                        Timers.Add(new Thread(LikeUpcomingThread));
                        TimersProperties[i] = new TimerProperites(false, 5000);
                        break;
                    case (int)TimerIndex.LikeLatestPhotos:
                        Timers.Add(new Thread(LikeLatestThread));
                        TimersProperties[i] = new TimerProperites(false, 5000);
                        break;
                }
                Timers[i].Start();
            }
        }

        public TimerProperites GetTimerProperties(TimerIndex timerIndex)
        {
            return TimersProperties[(int)timerIndex];
        }

        public void UpdateDBThread()
        {
            int id = (int)TimerIndex.UpdateDB;
            while (true)
            {
                TimersProperties[id].SetRunning(false);
                TimersProperties[id].mrse.WaitOne();
                TimersProperties[id].mrse.Reset();
                TimersProperties[id].SetRunning(true);
                while (TimersProperties[id].timeleft > 0)
                {
                    TimersProperties[id].DecreseTimer();
                    if (TimersProperties[id].shouldReset)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                if (TimersProperties[id].shouldReset)
                {
                    TimersProperties[id].ShouldRestart(false);
                    continue;
                }
                try
                {
                    PythonWorker.RunWorkerAsync("UpdateDB");
                    TimersProperties[id].Reset();
                }
                catch (Exception)
                {
                    TimersProperties[id].RandomDelay(5,20);
                    TimersProperties[id].mrse.Set();
                }
            }
        }
        public void LikeFreshThread()
        {
            int id = (int)TimerIndex.LikeFresh;
            while (true)
            {
                TimersProperties[id].SetRunning(false);
                TimersProperties[id].mrse.WaitOne();
                TimersProperties[id].mrse.Reset();
                TimersProperties[id].SetRunning(true);
                while (TimersProperties[id].timeleft > 0)
                {
                    TimersProperties[id].DecreseTimer();
                    if (TimersProperties[id].shouldReset)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                if (TimersProperties[id].shouldReset)
                {
                    TimersProperties[id].ShouldRestart(false);
                    continue;
                }
                try
                {
                    PythonWorker.RunWorkerAsync("LikeFresh 0 " + int.Parse(Properties.Settings.Default.FreshNumber));
                    TimersProperties[id].Reset();
                }
                catch (Exception)
                {
                    TimersProperties[id].RandomDelay(5, 20);
                    TimersProperties[id].mrse.Set();
                }
            }
        }
        public void LikeUpcomingThread()
        {
            int id = (int)TimerIndex.LikeUpcoming;
            while (true)
            {
                TimersProperties[id].SetRunning(false);
                TimersProperties[id].mrse.WaitOne();
                TimersProperties[id].mrse.Reset();
                TimersProperties[id].SetRunning(true);
                while (TimersProperties[id].timeleft > 0)
                {
                    TimersProperties[id].DecreseTimer();
                    if (TimersProperties[id].shouldReset)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                if (TimersProperties[id].shouldReset)
                {
                    TimersProperties[id].ShouldRestart(false);
                    continue;
                }
                try
                {
                    PythonWorker.RunWorkerAsync("LikeFresh 1 " + int.Parse(Properties.Settings.Default.UpcomingNumber));
                    TimersProperties[id].Reset();
                }
                catch (Exception)
                {
                    TimersProperties[id].RandomDelay(5, 20);
                    TimersProperties[id].mrse.Set();
                }
            }
        }
        public void LikeLatestThread()
        {
            int id = (int)TimerIndex.LikeLatestPhotos;
            while (true)
            {
                TimersProperties[id].SetRunning(false);
                TimersProperties[id].mrse.WaitOne();
                TimersProperties[id].mrse.Reset();
                TimersProperties[id].SetRunning(true);
                while (TimersProperties[id].timeleft > 0)
                {
                    TimersProperties[id].DecreseTimer();
                    if (TimersProperties[id].shouldReset)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                if (TimersProperties[id].shouldReset)
                {
                    TimersProperties[id].ShouldRestart(false);
                    continue;
                }
                try
                {
                    PythonWorker.RunWorkerAsync("LikeLatestPhotos");
                    TimersProperties[id].Reset();
                }
                catch (Exception)
                {
                    TimersProperties[id].RandomDelay(5, 20);
                    TimersProperties[id].mrse.Set();
                }
            }
        }
        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        private void SetTimerActive(TimerIndex index,bool enabled,bool cyclic)
        {
            Timers[(int)index].Start();
            //Timers[(int)index].AutoReset = cyclic;
        }
        public void SetPythonRunning(bool val)
        {
            if (!shutdown)
            {
            CurrentUser.Get().isStopped = false;
            for (int i = 0; i < (int)TimerIndex.Size; i++)
            {
                TimersProperties[i].Toggle();
                if (!val && TimersProperties[i].autoReset)
                    TimersProperties[i].mrse.Set();
            }
            isPythonRunning = val;
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () {
                PythonLabel.Visible = true;
                PythonLabel.Visible = val;
                pythonRunningPic.Visible = val;
                killingPythonButton.Visible = val;
            });
            
            if (val == false)
                Console.Beep();
            }
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            if(!profilePanel.Visible)
                profilePanel.Visible = true;
            if (followersPanel.Visible)
                followersPanel.Visible = false;
            if (likesPanel.Visible)
                likesPanel.Visible = false;
            if (timersPanel.Visible)
                timersPanel.Visible = false;
            if (startPanel.Visible)
                startPanel.Visible = false;
        }

        private void followersButton_Click(object sender, EventArgs e)
        {
            if(!followersPanel.Visible)
                followersPanel.Visible = true;
            if (profilePanel.Visible)
                profilePanel.Visible = false;
            if (likesPanel.Visible)
                likesPanel.Visible = false;
            if (timersPanel.Visible)
                timersPanel.Visible = false;
            if (startPanel.Visible)
                startPanel.Visible = false;
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
            if (timersPanel.Visible)
                timersPanel.Visible = false;
            if (startPanel.Visible)
                startPanel.Visible = false;
        }

        private void timersPanelButton_Click(object sender, EventArgs e)
        {
            if (!timersPanel.Visible)
                timersPanel.Visible = true;
            if (likesPanel.Visible)
                likesPanel.Visible = false;
            if (followersPanel.Visible)
                followersPanel.Visible = false;
            if (profilePanel.Visible)
                profilePanel.Visible = false;
            if (startPanel.Visible)
                startPanel.Visible = false;
        }

        private void OnlyFloat_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            Regex regex = new Regex(@"^([0-9]{0,4}((\.[0-9]{0,2})|(\.?)))$");
            if (!regex.Match(txtBox.Text).Success && txtBox.Text.Length!=0)
            {
                MessageBox.Show("Please enter only float numbers.\nExample: 1234.12");
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
            }
        }
        private void OnlyNumber_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            if (!new Regex(@"^([0-9])+$").Match(txtBox.Text).Success && txtBox.Text.Length != 0)
            {
                MessageBox.Show("Please enter only numbers.");
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
            }
        }
        

        //likes panel ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void LikeFresh(int SelectedIndex, int Number)
        {
            CurrentUser.Get().LikeFresh(SelectedIndex, Number);
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
            if (!CurrentUser.Get().isStopped)
                MessageBox.Show("Successfully liked all the photos");
        }
        private void likeLatestButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                PythonWorker.RunWorkerAsync("LikeLatestPhotos");
            }
        }

        private void LikeLikingMe(int number)
        {
            CurrentUser.Get().LikeLikingMe(number);
            if (!CurrentUser.Get().isStopped)
              MessageBox.Show("Successfully liked all the photos");
        }



        private void likePhotosButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning)
            {
                if (photosNumberTextBox.Text.Length != 0)
                {
                      PythonWorker.RunWorkerAsync("LikeLikingMe-"+ int.Parse(photosNumberTextBox.Text));

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

                if(!numberOfXTextBox.Visible)
                    numberOfXTextBox.Visible = true;

                if(followersComboBox.SelectedIndex == 0)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " all users on your list.";
                if(followersComboBox.SelectedIndex == 1)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Only Followers on your list..";
                if(followersComboBox.SelectedIndex == 2)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Only Followings on your list.";
                if (followersComboBox.SelectedIndex == 3)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Followers on your list.";
                if (followersComboBox.SelectedIndex == 4)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Followings on your list.";
                if (followersComboBox.SelectedIndex == 5)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Mutual Followers on your list.";
                

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
                where += "fullname = '"+ user.Replace("'","''")+"' or ";
            }
            where = where.Remove(where.Length - 4, 4);
            SQLiteCommand command = new SQLiteCommand("select * from users where fullname in(select fullname from users" + where + " ) and following_since!=''" , m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                if (current.isStopped)
                {
                    reader.Close();
                    return;
                }
                current.Unfollow(reader["name"].ToString());
            }
            command = new SQLiteCommand("Update Users set following_since = '' where fullname in(select fullname from users" + where + " ) and following_since!=''", m_dbConnection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DELETE from Users where follower_since='' AND following_since=''", m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        private void unfollowButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning && usersListView.CheckedItems.Count>0)
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
                where += "fullname = '" + user.Replace("'","''") + "' or ";
            }
            where = where.Remove(where.Length - 4, 4);
            SQLiteCommand command = new SQLiteCommand("select * from users where fullname in(select fullname from users" + where+ " ) and following_since=''", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                if (current.isStopped)
                {
                    reader.Close();
                    return;
                }
                current.Follow(reader["name"].ToString());
            }
            command = new SQLiteCommand("Update Users set following_since = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where fullname in(select fullname from users" + where+ " ) and following_since==''", m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        private void followButton_Click(object sender, EventArgs e)
        {
            if (!isPythonRunning && usersListView.CheckedItems.Count > 0)
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
                if (!dataGetter.DBExist())
                {
                    if (!isPythonRunning)
                    {
                        DialogResult dialogResult = MessageBox.Show("You need to dowload DataBase first. \nDo you want to do it now?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            PythonWorker.RunWorkerAsync("UpdateDB");
                            return;
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
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
                m_dbConnection.Close();
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
        private long TimetoS(DateTimePicker dateTimePicker)
        {
            DateTime d = Properties.Settings.Default.DryftTimePicker;
            Random random = new Random();
            long toReturn = (((dateTimePicker.Value.Hour * 60) + dateTimePicker.Value.Minute) * 60);
            int randomized =((d.Hour * 60) + d.Minute) * 60;
            toReturn += random.Next(-randomized, randomized);
            return toReturn <=10?10:toReturn;
        }

        private void saveTimersButton_Click(object sender, EventArgs e)
        {
            if (freshCheckBox.Checked && freshTimerTextBox.Text.Length == 0 && !upcomingCheckBox.Checked)
                MessageBox.Show("Please provide number of Fresh photos to be liked!");

            else if(upcomingCheckBox.Checked && upcomingTimerTextBox.Text.Length==0 && !freshCheckBox.Checked)
                MessageBox.Show("Please provide number of Upcoming photos to be liked!");

            else if(upcomingCheckBox.Checked && upcomingTimerTextBox.Text.Length == 0 && freshCheckBox.Checked && freshTimerTextBox.Text.Length == 0)
                MessageBox.Show("Please provide number of photos to be liked!");

            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to change the timers?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (TimerIndex i = 0; i < TimerIndex.Size; i++)
                    {
                        switch (i)
                        {
                            case TimerIndex.UpdateDB:
                                TimersProperties[(int)i].SetValues(BDcheckBox.Checked, TimetoS(DBdateTimePicker), DBcomboBox.SelectedIndex == 1);
                                break;
                            case TimerIndex.LikeFresh:
                                TimersProperties[(int)i].SetValues(freshCheckBox.Checked, TimetoS(freshDateTimePicker), comboBox1.SelectedIndex == 1);
                                break;
                            case TimerIndex.LikeUpcoming:
                                TimersProperties[(int)i].SetValues(upcomingCheckBox.Checked, TimetoS(upcomingDateTimePicker), comboBox2.SelectedIndex == 1);
                                break;
                            case TimerIndex.LikeLatestPhotos:
                                TimersProperties[(int)i].SetValues(lastestCheckBox.Checked, TimetoS(lastestDateTimePicker), comboBox3.SelectedIndex == 1);
                                break;
                            default:
                                break;
                        }
                        if (TimersProperties[(int)i].IsRunning() && !TimersProperties[(int)i].Enabled)
                        {
                            TimersProperties[(int)i].ShouldRestart(true);
                        }
                        else if (!TimersProperties[(int)i].IsRunning() && TimersProperties[(int)i].Enabled)
                            TimersProperties[(int)i].mrse.Set();

                    }
                    CurrentUser.Get().PythonDryft = PythonTimeDelay.Text.Length==0?"0": PythonTimeDelay.Text;
                    Properties.Settings.Default.DryftTimePicker = DryftTimePicker.Value;
                    Properties.Settings.Default.PythonDelay = PythonTimeDelay.Text;
                    Properties.Settings.Default.UpdateDBDateTime = DBdateTimePicker.Value;
                    Properties.Settings.Default.FreshDateTime = freshDateTimePicker.Value;
                    Properties.Settings.Default.UpcomingDateTime = upcomingDateTimePicker.Value;
                    Properties.Settings.Default.LastestDateTime = lastestDateTimePicker.Value;
                    Properties.Settings.Default.FreshNumber = freshTimerTextBox.Text;
                    Properties.Settings.Default.UpcomingNumber = upcomingTimerTextBox.Text;
                    Properties.Settings.Default.UpdateDBCyclic = DBcomboBox.SelectedIndex == 0 ? false : true;
                    Properties.Settings.Default.FreshCyclic = comboBox1.SelectedIndex == 0 ? false : true;
                    Properties.Settings.Default.UpcomingCyclic = comboBox2.SelectedIndex == 0 ? false : true;
                    Properties.Settings.Default.LatestCyclic = comboBox3.SelectedIndex == 0 ? false : true;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Successfully saved the timers!");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
            }
        }

        private void freshTimerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(freshPhotosNumberTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                freshPhotosNumberTextBox.Text = freshPhotosNumberTextBox.Text.Remove(freshPhotosNumberTextBox.Text.Length - 1);
            }
        }

        private void upcomingTimerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(freshPhotosNumberTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                freshPhotosNumberTextBox.Text = freshPhotosNumberTextBox.Text.Remove(freshPhotosNumberTextBox.Text.Length - 1);
            }
        }

        private void mainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Thread t in Timers)
            {
                t.Abort();
            }
            if (isPythonRunning)
            {
                shutdown = true;//Workaround. python jak się zamyka odświerza kontrolki które nie istnieją.
                killingPythonButton_Click(this, new EventArgs());
            }
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
            if (followersSearchPanel.Visible && !isPythonRunning && dataGetter.DBExist())
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

                switch (followersComboBox.SelectedIndex)
                {
                    case 0:
                    default:
                        break;
                }
                if (followersComboBox.SelectedIndex == 0)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " all users on your list.";
                if (followersComboBox.SelectedIndex == 1)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Only Followers on your list..";
                if (followersComboBox.SelectedIndex == 2)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Only Followings on your list.";
                if (followersComboBox.SelectedIndex == 3)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Followers on your list.";
                if (followersComboBox.SelectedIndex == 4)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Followings on your list.";
                if (followersComboBox.SelectedIndex == 5)
                    numberOfXTextBox.Text = "You have " + usersListView.Items.Count + " Mutual Followers on your list.";
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
                case "LikeLikingMe":
                    LikeLikingMe(arg[0]);
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
                PythonWorker.WorkerSupportsCancellation = true;
                PythonWorker.CancelAsync();
                CurrentUser.Get().isStopped = true;
                if (PythonWorker.IsBusy == true)
                {
                    PythonWorker.Dispose();
                    try
                    {
                        Process.GetProcessById(Pids.pid).Kill();
                    }
                    catch (Exception)
                    {

                    }

                }
                if (Directory.Exists(LocalizationData.GalleriesDir))
                {
                    Directory.Delete(LocalizationData.GalleriesDir, true);
                }
                if (Directory.Exists(LocalizationData.LikesForPhotosDir))
                {
                    Directory.Delete(LocalizationData.LikesForPhotosDir, true);
                }
                if (Directory.Exists(LocalizationData.PhotosDir))
                {
                    Directory.Delete(LocalizationData.PhotosDir, true);
                }

              

            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }
        }
    }
}
