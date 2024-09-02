using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MigrantId { get; set; }

        public Migrant Migrant { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public int OrderId { get; set; }

        public bool IsPaid { get; set; }
    }
}
