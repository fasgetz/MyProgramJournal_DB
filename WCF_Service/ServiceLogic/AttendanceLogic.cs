using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service.DataBase;
using WCF_Service.DataBase.Repositories;

namespace WCF_Service.ServiceLogic
{

    /// <summary>
    /// Дополнительный класс, который предоставляет методы работы с БД в контексте оценок
    /// </summary>

    class AttendanceLogic
    {

        // Метод, который добавляет добавленному в группу студенту итоговую оценку по предметам, а также добавляет его к занятиям
        public static void AddStudentsAttendances(StudentsGroup Student)
        {



            using (MyDB db = new MyDB())
            {
                // Находим список всех дисциплин группы
                var Disciplines = db.GroupDisciplines.Where(i => i.IdGroup == Student.idGroup);

                // Теперь добавляем в итоговые отметки
                foreach (var item in Disciplines)
                {
                    // Добавляем итоговую отметку по дисциплине
                    db.FinalAttendances.Add(new FinalAttendances(item.idTeacherActivities, Student.IdStudent));

                    // Теперь находим весь список занятий по дисциплине и добавляем занятиям оценки
                    foreach (var mark in db.LessonsDate.Where(i => i.IdTeacherActivities == item.idTeacherActivities))
                    {
                        // Добавляем оценку в занятии
                        db.Attendance.Add(new Attendance(mark.IdLesson, Student.IdStudent));
                    }

                }

                db.SaveChanges(); // Сохраняем БД
            }

        }

        // Метод, который заводит итоговые оценки всей группе (При добавлении дисциплины группе)
        public static void SetDisciplineFinalAttendances(GroupDisciplines GroupDiscipline)
        {
            // создаем репозиторий для работы с бд
            using (MyDB db = new MyDB())
            {               
                // Ищем всех студентов группы
                var students = db.StudentsGroup.Where(i => i.idGroup == GroupDiscipline.IdGroup);

                // Если количество студентов больше 0 (То есть кто-то в группе есть, то добавь им итоговые оенки
                if (students.Count() != 0)
                {
                    // Добавляем итоговую оценку каждому студенты группы
                    foreach (var item in students)
                    {
                        // Добавляем итоговую оценку в бд
                        db.FinalAttendances.Add(new FinalAttendances(GroupDiscipline.idTeacherActivities, item.IdStudent));
                    }

                    db.SaveChanges(); // Сохраняем бд
                }
            }

        }

    }
}
