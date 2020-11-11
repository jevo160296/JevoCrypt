using JevoCrypt.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace JevoCrypt.Tools
{
    public static class UsersEdit
    {
        public static bool Add(IList<User> users, string name, string password) => Add(users, new User(name, password));
        public static bool Remove(IList<User> users, string name) => users.Remove(new User(name, ""));

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
        public static bool Remove(IList<User> users, User user)=> users.Remove(user);
        public static bool UserExists(IList<User> users, string name) => users.Where(p => p.UserName == name).Count() > 0;
        public static User Get(IList<User> users, string name) => UserExists(users, name) ? users.First(p => p.UserName == name) : null;
    }
}
