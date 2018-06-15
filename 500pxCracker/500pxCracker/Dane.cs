using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.Windows.Forms;

namespace _500pxCracker
{

    public class DBUser
    {
        public DateTime? follows_since { get; set; }
        public int likes_amount { get; set; }
        public int userID { get; set; }
        public string username { get; set; }
        public DateTime? following_since { get; set; }
        public string fullname { get; set; }
        public int[] likes { get; set; }
    }
    public class DBMain
    {
        public int users_amount { get; set; }
        public DBUser[] users { get; set; }
    }
    public class HttpsLink
    {
        public string https { get; set; }
    }
    public class Avatars
    {
        public HttpsLink @default {get; set;}
        public HttpsLink large { get; set;}
        public HttpsLink small { get; set;}
        public HttpsLink tiny { get; set; }
    }
    public class Pids
    {

        public static int pid;
    }



    public class JsonUser
    {
        public string username { get; set; }
        public string city { get; set; }
        public bool store_on { get; set; }
        public string userpic_https_url { get; set; }
        public string firstname { get; set; }
        public string thumbnail_background_url { get; set; }
        public string lastname { get; set; }
        public int upgrade_status { get; set; }
        public Avatars avatars { get; set; }
        public string cover_url { get; set; }
        public int usertype { get; set; }
        public int followers_count { get; set; }
        public string country { get; set; }
        public string fullname { get; set; }
        public int id { get; set; }
        public int affection { get; set; }
    }
    public class dataGetter
    {
        static public bool DBExist()
        {
            if (!File.Exists(LocalizationData.DbDir + "scrapper.db"))
            {
                SQLiteConnection.CreateFile(LocalizationData.DbDir + "scrapper.db");
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source="+ LocalizationData.DbDir + "scrapper.db" + ";Version=3;");
                m_dbConnection.Open();
                SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Users (userID INTEGER PRIMARY KEY, name text, fullname text, following_since text, follower_since text)", m_dbConnection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Likes (userID INTEGER, photoID INTEGER, liked text, FOREIGN KEY (userID) REFERENCES Users(userID), UNIQUE (userID, photoID))", m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                MessageBox.Show("Wygenerowano pustą bazę danych");
            }
            return File.Exists(LocalizationData.DbDir + "scrapper.db");
        }
        public static Process process;
        public static User GetUserByFileName(string Name)
        {
            string json = "";
            if (File.Exists(LocalizationData.FollowersDir + Name))
            {
                json = File.ReadAllText(LocalizationData.FollowersDir + Name);
            }
            else
            {
                if (File.Exists(LocalizationData.FollowingDir + Name))
                {
                    json = File.ReadAllText(LocalizationData.FollowingDir + Name);
                }
            }
            if (json=="")
            {
                return new User();
            }
            else
            {
                JsonUser jsonUser = JsonConvert.DeserializeObject<JsonUser>(json);
                User user = new User();
                user._Id = jsonUser.id;
                user._Name = jsonUser.username;
                user._FullName = jsonUser.fullname;
                return user;
            }
        }
        public static void GetDb()
        {
            Credentials _Credentials = CurrentUser.Get().Get_Credentials();
             process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + _Credentials.login + " " + _Credentials.password + " -udb";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();
        }
        public static void UpdateFollowers()
        {
            List<User> users = new List<User>();
            DirectoryInfo d = new DirectoryInfo(LocalizationData.FollowersDir);
            
            foreach (var file in d.GetFiles("*"))
            {
                users.Add(GetUserByFileName(file.Name));
            }

            CurrentUser.Get()._Followers = users;
        }
        public static void UpdateFollowings()
        {
            List<User> users = new List<User>();
            DirectoryInfo d = new DirectoryInfo(LocalizationData.FollowingDir);
            foreach (var file in d.GetFiles("*"))
            {
                users.Add(GetUserByFileName(file.Name));
            }
            CurrentUser.Get()._Following = users;
        }
        public static void GetFollowersandFollowings()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();
            process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + credentials.login + " " + credentials.password + " -f1 -f2";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();
            UpdateFollowers();
            UpdateFollowings();
        }
    }
    public class LocalizationData
    {
        static public string ScriptsDir = Directory.GetCurrentDirectory()+ "\\..\\..\\..\\..\\PyScrapper\\";
        static public string MainPy = ScriptsDir + "Main.py";
        static public string GalleriesDir = ScriptsDir + "galleriesDumps\\";
        static public string LikesForPhotosDir = ScriptsDir + "likesForPhotos\\";
        static public string UserInfoRoot = ScriptsDir + "UserInfo\\";
        static public string UserInfoDir = UserInfoRoot; // set at login
        static public string FollowersDir = UserInfoDir + "followers\\";
        static public string FollowingDir = UserInfoDir + "followings\\";
        static public string PhotosDir = ScriptsDir + "photosDumps\\";
        static public string DbDir = ""; // set at login
        static public string PythonDir = ""; // set at login
        static public string Python = ""; // set at login
        static public string DbCurrentDir = ""; // set at login
    }
    public class Credentials
    {
        public Credentials(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
        public string login { set; get; }
        public string password { set; get; }

    }
    public class Like
    {
        public DateTime _LikeDate { get; set; }
        public int _UserId { get; set; }
    }
    public class Photo
    {
        public DateTime _PhotoUploadDate { get; set; }
        public string _PhotoId { get; set; }
        public List<Like> _Likes { get; set; }
    }


    public class User
    {
        public string _Name { get; set; }
        public string _FullName { get; set; }
        public int _Id { get; set; }
        public DateTime? _StartedFollowing { get; set; }
        public DateTime? _FollowedSince { get; set; }
        public List<Photo> _Photos { get; set; }
    }

    public class CurrentUser
    {
        public List<string> UsersToRemove = new List<string>();
        public List<string> UsersToAdd = new List<string>();
        private Credentials _Credentials;
        public bool isStopped = false;
        public Credentials Get_Credentials()
        {
            return _Credentials;
        }

        public void Set_Credentials(Credentials value)
        {
            _Credentials = value;
        }

        public User _User { get; set; }
        public List<User> _Followers { get; set; }
        public List<User> _Following { get; set; }

        static CurrentUser instance;
        public static CurrentUser Get()
        {
            if(instance == null)
            {
                instance = new CurrentUser();
                instance._Followers = new List<User>();
                instance._Following = new List<User>();
                instance._User = new User();
            }
            return instance;
        }
        public void Unfollow(string userName)
        {
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + _Credentials.login + " " + _Credentials.password+" -ufl " + userName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();
        }
        public void Follow(string userName)
        {
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + _Credentials.login + " " + _Credentials.password + " -fl " + userName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();  
        }

        public void LikeLatestPhotos()
        {
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -p -sf1 -galleries 1 -pages 1 -noCleanup";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            if (isStopped)
                return;
            process.WaitForExit();
            foreach (User u in _Following)
            {
                if (isStopped)
                    return;
                Dictionary<string, DateTime> photos = new Dictionary<string, DateTime>();
                DirectoryInfo d = new DirectoryInfo(LocalizationData.PhotosDir+"User"+u._Id.ToString());
                if (d.Exists)
                {
                    foreach (var gallery in d.GetDirectories())
                    {
                        string file = File.ReadAllText(d + "\\" + gallery.Name + "\\photos");
                        Regex regex = new Regex("\"liked\":(.*?),.*?\"feature_date\": \"(.*?)\".*?\"id\":(.*?),");
                        Match match = regex.Match(file);
                        if (match.Success && !bool.Parse(match.Groups[1].Value))
                        {
                            if (!photos.ContainsKey(match.Groups[3].Value))
                                photos.Add(match.Groups[3].Value, DateTime.Parse(match.Groups[2].Value));                                
                        }
                    }
                    if(File.Exists(d+"\\photos_unassigned"))
                    {
                        string file = File.ReadAllText(d + "\\photos_unassigned");
                        Regex regex = new Regex("\"liked\":(.*?),.*?\"feature_date\": \"(.*?)\".*?\"id\":(.*?),");
                        Match match = regex.Match(file);
                        if (match.Success && !bool.Parse(match.Groups[1].Value))
                        {
                            if (!photos.ContainsKey(match.Groups[3].Value))
                                photos.Add(match.Groups[3].Value, DateTime.Parse(match.Groups[2].Value));
                        }
                    }
                }
                KeyValuePair<string, DateTime> newestPhoto = new KeyValuePair<string,DateTime>(null,DateTime.MinValue);
                foreach (var p in photos)
                {
                    if (newestPhoto.Value < p.Value)
                        newestPhoto = p;
                }
                process = new Process();
                process.StartInfo.FileName = LocalizationData.Python;
                process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -v" + newestPhoto.Key + " -noCleanup";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                Pids.pid = process.Id;
                process.WaitForExit();
            }
            process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -offline";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();
        }


        public void getLocalUserPhotos()
        {
            _User._Photos = new List<Photo>();
            Photo photo = null;
            Process process;
            process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -p -u " + _User._Name + " -noCleanup";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            if (isStopped)
                return;
            process.WaitForExit();
            Dictionary<string, DateTime> MyPhotos = new Dictionary<string, DateTime>();
            DirectoryInfo dir = new DirectoryInfo(LocalizationData.PhotosDir + "User" + _User._Id.ToString());
            if (dir.Exists)
            {
                foreach (var gallery in dir.GetDirectories())
                {
                    string file = File.ReadAllText(dir + "\\" + gallery.Name + "\\photos");
                    Regex regex = new Regex("\"liked\":(.*?),.*?\"feature_date\": \"(.*?)\".*?\"id\":(.*?),");
                    MatchCollection matches = regex.Matches(file);
                    foreach (Match match in matches)
                    {
                        if (match.Success)
                        {
                            if (!MyPhotos.ContainsKey(match.Groups[3].Value))
                            {
                                photo = new Photo();
                                photo._PhotoId = match.Groups[3].Value;
                                photo._PhotoUploadDate = DateTime.Parse(match.Groups[2].Value);
                                MyPhotos.Add(match.Groups[3].Value, DateTime.Parse(match.Groups[2].Value));
                                _User._Photos.Add(photo);
                            }
                        }
                    }
                }
            }
            if (File.Exists(dir + "\\photos_unassigned"))
            {
                string file = File.ReadAllText(dir + "\\photos_unassigned");
                Regex regex = new Regex("\"liked\":(.*?),.*?\"feature_date\": \"(.*?)\".*?\"id\":(.*?),");
                //Match match = regex.Match(file);
                MatchCollection matches = regex.Matches(file);
                foreach (Match match in matches)
                {
                    if (match.Success && !bool.Parse(match.Groups[1].Value))
                    {
                        if (!MyPhotos.ContainsKey(match.Groups[3].Value))
                        {
                            photo = new Photo();
                            photo._PhotoId = match.Groups[3].Value;
                            photo._PhotoUploadDate = DateTime.Parse(match.Groups[2].Value);
                            MyPhotos.Add(match.Groups[3].Value, DateTime.Parse(match.Groups[2].Value));
                            _User._Photos.Add(photo);
                        }
                    }
                }
            }
        }

        public void LikeLikingMe(int number)
        {
            Dictionary<string, string> liking = new Dictionary<string, string>();
            Photo newestPhoto = new Photo();
            newestPhoto._PhotoUploadDate = DateTime.MinValue;
            Process process;
            DirectoryInfo dir;
            getLocalUserPhotos();
            //getting likes for our photos
            foreach (Photo pht in _User._Photos)
            {
                if (isStopped)
                    return;
                process = new Process();
                process.StartInfo.FileName = LocalizationData.Python;
                process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -l " + pht._PhotoId + " -noCleanup";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                Pids.pid = process.Id;
                process.WaitForExit();
            }

            //selecting most recent photo
            foreach (Photo pht in _User._Photos)
            {
                if (newestPhoto._PhotoUploadDate < pht._PhotoUploadDate)
                {
                    newestPhoto = pht;
                }
            }
            if (isStopped)
                return;
            dir = new DirectoryInfo(LocalizationData.LikesForPhotosDir);
            string file = File.ReadAllText(dir + "\\" + newestPhoto._PhotoId.Replace(" ", string.Empty));
            Regex regex = new Regex("\"username\": \"(.*?)\".*?\"id\": (.*?),");
            MatchCollection matches = regex.Matches(file);
            Dictionary<string, string> likesForNewestPhoto = new Dictionary<string, string>();
            newestPhoto._Likes = new List<Like>();
            foreach (Match match in matches)
            {
                if (isStopped)
                    return;
                if (match.Success)
                {
                    if (!likesForNewestPhoto.Keys.Contains(match.Groups[1].Value))
                    {
                        Like like = new Like();
                        likesForNewestPhoto.Add(match.Groups[1].Value, match.Groups[2].Value);
                        newestPhoto._Likes.Add(like);
                    }
                }
            }

            //adding user who liked our most recent photos, key-username, value-id
            foreach (var like in likesForNewestPhoto)
            {
                if (isStopped)
                    return;
                if (!liking.Keys.Contains(like.Key) & !like.Key.Contains("-"))
                {
                    liking.Add(like.Key, like.Value);
                }
            }

            //donwloading photos
            string joinedUser = String.Join(" ", liking.Keys);
            process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -p -u " + joinedUser + " -galleries 1 -pages 1 -noCleanup";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();
            //parsing downloaded json and sending like requests
            foreach (var user in liking)
            {
                if (isStopped)
                    return;
                DirectoryInfo d = new DirectoryInfo(LocalizationData.PhotosDir + "User" + user.Value);
                if (d.Exists)
                {
                    foreach (var gallery in d.GetDirectories())
                    {
                        string likingFiles = File.ReadAllText(d + "\\" + gallery.Name + "\\photos");
                        Regex regexp = new Regex("\"liked\":(.*?),.*?\"feature_date\": \"(.*?)\".*?\"id\":(.*?),");
                        MatchCollection likingMatches = regexp.Matches(likingFiles);
                        int iterMax;

                        if (likingMatches.Count >= number)
                        {
                            iterMax = number;

                        }
                        else
                        {
                            iterMax = likingMatches.Count;
                        }
                        for (int i = 0; i < iterMax; i++)
                        {

                            if (likingMatches[i].Success && !bool.Parse(likingMatches[i].Groups[1].Value))
                            {
                      
                                process = new Process();
                                process.StartInfo.FileName = LocalizationData.Python;
                                process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -v " + likingMatches[i].Groups[3].Value + " -noCleanup"; ;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.CreateNoWindow = true;
                                process.Start();
                                Pids.pid = process.Id;
                                process.WaitForExit();
                            }

                        }
                    }

                    if (File.Exists(d + "\\photos_unassigned"))
                    {
                        string likingFiles = File.ReadAllText(d + "\\photos_unassigned");
                        Regex regexp = new Regex("\"liked\":(.*?),.*?\"feature_date\": \"(.*?)\".*?\"id\":(.*?),");
                        MatchCollection likingMatches = regexp.Matches(likingFiles);
                        int iterMax;

                        if (likingMatches.Count >= number)

                        {
                            iterMax = number;
                        }
                        else
                        {
                            iterMax = likingMatches.Count;
                        }

                        for (int i = 0; i < iterMax; i++)
                        {
                            if (likingMatches[i].Success && !bool.Parse(likingMatches[i].Groups[1].Value))
                            {
                                if (isStopped)
                                    return;
                                process = new Process();
                                process.StartInfo.FileName = LocalizationData.Python;
                                process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -v " + likingMatches[i].Groups[3].Value + " -noCleanup"; ;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.CreateNoWindow = true;
                                process.Start();
                                Pids.pid = process.Id;
                                process.WaitForExit();
                            }
                        }
                    }
                }
            }
            process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -offline";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();
        }
        internal void LikeFresh(int mode, int amount)
        {
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password;
            switch (mode)
            {
                case 0:
                    process.StartInfo.Arguments += " -vf ";
                    break;
                case 1:
                    process.StartInfo.Arguments += " -vu ";
                    break;
            }
            process.StartInfo.Arguments += amount.ToString();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Pids.pid = process.Id;
            process.WaitForExit();
        }
    }
}
