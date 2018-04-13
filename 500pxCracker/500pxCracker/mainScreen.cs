using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
            nonFollowersPanel.Size = new Size(440, 185);
            mutualFollowersPanel.Size = new Size(440, 185);
            mutualFollowersPanel.Location = new Point(30, 160);
            topUsersPanel.Size = new Size(440, 185);
            topUsersPanel.Location = new Point(30, 160);
        }

        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
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
        private void likeFreshButton_Click(object sender, EventArgs e)
        {
            if (photoTypeDropDown.SelectedIndex > -1 && freshPhotosNumberTextBox.Text.Length!=0)
            {
                //do stuff
            }
            else
                MessageBox.Show("Please provide all required information!");
        }

        private void likeLatestButton_Click(object sender, EventArgs e)
        {
            //do stuff
        }

        private void likePhotosButton_Click(object sender, EventArgs e)
        {
            if(photosNumberTextBox.Text.Length!=0)
            {
                //do stuff
            }
            else
                MessageBox.Show("Please provide all required information!");
        }

        //followers panel ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void nonFolButton_Click(object sender, EventArgs e)
        {
            if (!nonFollowersPanel.Visible)
                nonFollowersPanel.Visible = true;
            if (mutualFollowersPanel.Visible)
                mutualFollowersPanel.Visible = false;
            if (topUsersPanel.Visible)
                topUsersPanel.Visible = false;

            var items = nonFollowersListBox.Items;
            //here add items to the nonFollowersListBox ~~~~~~~~~~~~~~
            items.Add("Perls");
            items.Add("Honey");
            items.Add("Aloe");
        }

        private void mutualFolButton_Click(object sender, EventArgs e)
        {
            if (nonFollowersPanel.Visible)
                nonFollowersPanel.Visible = false;
            if (!mutualFollowersPanel.Visible)
                mutualFollowersPanel.Visible = true;
            if (topUsersPanel.Visible)
                topUsersPanel.Visible = false;

            //here add items to mutualCheckedListBox ~~~~~~~~~~~~~~
        }

        private void topUsersButton_Click(object sender, EventArgs e)
        {
            if (numberOfTopUsersTextBox.Text.Length != 0)
            {
                if (nonFollowersPanel.Visible)
                    nonFollowersPanel.Visible = false;
                if (mutualFollowersPanel.Visible)
                    mutualFollowersPanel.Visible = false;
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
                if (numberProvided > nonFollowersListBox.Items.Count)
                {
                    MessageBox.Show("The number you have provided is larger than the number of users on the list!");
                    usersToBeSelectedNumber.Text = "";
                }
                else
                {
                    //tutaj dodać funkcję random albo co tam chcemy, bo ostatecznie nie wiem :v
                    for (int i = 0; i < numberProvided; i++)
                        nonFollowersListBox.SetItemChecked(i, true);
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
            for (int i = 0; i < nonFollowersListBox.Items.Count; i++)
            {
                nonFollowersListBox.SetItemChecked(i, true);
            }
        }

        private void deselectAllUsersButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nonFollowersListBox.Items.Count; i++)
            {
                nonFollowersListBox.SetItemChecked(i, false);
            }
        }
        private void unfollowButton_Click(object sender, EventArgs e)
        {
            //do stuff with selected items
        }

        //profile panel ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void statsButton_Click(object sender, EventArgs e)
        {
            //for meh for now
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
        
    }
}
