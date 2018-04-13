using System;
using System.IO;
using Newtonsoft.Json;

namespace _500pxCracker
{
    class Credentials
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
        Credentials _Credentials;
        public User _User { get; set; }
        public User[] _Followers { get; set; }
        public User[] _Following { get; set; }

        public void Serialize(CurrentUser d)
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
        public CurrentUser Deserialize()
        {
            return JsonConvert.DeserializeObject<CurrentUser>(File.ReadAllText("json.json"));
        }
        public CurrentUser GenData()
        {
            return new CurrentUser
            {
                _Credentials = new Credentials("login","password"),
                _User = new User
                {
                    _Name = "username",
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
                        _StartedFollowing = DateTime.Parse("15.03.1991 00:00:00"),
                        _FollowedSince = DateTime.Parse("26.02.1991 00:00:00"),
                        _Id = "ID5643311874",
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
                },
                _Following = new User[2]
                {
                    new User
                    {
                        _Name = "followed1",
                        _StartedFollowing = DateTime.Parse("04.02.2015 00:00:00"),
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
        }

    }
}
