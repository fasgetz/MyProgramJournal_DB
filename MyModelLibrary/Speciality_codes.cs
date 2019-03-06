using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{

    /// <summary>
    /// Сущность коды специальностей
    /// </summary>

    [Serializable]
    public class Speciality_codes
    {


        public Speciality_codes(int idSpeciality, string code_speciality, string name_speciality)
        {
            this.idSpeciality = idSpeciality;
            this.code_speciality = code_speciality;
            this.name_speciality = name_speciality;
        }

        #region Свойства

        public int idSpeciality { get; set; }
        public string code_speciality { get; set; }
        public string name_speciality { get; set; }

        public List<Groups> Groups { get; set; } // Список групп

        #endregion
    }
}
