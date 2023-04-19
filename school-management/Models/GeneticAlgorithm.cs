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
            int k = 0;
            while (k < Init.POPULATION_SIZE)
            {
                for (int i = 0; i < Init.POPULATION_SIZE; i++)
                {
                    for (int j = i + 1; j < Init.POPULATION_SIZE; ++j)
                    {
                        Random r = new Random();
                        if (r.NextDouble() < Init.CROSSOVER_RATE)
                        {
                            Population.Timetables[k] = CrossoverTimetable(Population.Timetables[i], Population.Timetables[j]);
                        }
                        else
                        {
                            Population.Timetables[k] = CrossoverTimetable(Population.Timetables[j], Population.Timetables[i]);
                        }
                        k++;
                        if (k >= Init.POPULATION_SIZE)
                        {
                            break;
                        }
                    }
                    if (k >= Init.POPULATION_SIZE)
                    {
                        break;
                    }
                }
            }
            Population.SortByFitness();
        }

        public Timetable CrossoverTimetable(Timetable timetable1, Timetable timetable2)
        {
            // giữ một nửa schedule của bố, nửa sau schedule của mẹ
            int timetableSize = timetable1.Schedules.Count;
            for (int i = 0; i < timetableSize / 2; i++) 
            {
                if (i >= timetable2.Schedules.Count)
                {
                    break;
                }
                timetable1.Schedules[timetableSize - i - 1] = timetable2.Schedules[i];
                
            }
            return timetable1;
        }

        public void MutatePopulation()
        {
            Random r = new Random();
            int mutateNumber = r.Next(Init.POPULATION_SIZE);
            Population.Timetables[mutateNumber] = MutateTimetable(Population.Timetables[mutateNumber]);
            Population.SortByFitness();
        }

        public Timetable MutateTimetable(Timetable timetable)
        {
            Random r = new Random();
            if (r.NextDouble() > Init.MUTATE_RATE) 
            {
                int mutateNumber = r.Next(timetable.Schedules.Count());
                while (timetable.Schedules[mutateNumber].Classes.Count == 0)
                {
                    mutateNumber = r.Next(timetable.Schedules.Count());
                }
                int numberOfClass = timetable.Schedules[mutateNumber].Classes.Count;
                int randomIndexOfClass = r.Next(numberOfClass);
                var cls = timetable.Schedules[mutateNumber].Classes[randomIndexOfClass];
                timetable.Schedules[mutateNumber].Classes.Remove(cls);
                timetable.Schedules[timetable.Schedules.Count - mutateNumber - 1].Classes.Add(cls);
            }
            return timetable;
        }

        public object Evolve()
        {
            int genTime = 1;
            while (Population.Timetables[0].Fitness() != 1 && genTime < 1000)
            {
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
