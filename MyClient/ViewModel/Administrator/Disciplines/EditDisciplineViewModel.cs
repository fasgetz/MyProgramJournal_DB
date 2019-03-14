using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;


namespace MyClient.ViewModel.Administrator.Disciplines
{
    /// <summary>
    /// VM представляет страницу редактирования названия дисциплины
    /// </summary>

    public class EditDisciplineViewModel : DisciplinesListViewModel
    {

        #region Команды

        // Команда редактирования названия дисциплины
        public ICommand EditDiscipline
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SelectedDiscipline != null)
                    {
                        // Присваиваем новые данные дисциплине
                        SelectedDiscipline = new MyModelLibrary.Discipline(SelectedDiscipline.idDiscipline, text);

                        // Посылаем данные на сервер. Если на выходе true, то редактирование прошло успешно
                        bool edit = MyAdminLogic.EditDiscipline(MyAcc, SelectedDiscipline);

                        // Если редактирование прошло успешно, то перейди на страницу списка дисциплин
                        if (edit == true)
                        {
                            // Обнови список дисциплин
                            locator.MyDisciplinesListVM.Disciplines = MyAdminLogic.GetDisciplines(MyAcc);

                            // Перейди в Page страницы дисциплин
                            navigation.Navigate("View/Administrator/Disciplines/DisciplineListPage.xaml");
                        }
                    }
                });
            }
        }

        #endregion

        #region Конструктор

        public EditDisciplineViewModel()
        {
            // Получаем аккаунт из предыдущей VM и SelectedDiscipline
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            Messenger.Default.Register<GenericMessage<MyModelLibrary.Discipline>>(this, GetDiscipline);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна            
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;   
        }

        // Вспомогательный метод для мессенджера, который проинициализирует Discipline из прошлой vm при создании текущей VM
        protected void GetDiscipline(GenericMessage<MyModelLibrary.Discipline> discipline)
        {
            SelectedDiscipline = discipline.Content;
            text = SelectedDiscipline.NameDiscipline;
        }


        #endregion
    }
}
