using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemoMC.DTO
{
    public class AgentRegistrationDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Mail { get; set; }
    }
}
