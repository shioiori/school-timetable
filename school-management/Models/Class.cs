using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_management.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public List<Shift> Shifts { get; set; }

        public Class()
        {
            Students = new List<Student>();
            Shifts = new List<Shift>();
        }

        public Class(Class cls)
        {
            ClassId = cls.ClassId;
            ClassName = cls.ClassName;
            Capacity = cls.Capacity;
            Students = new List<Student>(cls.Students);
            Shifts = new List<Shift>(cls.Shifts);
            Course = cls.Course;
            Teacher = cls.Teacher;
        }
    }
}
