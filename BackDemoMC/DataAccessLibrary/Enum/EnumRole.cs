namespace DataAccessLibrary.Enum
{
    using System.ComponentModel;

    public enum EnumRole : int
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Jurperson")]
        Jurperson = 2,
        [Description("Agent")]
        Agent = 3,
        [Description("Buhgalter")]
        Buhgalter = 4,
    }
}
