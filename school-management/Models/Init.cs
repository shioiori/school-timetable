namespace school_management.Models
{
    public static class Init
    {
        public static List<Student> Students { get; set; }
        public static List<Course> Courses { get; set; }
        public static List<Class> Classes { get; set; }
        public static List<Shift> Shifts { get; set; }
        public static int POPULATION_SIZE = 5;
        public static int NUMBER_OF_ELITE_TIMETABLE = 1;
        public static double MUTATE_RATE = 0.1;
        public static double CROSSOVER_RATE = 0.9;
        public static void Default(int numberOfStudent = 100, int maxRoomPerShift = 5, int numberOfCourse = 5, int capatityOfClass = 25, int numberOfShift = 24)
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
            Classes = new List<Class>();
            for (int i = 0; i < Courses.Count; ++i)
            {
                Random r = new Random();
                var studentInCourse = Students.Where(x => x.Courses.Select(x => x.CourseId).Contains(Courses[i].CourseId)).ToList();
                var newClass = new Class() {
                    Capacity = capatityOfClass,
                    Course = Courses[i],
                    ClassName = Courses[i].CourseName + " " + Init.RandomInt(),
                };
                foreach (var student in studentInCourse)
                {
                    if (newClass.Students.Count() == newClass.Capacity)
                    {
                        Classes.Add(newClass);
                        newClass = new Class()
                        {
                            ClassName = Courses[i].CourseName + " " + Init.RandomInt(),
                            Course = Courses[i],
                            Capacity = capatityOfClass,
                        };
                    }
                    newClass.Students.Add(student);
                }
                if (!Classes.Contains(newClass))
                {
                    Classes.Add(newClass);
                }
            }
            Shifts = new List<Shift>();
            for (int i = 0; i < numberOfShift; ++i)
            {
                var shift = new Shift()
                {
                    ShiftId = i + 1,
                    ShiftName = "Ca " + ((i % 4) + 1) + " thứ " + Math.Ceiling((i + 1) * 1.0 / 4),
                    MaxRoomPerShift = maxRoomPerShift,
                };
                Shifts.Add(shift);
            }
        }


        public static Timetable InitTimetable()
        {
            Timetable timetable = new Timetable();
            Random rand = new Random();
            var defaultClasses = new List<Class>(Classes);
            foreach (var cls in defaultClasses)
            {
                for (int i = 0; i < cls.Course.ShiftRequirePerWeek; ++i)
                {
                    var sh = Shifts[RandomInt(Shifts.Count)];
                    if (!cls.Shifts.Contains(sh))
                    {
                        cls.Shifts.Add(sh);
                    }
                }
            }
            timetable.Classes = defaultClasses;
            return timetable;
        }

        public static int RandomInt()
        {
            Random r = new Random();
            return r.Next();
        }

        public static int RandomInt(int a)
        {
            Random r = new Random();
            return r.Next(a);
        }

        public static int RandomInt(int a, int b)
        {
            Random r = new Random();
            return r.Next(a, b);
        }

        public static double RandomDouble()
        {
            Random r = new Random();
            return r.NextDouble();
        }
    }
}
