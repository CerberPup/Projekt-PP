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
        public DateTime _Date { get; set; }
        public String _UserId { get; set; }
    }
    public class Photo
    {
        public DateTime _Date { get; set; }
        public string _PhotoId { get; set; }
        public Like[] _Likes { get; set; }
    }

    public class User
    {
        public string _Name { get; set; }
        public string _Id { get; set; }
        public DateTime _Date { get; set; }
        public Photo[] _Photos { get; set; }
    }

    public class Dane
    {
        Credentials _Credentials;
        public User _User { get; set; }
        public User[] _Followers { get; set; }
        public User[] _Following { get; set; }
        public User[] _Follow2way { get; set; }

        public void Serialize(Dane d)
        {
            //d = GenData();
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("json.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, d);
            }
        }
        public Dane Deserialize()
        {
            return JsonConvert.DeserializeObject<Dane>(File.ReadAllText("json.json"));
        }
        public Dane GenData()
        {
            return new Dane
            {
                _Credentials = new Credentials("tuLogin","tuHaslo"),
                _User = new User
                {
                    _Name = "NazwaUsera",
                    //_Date = "<PUSTE>",
                    _Id = "ID56423196874",
                    _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        }
                    }
                },
                _Followers = new User[3]
                {
                    new User{
                        _Name = "floower1",
                        _Date = DateTime.Parse("04.02.1991 00:00:00"),
                        _Id = "ID56423196874",
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        }
                    }
                    }
                    ,
                    new User
                    {
                        _Name = "floower2",
                        _Date = DateTime.Parse("04.02.1991 00:00:00"),
                        _Id = "ID56423196124",
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        }
                    }
                    },
                    new User
                    {
                        _Name = "floower3",
                        _Date = DateTime.Parse("04.02.1991 00:00:00"),
                        _Id = "ID5643311874",
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
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
                        _Name = "follolowany1",
                        _Date = DateTime.Parse("04.02.1996 00:00:00"),
                        _Id = "ID52222196874",
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        }
                    }
                    },
                    new User
                    {
                        _Name = "follolowany2",
                        _Date = DateTime.Parse("04.02.1996 00:00:00"),
                        _Id = "ID56423446874",
                       _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        }
                    }
                    }
                },
                _Follow2way = new User[2]
                {
                    new User
                    {
                        _Name = "wzajemny1",
                        _Date = DateTime.Parse("04.03.1996 00:00:00"),
                        _Id = "ID56423226874",
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        }
                    }
                    },
                    new User
                    {
                        _Name = "wzajemny2",
                        _Date = DateTime.Parse("04.01.1996 00:00:00"),
                        _Id = "ID56333346874",
                        _Photos = new Photo[2]
                    {
                        new Photo
                        {
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "1212334",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID56423196874"
                                }
                            }
                        },
                        new Photo{
                            _Date = DateTime.Parse("04.02.1926 00:00:00"),
                            _PhotoId = "",
                            _Likes = new Like[2]
                            {
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
                                    _UserId = "ID5643311874"
                                },
                                new Like{
                                    _Date = DateTime.Parse("04.02.1996 00:00:00"),
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
