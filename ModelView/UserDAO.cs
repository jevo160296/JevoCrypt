using JevoCrypt.Classes;
using JevoCrypt.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace JevoCrypt.ModelView
{
    public class UserDAO : JevoCryptDAO<User>
    {
        public override ObservableCollection<User> Items
        {
            get
            {
                if (items is null)
                {
                    items = Get();
                    items.CollectionChanged += Items_CollectionChanged;
                    foreach (User item in items)
                    {
                        item.PropertyChanged += Item_PropertyChanged;
                    }
                }
                return items;
            }
        }

        public UserDAO(UsersContainer usersContainer) : base(usersContainer) { }

        public bool AddUser(string name,string password)
        {
            bool response=UsersEdit.Add(Items, name, password);
            this.Container.SaveChanges();
            return response;
        }
        public bool RemoveUser(string name)
        {
            bool response = UsersEdit.Remove(Items, name);
            this.Container.SaveChanges();
            return response;
        }
        public bool ChangePass(string name, string oldPassword,string newPassword)
        {
            bool response = UsersEdit.Get(Items, name)?.ChangePassword(oldPassword, newPassword) ?? false;
            this.Container.SaveChanges();
            return response;
        }
        public User UnlockedUser()
        {
            foreach (User user in Items)
            {
                if (user.KeepSignIn)
                {
                    return user;
                }
            }
            return null;
        }
        public User SignIn(string name,string password,bool keepsignin=false)
        {
            User returned,temp= UsersEdit.Get(Items, name);
            returned = (temp?.SignIn(name,password,keepsignin) ?? false) ? temp : null;
            this.Container.SaveChanges();
            return returned;
        }
        public void SignOut()
        {
            UnlockedUser()?.SignOut();
            this.Container.SaveChanges();
        }

        private ObservableCollection<User> Get()
        {
            IQueryable<User> query = Context.Users.OrderBy(p => p.UserName);
            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load<User>(query);
            ObservableCollection<User> returnedData = Context.Users.Local.ToObservableCollection();
            return returnedData;
        }
    }
}
