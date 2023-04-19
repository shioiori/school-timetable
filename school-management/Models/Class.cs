using System.ComponentModel.DataAnnotations;

namespace school_management.Models
{
    public class Class
    {
        [Key]
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }

        public Class()
        {
            ClassId = Guid.NewGuid().ToString();
            Students = new List<Student>();
        }
    }
}
