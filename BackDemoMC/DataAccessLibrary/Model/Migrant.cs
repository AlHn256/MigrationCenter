using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary
{
    public class Migrant
    {
        public Migrant()
        {
            this.UserMigrantes = new List<UserMigrant>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PassNumber { get; set; }

        public string PassSerri { get; set; }

        public bool IsDelite { get; set; }

        public List<UserMigrant> UserMigrantes { get; set; }
    }
}
