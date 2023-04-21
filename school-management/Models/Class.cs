﻿using System.ComponentModel.DataAnnotations;

namespace school_management.Models
{
    public class Class
    {
        [Key]
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public List<Shift> Shifts { get; set; }

        public Class()
        {
            ClassId = Guid.NewGuid().ToString();
            Students = new List<Student>();
            Shifts = new List<Shift>();
        }

        public Class(Class cls)
        {
            ClassId = cls.ClassId;
            ClassName = cls.ClassName;
            Capacity = cls.Capacity;
            Students = cls.Students.Select(x => new Student(x)).ToList();
            Shifts = cls.Shifts.Select(x => new Shift(x)).ToList();
            Course = cls.Course;
            Teacher = cls.Teacher;
        }
    }
}
