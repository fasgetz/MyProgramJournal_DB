using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ViewModel.Administrator.Disciplines
{

    /// <summary>
    /// VM представляет страницу списка дисциплин
    /// </summary>

    public class DisciplinesListViewModel : AdministratorViewModel
    {

        #region Свойства

        // Список дисциплин
        private List<MyModelLibrary.Discipline> _Disciplines;
        public List<MyModelLibrary.Discipline> Disciplines
        {
            get
            {
                return _Disciplines;
            }
            set
            {
                _Disciplines = value;
                RaisePropertyChanged("Disciplines");
            }
        }

        #endregion

        #region Конструктор

        public DisciplinesListViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна            
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;            
            Disciplines = MyAdminLogic.GetDisciplines(MyAcc); // Прогружаем список дисциплин            
        }
        #endregion

    }
}
