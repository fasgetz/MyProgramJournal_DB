using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System;
using MyClient.ViewModel.Administrator.Users;

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

        #region Команды

        // Команда на добавление дисциплины
        public ICommand AddDiscipline
        {
            get
            {
                return new RelayCommand(() =>
                {
                    bool Added = MyAdminLogic.AddDiscipline(MyAcc, new MyModelLibrary.Discipline(text));

                    // Если дисциплина добавлена успешно, то обнули textbox и прогрузи список дисциплин
                    if (Added == true)
                    {
                        Disciplines = MyAdminLogic.GetDisciplines(MyAcc); // Прогружаем список дисциплин           
                        text = string.Empty; // Обнуляем textbox
                    }
                });
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
