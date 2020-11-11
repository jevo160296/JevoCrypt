using JevoGastosCore;
using System.ComponentModel;

namespace JevoCrypt.ModelView
{
    public abstract class JevoCryptDAO<T>:DAO<T,UsersContext,UsersContainer>
        where T:class,INotifyPropertyChanged
    {
        public JevoCryptDAO(UsersContainer usersContainer)
        {
            this.Container = usersContainer;
        }
    }
}
