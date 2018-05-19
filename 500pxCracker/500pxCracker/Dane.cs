using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _500pxCracker
{
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
        public static void UpdateFollowers()
        {
            List<User> users = new List<User>();
            DirectoryInfo d = new DirectoryInfo(LocalizationData.FollowersDir);
            foreach (var file in d.GetFiles("*"))
            {
                users.Add(GetUserByFileName(file.Name));
            }
            CurrentUser.Get()._Followers = users.ToArray();
        }
        public static void UpdateFollowings()
        {
            List<User> users = new List<User>();
            DirectoryInfo d = new DirectoryInfo(LocalizationData.FollowingDir);
            foreach (var file in d.GetFiles("*"))
            {
                users.Add(GetUserByFileName(file.Name));
            }
            CurrentUser.Get()._Following = users.ToArray();
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
        }
    }
    public class LocalizationData
    {
        static public string ScriptsDir = Directory.GetCurrentDirectory()+ "\\..\\..\\..\\..\\PyScrapper\\";
        static public string MainPy = ScriptsDir + "Main.py";
        static public string GalleriesDir = ScriptsDir + "galleriesDumps\\";
        static public string LikesForPhotosDir = ScriptsDir + "likesForPhotos\\";
        static public string UserInfoDir = ScriptsDir + "UserInfo\\";
        static public string FollowersDir = UserInfoDir + "followers\\";
        static public string FollowingDir = UserInfoDir + "followings\\";
        static public string PhotosDir = ScriptsDir + "photosDumps\\";
        static public string PythonDir = "";
        static public string Python = "";
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
        public Like[] _Likes { get; set; }
    }

    public class User
    {
        public string _Name { get; set; }
        public string _FullName { get; set; }
        public int _Id { get; set; }
        public DateTime _StartedFollowing { get; set; }
        public DateTime _FollowedSince { get; set; }
        public Photo[] _Photos { get; set; }
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
        public User[] _Followers { get; set; }
        public User[] _Following { get; set; }

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
            //500pxCracker/500pxCracker/mainScreen.resx         | 1582 +++++++++++++++++++++d = GenData();
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("json.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, d);
            }
        }
        static public CurrentUser Deserialize()
        {
            return JsonConvert.DeserializeObject<CurrentUser>(File.ReadAllText("json.json"));
        }
        static CurrentUser GenData()
        {
            CurrentUser curr = new CurrentUser
            {
                _User = new User
                {
                    _Name = "username",
                    _Id = 2548265,
                    _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _PhotoUploadDate = DateTime.Parse("04.02.1992 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("22.04.2017 00:00:00"),
                                    _UserId = 96874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.2018 00:00:00"),
                                    _UserId = 564
                                }
                            }
                        },
                        new Photo{
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 564
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 231
                                }
                            }
                        }
                    }
                },
                _Followers = new User[3]
                {
                    new User{
                        _Name = "follower1",
                        _StartedFollowing = DateTime.Parse("04.02.1991 00:00:00"),
                        _FollowedSince = DateTime.Parse("03.02.1991 00:00:00"),
                        _Id = 2548265,
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 319687
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 56423
                                }
                            }
                        },
                        new Photo{
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 311874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 42319
                                }
                            }
                        }
                    }
                    }
                    ,
                    new User
                    {
                        _Name = "follower2",
                        _StartedFollowing = DateTime.Parse("04.05.1993 00:00:00"),
                        _FollowedSince = DateTime.Parse("03.08.1992 00:00:00"),
                        _Id = 6124,
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 74
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 196874
                                }
                            }
                        },
                        new Photo{
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 11874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 96874
                                }
                            }
                        }
                    }
                    },
                    new User
                    {
                        _Name = "follower3",
                        _StartedFollowing = DateTime.Parse("13.05.1991 00:00:00"),
                        _FollowedSince = DateTime.Parse("22.06.1991 00:00:00"),
                        _Id = 561874,
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _PhotoUploadDate = DateTime.Parse("04.02.1986 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.2017 00:00:00"),
                                    _UserId = 196874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.2018 00:00:00"),
                                    _UserId = 64231
                                }
                            }
                        },
                        new Photo{
                            _PhotoUploadDate = DateTime.Parse("04.02.1986 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 3311874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 42319
                                }
                            }
                        }
                    }
                    }
                },
                _Following = new User[2]
                {
                    new User
                    {
                        _Name = "followed1",
                        _FollowedSince = DateTime.Parse("03.02.2014 00:00:00"),
                        _Id = 5222,
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 3196874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 6423
                                }
                            }
                        },
                        new Photo{
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 11874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 56423196
                                }
                            }
                        }
                    }
                    },
                    new User
                    {
                        _Name = "follolowed2",
                        _StartedFollowing = DateTime.Parse("05.05.2005 05:05:05"),
                        _FollowedSince = DateTime.Parse("12.12.2012 12:12:12"),
                        _Id = 42344,
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 231968
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 6423
                                }
                            }
                        },
                        new Photo{
                            _PhotoUploadDate = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 311874
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = 423196
                                }
                            }
                        }
                    }
                    }
                }

            };
            curr.Set_Credentials(new Credentials("login", "password"));
            return curr;
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
        public User[] OneWayFollow()
        {
            List<User> response = new List<User>();
            
            foreach (User u in _Following)
            {
                if (FindFollowerByName(u._Name) == null)
                response.Add(u);
            }
            return response.ToArray();
        }

        public DateTime GetStartedFollowing(User user)
        {
            return user._StartedFollowing;
        }

        public DateTime GetFollowedSince(User user)
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

        public Dictionary<User, int> GetLastLikes(DateTime since)
        {
            Dictionary<User, int> response = new Dictionary<User, int>();
            foreach(Photo photo in _User._Photos)
            {
                foreach(Like like in photo._Likes)
                {
                    if(like._LikeDate >= since)
                    {
                        User user = GetUserById(like._UserId);
                        if (user != null)
                        {
                            if (response.ContainsKey(user))
                            {
                                response[user]++;
                            }
                            else
                            {
                                response.Add(user, 1);
                            }
                        }
                    }
                }
            }
            return response;
        }

        public User[] UnfollowNonMutual()
        {
            User[] unfollowed = OneWayFollow();
            foreach(User user in unfollowed)
            {
                //Send unfollow request to server
            }
            _Following = Array.FindAll(_Following, u => !Array.Exists(_Following,u2=>u2==u));
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
            _Following = Array.FindAll(_Following, u => u!=user);
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
