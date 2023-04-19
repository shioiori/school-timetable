namespace school_management.Models
{
    public class Timetable
    {
        public List<Class> Classes { get; set; }
        public int NumberOfConflict { get; set; }

        public Timetable() 
        {
            Classes = new List<Class>();
            NumberOfConflict = 0;
        }
        // hard constrain gồm:
        // số phòng sử dụng <= số phòng trống trong 1 ca 
        // giáo viên không dạy 2 lớp cùng một lúc
        // học sinh không học 2 lớp cùng một lúc
        public double Fitness()
        {
            int numberOfConflict = 0;
            foreach (Shift shift in Init.Shifts)
            {
                // số phòng sử dụng > số phòng trống
                if (shift.MaxRoomPerShift < Classes.Where(x => x.Shift.ShiftId == shift.ShiftId).Count())
                {
                    numberOfConflict++;
                }
                
                // 2 học sinh trùng id trong 1 ca -> conflict
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                var studentInShift = Classes.Where(x => x.Shift.ShiftId == shift.ShiftId).Select(x => x.Students).ToList();
                foreach (var listStudents in studentInShift)
                {
                    foreach (var student in listStudents)
                    {
                        if (dictionary.ContainsKey(student.StudentId))
                        {
                            dictionary[student.StudentId]++;
                        }
                        else
                        {
                            dictionary.Add(student.StudentId, 1);
                        }
                    }
                }

                foreach (var student in dictionary)
                {
                    if (student.Value > 1)
                    {
                        numberOfConflict++;
                    }
                }
            }

            foreach (var cls in Classes)
            {
                if (Classes.Count(x => x.ClassName == cls.ClassName) > 1) 
                {
                    numberOfConflict++;
                }
            }
            this.NumberOfConflict = numberOfConflict;
            return 1 / (1.0 * (numberOfConflict + 1));
        }

        public void SortClassByName()
        {
            Classes.Sort((a, b) =>
            {
                return a.ClassName.CompareTo(b.ClassName);
            });
        }
    }
}
