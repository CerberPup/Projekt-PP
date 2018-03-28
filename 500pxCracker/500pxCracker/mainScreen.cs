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

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            //tymczasowe wstecz

            loginScreen frm2 = new loginScreen();
            frm2.FormClosed += new FormClosedEventHandler(frm2_FormClosed);
            frm2.Show();
            this.Hide();
            
        }
        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

    }
}
