using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _500pxCracker
{

    class Statistics
    {
        //dont know yet, will do
    }
    class FollowInfo
    {
        protected User _Parent;
        protected User _Target; //any ideas for better name?
        protected Statistics _Stats;
        protected User Parent
        {
            set
            {
                _Parent = value;
            }
            get
            {
                return _Parent;
            }
        }

        protected User Target
        {
            set
            {
                _Target = value;
            }
            get
            {
                return _Target;
            }
        }
        protected Statistics Stats
        {
            get
            {
                return _Stats;
            }
            set
            {
                _Stats = value;
            }
        }
    }


    class Follower : FollowInfo
    {
        public Follower(User parent, User target)
        {
            Parent = parent;
            Target = target;
        }

        public void Follow()
        {
            //todo: implementation. Should create the Following reference between Parent and Target. 
        }
        
    }

    class Following: FollowInfo
    {
        public Following(User parent, User target)
        {
            Parent = parent;
            Target = target;
        }

        public void Unfollow()
        {
            //todo: implementation. Should remove the Follower reference between Parent and Target. 
        }
    }
}
