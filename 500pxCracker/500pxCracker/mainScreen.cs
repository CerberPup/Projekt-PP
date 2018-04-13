using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _500pxCracker
{
    public partial class mainScreen : Form
    {
        public mainScreen()
        {
            InitializeComponent();
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
            //do stuff
        }

        private void mutualFolButton_Click(object sender, EventArgs e)
        {
            //do stuff
        }

        private void topUsersButton_Click(object sender, EventArgs e)
        {
            if (numberOfTopUsersTextBox.Text.Length != 0)
            {
                //do stuff
            }
            else
                MessageBox.Show("Please provide all required information!");
        }

        //profile panel ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void statsButton_Click(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit? ", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }
        }
    }
}
