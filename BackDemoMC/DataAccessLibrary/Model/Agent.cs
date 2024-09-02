using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary
{
    [Table("Agent")]
    public class Agent : User
    {
        public string Phone { get; set; }
    }
}
