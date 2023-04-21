using System.ComponentModel.DataAnnotations;

namespace school_management.Models
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int ShiftRequirePerWeek { get; set; }

        public Course() { }
        public Course(Course course)
        {
            CourseId = course.CourseId;
            CourseName = course.CourseName;
            ShiftRequirePerWeek = course.ShiftRequirePerWeek;
        }

    }
}
