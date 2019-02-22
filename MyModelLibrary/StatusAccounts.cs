using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class StatusAccounts
    {
        public StatusAccounts()
        {
            //this.accounts = new List<accounts>();
        }


        // Инициализируем свойства в конструкторе
        public StatusAccounts(int idStatus, string StatusAccount)
        {
            this.idStatus = idStatus;
            this.StatusAccount = StatusAccount;
        }



        #region Список свойств

        public int idStatus { get; set; } // Айди статуса 
        public string StatusAccount { get; set; } // Название статуса 
        //public List<accounts> accounts { get; set; } // Список с аккаунтами

        #endregion


    }
}
