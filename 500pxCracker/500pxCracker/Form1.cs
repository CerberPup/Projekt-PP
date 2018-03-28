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
    public partial class loginScreen : Form
    {
        public loginScreen()
        {
            InitializeComponent();
        }

        private void loginScreen_Load(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //dorobic warunek gdy login jest bialymi znakami?
            //loginTextBox.Text[0] != ' ' \

            //tutaj logowanie przez aplikację
            if (loginTextBox.Text.Length != 0 && pswdTextBox.Text.Length != 0)
            {
                mainScreen frm2 = new mainScreen();
                frm2.FormClosed += new FormClosedEventHandler(frm2_FormClosed);
                frm2.Show();
                this.Hide();
            }
            else
            {
                warningTextbox.Visible = true;
            }
        }
        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void loginTextBox_TextChanged(object sender, EventArgs e)
        {
            if (warningTextbox.Visible)
                warningTextbox.Visible = false;
        }

        private void pswdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (warningTextbox.Visible)
                warningTextbox.Visible = false;
        }
    }
    
}
