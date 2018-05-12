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
            LocalizationData.ScriptsDir = Path.GetFullPath(LocalizationData.ScriptsDir);
            InitializePython();
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void InitializePython()
        {
            string[] paths = Environment.GetEnvironmentVariable("Path").Split(';');
            foreach (string path in paths)
            {
                if(File.Exists(path+ "python.exe"))
                {
                    Process process = new Process();
                    process.StartInfo.FileName = path+"python.exe";
                    process.StartInfo.Arguments = "--version";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (output != "")
                    {
                        if (output.Split('.')[0] == "Python 2")
                        {
                            LocalizationData.PythonDir = path;
                            LocalizationData.Python = path + "python.exe";
                            break;
                        }
                    }
                    if (err!="")
                    {
                        if (err.Split('.')[0] == "Python 2")
                        {
                            LocalizationData.PythonDir = path;
                            LocalizationData.Python = path + "python.exe";
                            break;
                        }
                    }
                    
                }
            }
            if(LocalizationData.PythonDir=="")
            {
                MessageBox.Show("You don't have Python2 in Path variable");
                return;
            }
            else
            {
                Process process = new Process();
                if (!File.Exists(LocalizationData.PythonDir + "Scripts\\pip.exe"))
                {
                    DialogResult dialogResult = MessageBox.Show("Would you like to add pip to your python installation?", "Pip Missing", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        process.StartInfo.FileName = LocalizationData.Python;
                        process.StartInfo.Arguments = LocalizationData.ScriptsDir+ "get-pip.py";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.CreateNoWindow = true;
                        process.Start();
                        process.WaitForExit();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }

                process.StartInfo.FileName = LocalizationData.Python;
                process.StartInfo.Arguments = "-m pip list";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                if (output != "")
                {
                    Regex regex = new Regex(@"(bs4|beautifulsoup4|requests)\s+(((^\d|^.|\S))+)");
                    Match match = regex.Match(output);
                    bool shouldDownloadDependencies = !match.Success;
                    Dictionary<string, Version> requiredDependencies = new Dictionary<string, Version>();
                    requiredDependencies["beautifulsoup4"] = new Version("1.0");
                    requiredDependencies["bs4"] = new Version("0.0.1");
                    requiredDependencies["requests"] = new Version("2.18.4");
                    int matches = 0;
                    while (match.Success)
                    {
                        matches++;
                        if (requiredDependencies[match.Groups[1].Value].CompareTo(new Version(match.Groups[2].Value))==1)//jest starsza
                        {
                            shouldDownloadDependencies = true;
                        }
                        match = match.NextMatch();
                    }
                    if(shouldDownloadDependencies|matches<3)
                    {
                        DialogResult dialogResult = MessageBox.Show("Would you like to install required dependecies?", "Dependencies", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            process = new Process();
                            process.StartInfo.FileName = LocalizationData.Python;
                            process.StartInfo.Arguments = "-m pip install -r " + Path.GetFullPath(LocalizationData.ScriptsDir + "Dependencies.txt");
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = false;
                            process.StartInfo.RedirectStandardOutput = true;
                            process.StartInfo.RedirectStandardError = true;
                            process.Start();
                            string ou = process.StandardOutput.ReadToEnd();
                            string err = process.StandardError.ReadToEnd();
                            process.WaitForExit();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                    
            }
            

        }
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
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + credentials.login + " " + credentials.password;// + " -offline";
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
            if (LocalizationData.PythonDir != "")
            {
                if (loginTextBox.Text.Length == 0 && pswdTextBox.Text.Length == 0)
                {
                    loginTextBox.Text = "januszek@carbtc.net";
                    pswdTextBox.Text = "haselko";
                }
                if (loginTextBox.Text.Length != 0 && pswdTextBox.Text.Length != 0)
                {
                    CurrentUser.Get().Set_Credentials(new Credentials(loginTextBox.Text, pswdTextBox.Text));
                    if (tryToLogin())
                    {
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
            else
            {
                MessageBox.Show("You don't have python poperly installed");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}