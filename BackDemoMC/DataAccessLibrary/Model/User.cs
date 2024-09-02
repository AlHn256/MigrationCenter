namespace DataAccessLibrary
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public string Pass { get; set; }

        public bool IsDelete { get; set; }
    }
}
