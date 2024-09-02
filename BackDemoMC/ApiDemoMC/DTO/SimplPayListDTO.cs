using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemoMC.DTO
{
    public class SimplPayListDTO
    {
        public int Id { get; set; }

        public int PayerId { get; set; }

        public int RecipientId { get; set; }

        public decimal Amount { get; set; }

        public string PurposeOfPayment { get; set; }
    }
}
