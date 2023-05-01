namespace school_management.Models
{
    public static class Init
    {
        public static List<Student> Students { get; set; }
        public static List<Course> Courses { get; set; }
        public static List<Class> Classes { get; set; }
        public static List<Shift> Shifts { get; set; }

        public static Timetable Timetable { get; set; }

        public static int POPULATION_SIZE = 5;
        public static int NUMBER_OF_ELITE_TIMETABLE = 1;
        public static double MUTATE_RATE = 0.1;
        public static double CROSSOVER_RATE = 0.9;
        public static Random r = new Random();
        public static void Default(int numberOfStudent = 1000, int maxRoomPerShift = 50, int numberOfCourse = 5, int capatityOfClass = 70, int numberOfShift = 24)
        {
            if (Classes != null) return;
            Courses = new List<Course>()
            {
                new Course(){ CourseId = "TAA1", CourseName = "Tiếng Anh A1", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "LTTQ", CourseName = "Lập trình trực quan", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "CSDL", CourseName = "Cơ sở dữ liệu", ShiftRequirePerWeek = 3 },
                new Course(){ CourseId = "MMT", CourseName = "Mạng máy tính", ShiftRequirePerWeek = 1 },
                new Course(){ CourseId = "HTTT", CourseName = "Hệ thống thông tin", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "CTDLGT", CourseName = "Cấu trúc dữ liệu và giải thuật", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "LTAR", CourseName = "Lập trình Android", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "ATBMTT", CourseName = "An toàn bảo mật thông tin", ShiftRequirePerWeek = 2 },
                new Course(){ CourseId = "TTNT", CourseName = "Trí tuệ nhân tạo", ShiftRequirePerWeek = 2 }
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
                int idx = 1;
                var studentInCourse = Students.Where(x => x.Courses.Select(x => x.CourseId).Contains(Courses[i].CourseId)).ToList();
                var newClass = new Class() {
                    ClassId = idx,
                    Capacity = capatityOfClass,
                    Course = Courses[i],
                    ClassName = Courses[i].CourseName + " " + idx,
                };
                idx++;
                foreach (var student in studentInCourse)
                {
                    if (newClass.Students.Count() == newClass.Capacity)
                    {
                        Classes.Add(newClass);
                        newClass = new Class()
                        {
                            ClassId = idx,
                            ClassName = Courses[i].CourseName + " " + idx,
                            Course = Courses[i],
                            Capacity = capatityOfClass,
                        };
                        idx++;
                    }
                    newClass.Students.Add(student);
                }
                if (!Classes.Contains(newClass))
                {
                    Classes.Add(newClass);
                    idx++;
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
            var defaultClasses = new List<Class>();
            foreach (var cls in Classes)
            {
                var newCls = new Class(cls);
                for (int i = 0, shiftRequired = newCls.Course.ShiftRequirePerWeek; i < shiftRequired; ++i)
                {
                    var sh = Shifts[RandomInt(Shifts.Count)];
                    if (!newCls.Shifts.Contains(sh))
                    {
                        newCls.Shifts.Add(sh);
                    }
                }
                defaultClasses.Add(newCls);
            }
            timetable.Classes = defaultClasses;
            return timetable;
        }

        public static int RandomInt()
        {
            return r.Next();
        }

        public static int RandomInt(int a)
        {
            return r.Next(a);
        }

        public static int RandomInt(int a, int b)
        {
            return r.Next(a, b);
        }

        public static double RandomDouble()
        {
            return r.NextDouble();
        }
    }
}
