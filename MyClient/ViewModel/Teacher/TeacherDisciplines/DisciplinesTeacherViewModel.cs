using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ViewModel.Teacher.TeacherDisciplines
{
    /// <summary>
    /// VM представляет страницу TeacherDisciplinesPage
    /// </summary>

    public class DisciplinesTeacherViewModel : TeacherViewModel
    {
        private List<MyModelLibrary.Discipline> _disciplines;
        public List<MyModelLibrary.Discipline> disciplines
        {
            get => _disciplines;
            set
            {
                _disciplines = value;
                RaisePropertyChanged("disciplines");
            }
        }


        #region Конструктор

        public DisciplinesTeacherViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);            
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        private new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;

            disciplines = new ProgramLogic.ServiceLogic.TeacherLogic().GetDisciplines(MyAcc);
        }
        #endregion
    }
}
