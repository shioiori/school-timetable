namespace school_management.Models
{
    public class Population
    {
        public List<Timetable> Timetables { get; set; }

        public Population(int size)
        {
            Timetables = new List<Timetable>();
            for (int i = 0; i < size; ++i)
            {
                Timetables.Add(Init.InitTimetable());
            }
        }
        public List<Timetable> SortByFitness()
        {
            Timetables.Sort((x, y) =>
            {
                return y.Fitness().CompareTo(x.Fitness());
            });
            return Timetables;
        }
    }
}
