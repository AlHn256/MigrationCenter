using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemoMC.DTO
{
    public class SimpleJurpersonDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string INN { get; set; }

        public string KPP { get; set; }

        public decimal Account { get; set; }
    }
}
