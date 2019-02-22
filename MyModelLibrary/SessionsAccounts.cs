using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class SessionsAccounts
    {
        public override string ToString()
        {
            return $"Сессия + {idSession}";
        }

        // Пустой конструктор
        public SessionsAccounts()
        {

        }


        // Конструктор с параметрами
        public SessionsAccounts(int idSession, int idAccount, DateTime StartLogin, DateTime EndLogin)
        {
            this.idSession = idSession;
            this.idAccount = idAccount;
            this.StartLogin = StartLogin;
            this.EndLogin = EndLogin;
            //this.accounts = accounts;
        }

        #region Свойства сессии

        public int idSession { get; set; } // Айди сессии
        public int idAccount { get; set; } // Айди аккаунта
        public Nullable<System.DateTime> StartLogin { get; set; } // Начало сессии
        public Nullable<System.DateTime> EndLogin { get; set; } // Конец сессии

        #endregion

        //public accounts accounts { get; set; }
    }
