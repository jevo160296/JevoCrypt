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
        public User SignIn(string name,string password)
        {
            User returned,temp= UsersEdit.Get(Items, name);
            returned = (temp?.IsCorrectPassword(password) ?? false) ? temp : null;
            return returned;
        }

        private ObservableCollection<User> Get()
        {
            IQueryable<User> query = Context.Users.OrderByDescending(p => p.UserName);
            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load<User>(query);
            ObservableCollection<User> returnedData = Context.Users.Local.ToObservableCollection();
            return returnedData;
        }
    }
}
