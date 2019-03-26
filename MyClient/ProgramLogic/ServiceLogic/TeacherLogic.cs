using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ProgramLogic.ServiceLogic
{

    /// <summary>
    /// Класс предназначен для организации логики работы преподавателей с сервером
    /// </summary>

    class TeacherLogic : CommonLogic
    {

        // Метод, который задает оценку (С проверкой статуса)
        public bool SetAttendance(MyModelLibrary.accounts MyAcc, MyModelLibrary.Attendance Attendance)
        {
            try
            {
                using (client = new MyService.TransferServiceClient())
                    return client.SetAttendance(MyAcc, Attendance);
            }
            catch (FaultException<AccountService.AccountConnectedException> ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return false; // Возвращаем false, если неудачно
        }

        // Метод, который выдает преподавателю список его групп, занятия у которых он ведет (С проверкой на учителя/администратора)       
        public List<MyModelLibrary.GroupDisciplines> GetTeacherDiscipline(MyModelLibrary.accounts MyAcc)
        {
            try
            {
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetTeacherDiscipline(MyAcc);
                }
            }
            catch (FaultException<AccountService.AccountConnectedException> ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return null; // Возвращаем null в случае ошибки
        }

        // Метод, который прогружает список занятий и (оценки по занятиям)
        public List<MyModelLibrary.LessonsDate> GetAttendancesFromJournal(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines SelectedTeacherAtivitie)
        {
            try
            {
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetAttendancesFromJournal(MyAcc, SelectedTeacherAtivitie);
                }
            }
            catch (FaultException<AccountService.AccountConnectedException> ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return null; // Возвращаем null в случае ошибки

        }
    }
}
