using System.Xml.Linq;

namespace school_management.Models
{
    public class GeneticAlgorithm
    {
        public Population Population { get; set; }
        public GeneticAlgorithm() 
        {
            Init.Default();
            Population = new Population(Init.POPULATION_SIZE);
        }
        public void CrossoverPopulation()
        {
            int populationSize = Population.Timetables.Count;
            for (int i = 1; i < Init.POPULATION_SIZE; i++)
            {
                if (Init.CROSSOVER_RATE > Init.RandomDouble())
                {
                    Population.SortByFitness();
                    Population.Timetables[i] = CrossoverTimetable(Population.Timetables[0], Population.Timetables[1]);
                }
            }
        }

        public Timetable CrossoverTimetable(Timetable timetable1, Timetable timetable2)
        {
            timetable1.SortClassByName();
            timetable2.SortClassByName();

            Timetable timetable = new Timetable();
            for (int i = 0; i < Init.Classes.Count; ++i)
            {
                if (Init.RandomDouble() > 0.5)
                {
                    timetable.Classes.Add(timetable1.Classes[i]);
                }
                else
                {
                    timetable.Classes.Add(timetable2.Classes[i]);
                }
            }
            
            return timetable;
        }

        public void MutatePopulation()
        {
            for (int i = 1; i < Init.POPULATION_SIZE; i++) 
            {
                Population.Timetables[i] = MutateTimetable(Population.Timetables[i]);
            }
        }

        public Timetable MutateTimetable(Timetable timetable)
        {
            Timetable ttb = Init.InitTimetable();
            ttb.SortClassByName();
            timetable.SortClassByName();
            for (int i = 0; i < Init.Classes.Count; ++i)
            {
                if (Init.RandomDouble() < Init.MUTATE_RATE)
                {
                    timetable.Classes[i] = ttb.Classes[i];
                }
            }
            return timetable;
        }

        public object Evolve()
        {
            int genTime = 1;
            while (Population.Timetables[0].Fitness() != 1)
            {
                Population.SortByFitness();
                CrossoverPopulation();
                MutatePopulation();
                genTime++;
            }
            Population.SortByFitness();
            return new
            {
                GenerationTime = genTime,
                NumberOfConflict = Population.Timetables[0].NumberOfConflict,
                Timetable = Population.Timetables[0],
            };
        }

        public object InitGen()
        {
            Population.SortByFitness();
            return new
            {
                GenerationTime = 0,
                NumberOfConflict = 0,
                Timetable = Population.Timetables[0],
            };
        }
    }
}
