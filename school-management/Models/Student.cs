using System.ComponentModel.DataAnnotations;

namespace school_management.Models
{
    public class Student
    {
        [Key]
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public List<Course> Courses { get; set; }
        public List<Class> Classes { get; set; }    

        public Student()
        {
            StudentId = Guid.NewGuid().ToString();
            var name = new List<string>() { "Olivia", "Dove", "John Lee", "Ruth", "Baal", "Kunikuze", "Solomon", "Agatha", "Mono",
                                            "Havert", "July", "Ann", "Mei", "Kiana", "Ruby", "Aqua", "Hoshino", "Tempest", "Jun",
                                            "Okoto", "Mary", "Rin", "Mika", "Yuu", "Tahibito", "Serene", "Judy", "August", "Chris",
                                            "Louis", "Memo", "Shiro", "Kuro", "Kyu", "Sino", "Focalor", "Andy", "Helena", "Faust",
                                            "Timmie", "Eren", "Levi", "Tomato", "Chii", "Solaem", "Bruno", "Alexander", "Sovenier"};
            Random random = new Random();
            int randomNumber = random.Next(0, name.Count());
            StudentName = name[randomNumber];
            int count = random.Next(1, 6);
            Courses = new List<Course>();
            for (int i = 0; i < count; i++)
            {
                var course = Init.Courses[random.Next(Init.Courses.Count)];
                Courses.Add(course);
            }
            Classes = new List<Class>();
        }

        public Student(Student st)
        {
            StudentId = st.StudentId; 
            StudentName = st.StudentName;
            Courses = st.Courses.Select(x => new Course(x)).ToList();
            Classes = st.Classes.Select(x => new Class(x)).ToList(); 
        }
    }
}
