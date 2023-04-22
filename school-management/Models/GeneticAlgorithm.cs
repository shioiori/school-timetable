using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace school_management.Models
{
    public class GeneticAlgorithm
    {

        public Population Population { get; }

        public GeneticAlgorithm()
        {
            Init.Default();
            Population = new Population(Init.POPULATION_SIZE);
        }

        public void CrossoverPopulation()
        {
            for (int i = 1; i < Init.POPULATION_SIZE; ++i)
            {
                if (Init.CROSSOVER_RATE > Init.RandomDouble())
                {
                    Population.SortByFitness();
                    var first_timetable = Population.Timetables[0];
                    var second_timetable = Population.Timetables[1];
                    Population.Timetables[i] = CrossoverTimetable(ref first_timetable, ref second_timetable);
                }
            }
        }

        public Timetable CrossoverTimetable(ref Timetable timetable1, ref Timetable timetable2)
        {
            Timetable timetable = new Timetable();
            for (int i = 0, numberOfClasses = Init.Classes.Count; i < numberOfClasses; ++i)
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
            Parallel.For(1, Init.POPULATION_SIZE, i =>
            {
                Population.Timetables[i] = MutateTimetable(Population.Timetables[i]);
            });
        }

        public Timetable MutateTimetable(Timetable timetable)
        {
            Timetable ttb = Init.InitTimetable();
            for (int i = 0, numberOfClasses = Init.Classes.Count; i < numberOfClasses; ++i)
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (Population.Timetables[0].Fitness() != 1)
            {
                CrossoverPopulation();
                MutatePopulation();
                genTime++;
            }

            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            return new
            {
                ElapseTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timespan.Hours, timespan.Minutes, timespan.Seconds),
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
