using System.ComponentModel.DataAnnotations;

namespace school_management.Models
{
    public class Teacher
    {
        [Key]
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Course Course { get; set; }

        public Teacher()
        {
            TeacherId = Guid.NewGuid().ToString();
            var name = new List<string>() { "Olivia", "Dove", "John Lee", "Ruth", "Baal", "Kunikuze", "Solomon", "Agatha", "Mono"};
            Random random = new Random();
            int randomNumber = random.Next(0, name.Count());
            TeacherName = name[randomNumber];
        }
    }
}
