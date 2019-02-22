using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class UserStatus
    {
        public UserStatus()
        {
            // this.Users = new List<Users>();
        }


        // Конструктор для инициализации
        public UserStatus(int idUserStatus, string StatusUser)
        {
            this.idUserStatus = idUserStatus;
            this.StatusUser = StatusUser;
        }

        #region Свойства 

        public int idUserStatus { get; set; }
        public string StatusUser { get; set; }


        //public virtual ICollection<Users> Users { get; set; }

        #endregion
    }
}
