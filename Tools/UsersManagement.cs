using JevoCrypt.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JevoCrypt.Tools
{
    public static class UsersManagement
    {
        public static bool Add(IList<User> users,string name,string password)
        {
            return Add(users, new User(name, password));
        }
        public static bool Remove(IList<User> users,string name)
        {
            return users.Remove(new User(name, ""));
        }

        public static bool Add(IList<User> users, User user)
        {
            bool added = false;
            if (!(users.Contains(user) || user is null))
            {
                users.Add(user);
                added = true;
            }
            return added;
        }
        public static List<bool> Add(IList<User> users,IEnumerable<User> usersToAdd)
        {
            List<bool> bools = new List<bool>();
            foreach (User user in usersToAdd)
            {
                bools.Add(Add(users, user));
            }
            return bools;
        }
        public static bool Remove(IList<User> users, User user)
        {
            return users.Remove(user);
        }
    }
}
