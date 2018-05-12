﻿using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace _500pxCracker
{
    public class dataGetter
    {
        public static void GetFollowers()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + credentials.login + " " + credentials.password +" -f2";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }
        public static void GetFollowings()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + credentials.login + " " + credentials.password + " -f1";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }
        public static void GetFollowersandFollowings()
        {
            Credentials credentials = CurrentUser.Get().Get_Credentials();
            Process process = new Process();
            process.StartInfo.FileName = LocalizationData.Python;
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + credentials.login + " " + credentials.password + " -f1 -f2";
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
        public String _UserId { get; set; }
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
        public string _Id { get; set; }
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
                    _Id = "ID56423196874",
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
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.2018 00:00:00"),
                                    _UserId = "ID56423196874"
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
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                        _Id = "ID56423196874",
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
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                        _Id = "ID56423196124",
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
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                        _Id = "ID5643311874",
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
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.2018 00:00:00"),
                                    _UserId = "ID56423196874"
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
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                        _Id = "ID52222196874",
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
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                        _Id = "ID56423446874",
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
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _LikeDate = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
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

        public User GetUserById(string id)
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
            process.StartInfo.FileName = "python.exe";
            process.StartInfo.Arguments = LocalizationData.MainPy + " " + _Credentials.login + " " + _Credentials.password+" -ufl " + user._Name + " -debug";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            _Following = Array.FindAll(_Following, u => u!=user);
        }

        public void LikeLatestPhotos()
        {
            foreach(User u in _Following)
            {
                //Send like request to server
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
    }
}
