namespace school_management.Models
{
    public class Timetable
    {
        public List<Schedule> Schedules { get; set; }
        public int NumberOfConflict { get; set; }

        public Timetable() 
        {
            Schedules = new List<Schedule>();
            NumberOfConflict = 0;
        }
        // hard constrain gồm:
        // số phòng sử dụng <= số phòng trống trong 1 ca 
        // giáo viên không dạy 2 lớp cùng một lúc
        // học sinh không học 2 lớp cùng một lúc
        public double Fitness()
        {
            int numberOfConflict = 1;
            foreach (Schedule schedule in Schedules)
            {
                // số phòng sử dụng > số phòng trống
                if (schedule.Shift.MaxRoomPerShift > schedule.Classes.Count())
                {
                    numberOfConflict++;
                }

                /*// giáo viên dạy nhiều lớp cùng 1 lúc
                var teacherInShift = schedule.Classes.Select(x => x.Teacher).ToList();
                teacherInShift.OrderBy(x => x.TeacherId);
                for (int i = 1; i < teacherInShift.Count; i++)
                {
                    if (teacherInShift[i].TeacherId == teacherInShift[i - 1].TeacherId)
                    {
                        numberOfConflict++;
                    }
                }*/

                // 2 học sinh trùng id trong 1 ca -> conflict
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                var studentInShift = schedule.Classes.Select(x => x.Students).ToList();
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
            this.NumberOfConflict = numberOfConflict;
            return 1 / (1.0 * (numberOfConflict + 1));
        }
    }
}
