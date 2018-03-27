using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    class References
    {
        private List<Following> _followings;
        private List<Follower> _followers;
        public List<Following> followings
        {
            set
            {
                _followings = value;
            }
            get
            {
                return _followings;
            }
        }
        public List<Follower> followers
        {
            set
            {
                _followers = value;
            }
            get
            {
                return _followers;
            }
        }
    }

    class User
    {
        private int _id;
        private Credentials _creds;
        private References _refs;
        public int ID
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        public Credentials credentials
        {
            set
            {
                _creds = value;
            }
            get
            {
                return _creds;
            }
        }
        public References references
        {
            set
            {
                _refs = value;
            }
            get
            {
                return _refs;
            }
        }
    }
}
