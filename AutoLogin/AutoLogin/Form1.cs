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
        public Form1()
        {
            InitializeComponent();
        }

        public Dane genData()
        {
            return new Dane
            {
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

        private void button1_Click(object sender, EventArgs e)
        {

            Dane d = genData();
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("json.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, d);
            }

            Dane dd = JsonConvert.DeserializeObject<Dane>(File.ReadAllText("json.json"));
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
        public User _User { get; set; }
        public User[] _Followers { get; set; }
        public User[] _Following { get; set; }
        public User[] _Follow2way { get; set; }

    }
}
