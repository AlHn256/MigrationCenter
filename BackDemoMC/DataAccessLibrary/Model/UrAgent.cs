using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary
{
    [Table("UrAgent")]
    public class UrAgent : User
    {
        public string INN { get; set; }

        public string KPP { get; set; }

        public string Rasch_Sch { get; set; } // Расчетный счет

        public string Bank_Rasch_Sch { get; set; } // Банк расчетного счета

        public string Kor_Sch { get; set; } // Корреспондентский счет

        public string Bank_Kor_Sch { get; set; }

        public string Ur_Adress { get; set; } // Юридический адрес

        public string FIO { get; set; } // ФИО лица с правом подписи

        public string GradationFIO { get; set; } // Должность лица с правом подписи

        public string Main_Document { get; set; } // На основании какого документа действует лицо с правом подписи

        public string Bik_Bank { get; set; } // БИК банка

        public string Phone { get; set; }

        public List<UserMigrant> UserMigrantes { get; set; }

        public List<Order> Orders { get; set; }
    }
}
