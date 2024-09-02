using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLibrary.Enum;

namespace DataAccessLibrary
{
    [Table("Buhgalter")]
    public class Buhgalter : User
    {
        public string LicensNumber { get; set; }

        public DateTime StartDateLicens { get; set; }

        public DateTime EndDateLicens { get; set; }

        public EnumQualification Qualification { get; set; }
    }
}
