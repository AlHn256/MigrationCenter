using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Model
{
    public class PayList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PayerId { get; set; }

        public int RecipientId { get; set; }

        public decimal Amount { get; set; }

        public string PurposeOfPayment { get; set; }

        public DateTime Date { get; set; }

        public bool IsDebet { get; set; }

        public bool IsDelete { get; set; }
    }
}
