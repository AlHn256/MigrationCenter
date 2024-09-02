using System.ComponentModel;

namespace DataAccessLibrary.Enum
{
    public enum EnumOrderStatus : int
    {
        [Description("New")]
        New = 1,
        [Description("IsDone")]
        IsDone = 2,
        [Description("IsPaid")]
        IsPaid = 3,
    }
}
