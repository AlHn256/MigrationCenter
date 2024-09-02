// <copyright file="RegistrationDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataAccessLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RegistrationDTO
    {
        public string Name { get; set; }

        public string INN { get; set; }

        public string KPP { get; set; }

        public string Rasch_Sch { get; set; } // Расчетный счет

        public string Bank_Rasch_Sch { get; set; } // Банк расчетного счета

        public string Kor_Sch { get; set; } // Корреспондентский счет

        public string Bank_Kor_Sch { get; set; } // Банк корреспондентского счета

        public string Phone { get; set; }

        public string Ur_Adress { get; set; } // Юридический адрес

        public string FIO { get; set; }

        public string GradationFIO { get; set; } // Должность лица с правом подписи

        public string Main_Document { get; set; } // На основании какого документа действует лицо с правом подписи

        public string Bik_Bank { get; set; }

        public string Password { get; set; }

        public string Mail { get; set; }
    }
}
