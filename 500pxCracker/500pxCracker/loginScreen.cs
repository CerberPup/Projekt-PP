﻿using System;
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
    public partial class loginScreen : Form
    {

        public loginScreen()
        {
            InitializeComponent();
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void loginScreen_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void loginScreen_Load(object sender, EventArgs e)
        {

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

        private void logInButton_Click(object sender, EventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}