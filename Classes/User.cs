using JevoCrypt.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace JevoCrypt.Classes
{
    public class User: INotifyPropertyChanged
    {
        #region Atributos
        private int id;
        private string username;
        private string validationstring;
        #endregion
        #region Propiedades
        public int Id { get => id; set => id = value; }
        public string UserName 
        { 
            get => username;
            set 
            {
                if (username!=value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            } 
        }
        public string ValidationString
        {
            get => validationstring;
            set
            {
                validationstring = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Constructores
        public User() : this("", "")
        {

        }
        public User(string username, string password)
        {
            this.username = username;
            this.validationstring = GenValidationString(username, password);
        }
        #endregion
        #region Public methods
        public bool IsCorrectPassword(string password)
        {
            return ValidationString == User.GenValidationString(UserName, password);
        }
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            bool changed = false;
            if (IsCorrectPassword(oldPassword))
            {
                ValidationString = GenValidationString(username, newPassword);
                changed = true;
            }
            return changed;
        }
        #endregion
        #region Static methods
        public static string GenValidationString(string username, string password)
        {
            return Crypto.Encrypt(username + password, password);
        }
        #endregion
        #region == Overide
        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return (obj is User) && this.Equals((User)obj);
        }
        public bool Equals(User obj)
        {
            return !(obj is null) && obj.UserName == this.UserName;
        }
        public static bool operator ==(User u1, User u2)
        {
            return
                Object.ReferenceEquals(u1, u2) ||
                (u1 is null ?
                false :
                u1.Equals(u2));
        }
        public static bool operator !=(User u1, User u2)
        {
            return !(u1 == u2);
        }
        #endregion
        #region Notifications
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}