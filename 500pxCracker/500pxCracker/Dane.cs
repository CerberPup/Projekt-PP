using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _500pxCracker
{
    public class DBUser
    {
        public DateTime? follows_since { get; set; }
        public int likes_amount { get; set; }
        public int userID { get; set; }
        public string username { get; set; }
        public DateTime? following_since { get; set; }
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
        public static void UpdateDb()
        {
            DirectoryInfo d = new DirectoryInfo(LocalizationData.DbDir);
            DateTime max = DateTime.MinValue;
            string filename = "";
            foreach (var file in d.GetFiles("*"))
            {
                string date = file.Name.Substring(7);
                string[] s = date.Split('_'); //04.02.1926 00:00:00
                s[1] = s[1].Replace('-', ':');
                DateTime dateTime = DateTime.Parse(s[0] + ' ' + s[1]);
                if (dateTime > max)
                {
                    max = dateTime;
                    filename = file.FullName;
                }

            }
            if(LocalizationData.DbCurrentDir != filename)
            {
                LocalizationData.DbCurrentDir = filename;
                DBMain dBMain = JsonConvert.DeserializeObject<DBMain>(File.ReadAllText(filename));
                List<User> followers = new List<User>();
                List<User> following = new List<User>();
                CurrentUser c = CurrentUser.Get();
                c._User._Photos = new List<Photo>();

                foreach (var user in dBMain.users)
                {
                    User u = new User()
                    {
                        _FollowedSince = user.follows_since,
                        _StartedFollowing = user.following_since,
                        _Id = user.userID,
                        _FullName = "JeszczePusto",
                        _Name = user.username
                    };
                    if(user.following_since.HasValue == true)
                    {
                        followers.Add(u);
                    }
                    if (user.follows_since.HasValue == true)
                    {
                        following.Add(u);
                    }
                }
                c._Followers = followers;
                c._Following = following;
            }
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
        public static void GetFollowers()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + credentials.login + " " + credentials.password +" -f2";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            UpdateFollowers();
        }
        public static void GetFollowings()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + credentials.login + " " + credentials.password + " -f1";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            UpdateFollowings();
        }
        public static void GetFollowersandFollowings()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + credentials.login + " " + credentials.password + " -f1 -f2";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
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
        static public string UserInfoDir = UserInfoRoot; // + user_email
        static public string FollowersDir = UserInfoDir + "followers\\";
        static public string FollowingDir = UserInfoDir + "db\\";
        static public string DbDir = UserInfoDir + "db\\";
        static public string PhotosDir = ScriptsDir + "photosDumps\\";
        static public string PythonDir = "";
        static public string Python = "";
        static public string DbCurrentDir = "";
    }
    public class Credentials
    {
        private string _login;
        private string _passwd;
        public Credentials(string login, string password)
        {
            _login = login;
            _passwd = password;
        }
        public string login
        {
            set
            {
                _login = value;
            }
            get
            {
                return _login;
            }
        }
        public string password
        {
            set
            {
                _passwd = value;
            }
            get
            {
                return _passwd;
            }
        }

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
        private Credentials _Credentials;

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
                //Serialize(GenData());
                //instance = Deserialize();
            }
            return instance;
        }

        static public void Serialize(CurrentUser d)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("json.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, d);
            }
        }
        public User[] MutualFollow()
        {
            List<User> response = new List<User>();
            foreach (User u in _Following)
            {
                User follower = FindFollowerByName(u._Name);
                if (follower != null)
                    response.Add(follower);
            }
            return response.ToArray();
        }

        public User FindFollowingByName(string Name)
        {
            foreach (User u in _Following)
            {
                if (u._Name == Name)
                    return u;
            }
            return null;
        }
        public User FindFollowerByName(string Name)
        {
            foreach (User u in _Followers)
            {
                if (u._Name == Name)
                    return u;
            }
            return null;
        }
        public List<User> OneWayFollow()
        {
            List<User> response = new List<User>();
            
            foreach (User u in _Following)
            {
                if (FindFollowerByName(u._Name) == null)
                response.Add(u);
            }
            return response;
        }

        public DateTime? GetStartedFollowing(User user)
        {
            return user._StartedFollowing;
        }

        public DateTime? GetFollowedSince(User user)
        {
            return user._FollowedSince;
        }

        public User GetUserByName(string name)
        {
            foreach (User user in _Following)
            {
                if (user._Name == name)
                {
                    return user;
                }
            }
            foreach (User user in _Followers)
            {
                if (user._Name == name)
                {
                    return user;
                }
            }
            return null;
        }

        public User GetUserById(int id)
        {
            foreach (User user in _Following)
            {
                if (user._Id== id)
                {
                    return user;
                }
            }
            foreach (User user in _Followers)
            {
                if (user._Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public Dictionary<string, int> GetLastLikes(DateTime since)
        {
            Dictionary<string, int> response = new Dictionary<string, int>();

            DirectoryInfo d = new DirectoryInfo(LocalizationData.DbDir);
            DateTime newest = DateTime.MinValue;
            DateTime oldest = DateTime.MaxValue;
            string filenameNewest = "";
            string filenameOldest = "";
            foreach (var file in d.GetFiles("*"))
            {
                string date = file.Name.Substring(7);
                string[] s = date.Split('_'); //04.02.1926 00:00:00
                s[1] = s[1].Replace('-', ':');
                DateTime dateTime = DateTime.Parse(s[0] + ' ' + s[1]);
                if (dateTime > newest)
                {
                    newest = dateTime;
                    filenameNewest = file.FullName;
                }
                if(oldest > dateTime && dateTime>since)
                {
                    oldest = dateTime;
                    filenameOldest = file.FullName;
                }

            }

            DBMain dBMainOld = JsonConvert.DeserializeObject<DBMain>(File.ReadAllText(filenameOldest));
            DBMain dBMainNew = JsonConvert.DeserializeObject<DBMain>(File.ReadAllText(filenameNewest));

            foreach (var user in dBMainNew.users)
            {
                response[user.username] = user.likes_amount;
            }
            foreach (var user in dBMainOld.users)
            {
                response[user.username] -= user.likes_amount;
            }
            return response;
        }

        public List<User> UnfollowNonMutual()
        {
            List<User> unfollowed = OneWayFollow();
            foreach(User user in unfollowed)
            {
                //Send unfollow request to server
            }
            _Following = _Following.Except(unfollowed).ToList();
                //List<User>.FindAll(_Following, u => !List<User>.Exists(_Following,u2=>u2==u));
            return unfollowed;
        }

        public void Unfollow(User user)
        {
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + _Credentials.login + " " + _Credentials.password+" -ufl " + user._Name + " -debug";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            _Following.Remove(user);
        }

        public void LikeLatestPhotos()
        {
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -p -sf1";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            foreach (User u in _Following)
            {
                Dictionary<string, DateTime> photos = new Dictionary<string, DateTime>();
                DirectoryInfo d = new DirectoryInfo(LocalizationData.PhotosDir+"User"+u._Id.ToString());
                if(d.Exists)
                    foreach (var gallery in d.GetDirectories())
                    {
                        string file = File.ReadAllText(d+"\\"+gallery.Name + "\\photos");
                        Regex regex = new Regex("\"liked\":(.*?),.*?\"feature_date\": \"(.*?)\".*?\"id\":(.*?),");
                        Match match = regex.Match(file);
                        if(match.Success && !bool.Parse(match.Groups[1].Value))
                        {
                            if(!photos.ContainsKey(match.Groups[3].Value))
                                photos.Add(match.Groups[3].Value, DateTime.Parse(match.Groups[2].Value));
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
                process.StartInfo.Arguments = "\"" + LocalizationData.MainPy + "\" " + _Credentials.login + " " + _Credentials.password + " -v" + newestPhoto.Key;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
        }

        public void LikeLikingMe(int number)
        {
            List<User> liking = new List<User>();
            foreach (Photo photo in _User._Photos)
            {
                foreach (Like like in photo._Likes)
                {
                    if (!liking.Contains(GetUserById(like._UserId)))
                    {
                        liking.Add(GetUserById(like._UserId));
                    }
                }
            }
            foreach(User user in liking)
            {
                for(int i=0; i<number;i++)
                {
                    //send like request to server
                }
            }
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
            process.WaitForExit();
        }
    }
}
