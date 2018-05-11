using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
        bool tryToLogin()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();

            string logDir = LocalizationData.UserInfoDir + "log_" + credentials.login;
            /*if (File.Exists(logDir))
                File.Delete(logDir);*/

            Process process = new Process();
            process.StartInfo.FileName = "python.exe";
            process.StartInfo.Arguments = LocalizationData.MainPy+" " + credentials.login + " " + credentials.password;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            //string output = process.StandardOutput.ReadToEnd();
            string output = "";
            if (File.Exists(logDir))
            {
                output = File.ReadAllText(logDir);
            }
            Regex regex = new Regex(@"Logged in as: (\w*)");
            Match match = regex.Match(output);
            string name = "";
            string id = "";
            if (match.Success)
            {
                name = match.Groups[1].Value;
            }
            else
            {
                return false;
            }
            regex = new Regex(@"User ID: (\w*)");
            match = regex.Match(output);
            if (match.Success)
            {
                id = match.Groups[1].Value;
            }
            else
            {
                return false;
            }
            CurrentUser.Get()._User = new User();
            CurrentUser.Get()._User._Id = id;
            CurrentUser.Get()._User._Name = name;
            //process.WaitForExit();
            return true;
        }
        private void CheckEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                logInButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void logInButton_Click(object sender, EventArgs e)
        {
            //dorobic warunek gdy login jest bialymi znakami?
            //loginTextBox.Text[0] != ' ' \

            //tutaj logowanie przez aplikację

            if (loginTextBox.Text.Length == 0 && pswdTextBox.Text.Length == 0)
            {
                loginTextBox.Text = "januszek@carbtc.net";
                pswdTextBox.Text = "haselko";
            }
            if (loginTextBox.Text.Length != 0 && pswdTextBox.Text.Length != 0)
            {
                CurrentUser.Get().Set_Credentials(new Credentials(loginTextBox.Text, pswdTextBox.Text));
                if (tryToLogin()) {
                    mainScreen frm2 = new mainScreen();
                    frm2.FormClosed += new FormClosedEventHandler(frm2_FormClosed);
                    frm2.Show();
                    this.Hide();
                }
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