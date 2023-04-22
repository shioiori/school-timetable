using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

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
            Dictionary<KeyValuePair<int, int>, int> ShiftClass = new Dictionary<KeyValuePair<int, int>, int>();
            foreach (var cls in Classes)
            {
                foreach (var sh in cls.Shifts)
                {
                    var key = new KeyValuePair<int, int>(cls.ClassId, sh.MaxRoomPerShift);
                    if (ShiftClass.ContainsKey(key))
                    {
                        ShiftClass[key] ++;
                    }
                    else
                    {
                        ShiftClass.Add(key, 1);
                    }
                }
            }
            // số phòng sử dụng > số phòng trống
            foreach (var cl in ShiftClass)
            {
                if (cl.Value > cl.Key.Value)
                {
                    numberOfConflict++;
                }
            }

            // conflict lịch học sinh
            Dictionary<KeyValuePair<string, int>, int> ShiftStudent = new Dictionary<KeyValuePair<string, int>, int>();
            foreach (var cls in Classes)
            {
                foreach (var sh in cls.Shifts)
                {
                    foreach (var st in cls.Students)
                    {
                        var key = new KeyValuePair<string, int>(st.StudentId, sh.ShiftId);
                        if (ShiftStudent.ContainsKey(key))
                        {
                            ShiftStudent[key]++;
                        }
                        else
                        {
                            ShiftStudent.Add(key, 1);
                        }
                    }
                }
            }

            foreach (var cl in ShiftStudent)
            {
                if (cl.Value > 1)
                {
                    numberOfConflict++;
                }
            }

            /*            foreach (var cls in Classes)
                        {
                            if (Classes.Count(x => x.ClassName == cls.ClassName) > 1) 
                            {
                                numberOfConflict++;
                            }
                        }
            */
            this.NumberOfConflict = numberOfConflict;
            return 1 / (1.0 * (numberOfConflict + 1));
        }

        /*public void SortClassByName()
        {
            Classes.Sort((a, b) =>
            {
                return a.ClassName.CompareTo(b.ClassName);
            });
        }*/
    }
}
