namespace school_management.Models
{
    public class Schedule
    {
        public Shift Shift { get; set; }
        public List<Class> Classes { get; set; }

        public Schedule()
        {
            Classes = new List<Class>();
        }
    }
}
