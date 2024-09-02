using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Enum;

namespace ApiDemoMC.DTO
{
    public class BuhgalterRegistrationDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string LicensNumber { get; set; }

        public DateTime StartDateLicens { get; set; }

        public DateTime EndDateLicens { get; set; }

        public EnumQualification Qualification { get; set; }
    }
}
