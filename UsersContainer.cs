using System;
using System.Collections.Generic;
using System.Text;
using EntityCoreBasics;
using JevoCrypt.ModelView;
using Microsoft.EntityFrameworkCore;

namespace JevoCrypt
{
    public class UsersContainer:Container<UsersContext>
    {
        #region Atributos
        private UserDAO userDAO;
        #endregion
        #region Propiedades
        public UserDAO UserDAO
        {
            get
            {
                if (userDAO is null)
                {
                    userDAO = new UserDAO(this);
                }
                return userDAO;
            }
        }
        #endregion
        #region Constructores
        public UsersContainer(string folderpath)
        {
            this.Context = new UsersContext(folderpath);
        }
        #endregion
        #region Metodos
        public void SaveChanges()
        {
            this.Context.SaveChanges();
            this.Context.Database.ExecuteSqlRaw("VACUUM");
        }
        #endregion
    }
}
