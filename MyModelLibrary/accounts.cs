using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public accounts()
        {
            //this.SessionsAccounts = new List<SessionsAccounts>();
        }


        public accounts(int idAccount, int idStatus, string login, string password, System.DateTime DateRegistration)
        {
            this.idAccount = idAccount;
            this.idStatus = idStatus;
            this.login = login;
            this.password = password;
            this.DateRegistration = DateRegistration;
        }

        // Конструктор для инициализации данных
        public accounts(int idAccount, int idStatus, string login, string password, System.DateTime DateRegistration, StatusAccounts StatusAccount, List<SessionsAccounts> MySessions, Users user)
        {
            this.idAccount = idAccount;
            this.idStatus = idStatus;
            this.login = login;
            this.password = password;
            this.DateRegistration = DateRegistration;
            this.StatusAccounts = StatusAccount;
            this.SessionsAccounts = MySessions;
            this.Users = user;
        }


        #region Свойства аккаунта

        public int idAccount { get; set; } // Айди аккаунта
        public int idStatus { get; set; } // Статус аккаунта
        public string login { get; set; } // Логин аккаунта
        public string password { get; set; } // Пароль аккаунта
        public Nullable<System.DateTime> DateRegistration { get; set; } // Дата регистрации аккаунта

        public virtual StatusAccounts StatusAccounts { get; set; } // Ссылка на сущность статус аккаунта
        public List<SessionsAccounts> SessionsAccounts { get; set; } // Ссылка на список сессий аккаунта
        public virtual Users Users { get; set; } // Ссылка на сущность Юзер 

        #endregion





    }
}
