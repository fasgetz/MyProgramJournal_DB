using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Administrator.Groups
{

    /// <summary>
    /// VM представляет страницу AddGroupDisciplinesPage
    /// </summary>

    public class AddGroupDisciplineViewModel : CreateGroupViewModel
    {

        public AddGroupDisciplineViewModel()
        {
            // Получаем аккаунт и группу из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            Messenger.Default.Register<GenericMessage<MyModelLibrary.Groups>>(this, GetGroup);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new DialogService(); // Инициализируем диалоговые окна  
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            // Инициализируем свойства
            MyAcc = GetAcc.Content;
        }

        // Вспомогательный метод для мессенджера, который проинициализирует выбранную группу из прошлого vm при создании текущей vm
        private void GetGroup(GenericMessage<MyModelLibrary.Groups> GetGroup)
        {
            // Инициализируем свойства
            SelectedGroup = GetGroup.Content;
            MyGroupName = $"Дисциплины группы {SelectedGroup.GroupName}";

            //try
            //{
            //    // Получаем список студентов
            //    StudentsList = new ObservableCollection<MyModelLibrary.Users>(MyAdminLogic.GetStudentsInGroup(MyAcc, SelectedGroup.idGroup).OrderBy(i => i.StudentsGroup.NumberInJournal)); // Список студентов группы, отсортированный по номеру в журнале
            //    NotGroupedStudents = new ObservableCollection<MyModelLibrary.Users>(MyAdminLogic.GetStudentsInGroup(MyAcc, 0)); // Список студентов без группы   
            //}
            //catch (ArgumentNullException)
            //{
            //    dialog.ShowMessage($"Ошибка: вы получили пустые списки");
            //}

        }
    }
}
