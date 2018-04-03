using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace AutoLogin
{
    public partial class Form1 : Form
    {
        private bool zaladowano = false;
        private String ReadHtmlPage()
        {

            //setup some variables

            

            //setup some variables end

            //String result = "";
            //String strPost = "email=" + username + "&password=" + password;
            //StreamWriter myWriter = null;

            //HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("https://500px.com/login");

           
            return "wow";
        }
        public Form1()
        {
            InitializeComponent();
        }

        public void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            String ausername = "wowr130013@gmail.com";
            String apassword = "HFXKV7x2hYUF";
            var webBrowser = sender as WebBrowser;
            try
            {
                HtmlDocument doc = webBrowser1.Document;
                HtmlElement userName = doc.GetElementById("email");// These not worked because ID of the elements were hidden so they are here to show which of these did not work.
                HtmlElement pass = doc.GetElementById("password");
                var links = webBrowser1.Document.GetElementsByTagName("form");
                HtmlElement submit = null;
                foreach (HtmlElement link in links)
                {
                    if (link.GetAttribute("className") == "button submit medium full unified_signup__submit_button")
                    {
                        submit = link;
                        //do something
                    }
                }
                //HtmlElement submit = webBrowser1.Document.Forms[0].Document.All["PIN"].Parent.Parent.Parent.NextSibling.FirstChild;

                userName.SetAttribute("value", ausername);
                pass.SetAttribute("value", apassword);
                submit.InvokeMember("Click");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show(webBrowser.Url.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string input = "https://500px.com/login";


            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);

            webBrowser1.Navigate(input);*/
            //MessageBox.Show(ReadHtmlPage());
            //email password
            Dane d = new Dane
            {
                Username = "haha",
                data = "wololo"
            };

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("json.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, d);
            }

            Dane dd = JsonConvert.DeserializeObject<Dane>(File.ReadAllText("json.json"));



            string pass = "HFXKV7x2hYUF";
            string user = "wowr130013@gmail.com";
            string site = "https://500px.com/login";

            /*
            var request = (HttpWebRequest)WebRequest.Create("https://500px.com/login");

            var postData = "email="+user;
            postData += "&password=" + pass;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            */

            MessageBox.Show(ReadHtmlPage());


        }
    }

    public class Dane
    {
        public string Username { get; set; }
        public string data { get; set; }
    }
}
