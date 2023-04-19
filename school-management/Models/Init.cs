namespace school_management.Models
{
    public static class Init
    {
        public static List<Student> Students { get; set; }
        public static List<Teacher> Teachers { get; set; }
        public static List<Course> Courses { get; set; }
        public static List<Class> Classes { get; set; }
        public static List<Shift> Shifts { get; set; }
        public static int POPULATION_SIZE = 5;
        public static int NUMBER_OF_ELITE_TIMETABLE = 1;
        public static double MUTATE_RATE = 0.2;
        public static double CROSSOVER_RATE = 0.8;
        public static void Default(int numberOfStudent = 100, int numberOfTeacher = 5, int numberOfCourse = 5, int capatityOfClass = 25, int numberOfShift = 24)
        { 
            Courses = new List<Course>()
            {
                new Course(){ CourseId = "TAA1", CourseName = "Tiếng Anh A1", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "LTTQ", CourseName = "Lập trình trực quan", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "CSDL", CourseName = "Cơ sở dữ liệu", ShiftRequirePerWeek = 3 },
                new Course(){ CourseId = "MMT", CourseName = "Mạng máy tính", ShiftRequirePerWeek = 1 },
                new Course(){ CourseId = "HTTT", CourseName = "Hệ thống thông tin", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "CTDLGT", CourseName = "Cấu trúc dữ liệu và giải thuật", ShiftRequirePerWeek = 2 }
            };
            Students = new List<Student>();
            for (int i = 0; i < numberOfStudent; ++i)
            {
                var student = new Student();
                Students.Add(student);
            }
            Teachers = new List<Teacher>();
            for (int i = 0; i < numberOfTeacher; ++i)
            {
                var teacher = new Teacher();
                Teachers.Add(teacher);
            }
            Classes = new List<Class>();
            for (int i = 0; i < Courses.Count; ++i)
            {
                for (int j = 0; j < Courses[i].ShiftRequirePerWeek; ++j)
                {
                    Random r = new Random();
                    var studentInCourse = Students.Where(x => x.Courses.Select(x => x.CourseId).Contains(Courses[i].CourseId)).ToList();
                    var newClass = new Class();
                    newClass.Capacity = capatityOfClass;
                    newClass.Course = Courses[i];
                    newClass.ClassName = Courses[i].CourseName + " " + (j + 1);
                    foreach (var student in studentInCourse)
                    {
                        if (newClass.Students.Count() == newClass.Capacity)
                        {
                            Classes.Add(newClass);
                            newClass = new Class();
                            newClass.ClassName = Courses[i].CourseName + " " + (j + 1);
                            newClass.Course = Courses[i];
                            newClass.Capacity = capatityOfClass;
                        }
                        newClass.Students.Add(student);
                    }
                    if (!Classes.Contains(newClass))
                    {
                        Classes.Add(newClass);
                    }
                }
            }
            Shifts = new List<Shift>();
            for (int i = 0; i < numberOfShift; ++i)
            {
                var shift = new Shift()
                {
                    ShiftId = i + 1,
                    ShiftName = "Ca " + ((i % 4) + 1) + " thứ " + Math.Ceiling((i + 1) * 1.0 / 4),
                    MaxRoomPerShift = 5,
                };
                Shifts.Add(shift);
            }
        }


        public static Schedule InitSchedule(ref List<Class> listClasses, Shift shift)
        {
            Random random = new Random();
            int classInShift = random.Next(shift.MaxRoomPerShift);
            List<Class> classes = new List<Class>();
            for (int i = 0; i < classInShift; ++i)
            {
                if (listClasses.Count == 0) break;
                int idx = random.Next(listClasses.Count);
                classes.Add(listClasses[idx]);
                listClasses.RemoveAt(idx);
            }
            Schedule schedule = new Schedule()
            {
                Shift = shift,
                Classes = classes,
            };
            return schedule;
        }

        public static Timetable InitTimetable()
        {
            Timetable timetable = new Timetable();
            var defaultClasses = new List<Class>(Classes);
            Random rand = new Random();
            while (defaultClasses.Count > 0)
            {                
                Shift shift = Shifts[rand.Next(Shifts.Count)];
                Schedule schedule = InitSchedule(ref defaultClasses, shift);
                timetable.Schedules.Add(schedule);
            }
            return timetable;
        }
    }
}
