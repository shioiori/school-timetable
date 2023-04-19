using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace school_management.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { get; set; }   
        public string ShiftName { get; set; }
        public int MaxRoomPerShift { get; set; }

    }
}
