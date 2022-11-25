using Accord.Genetic;
using ASLET.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ASLET.Server.Chromosomes;

public class TimetableChromosome : ChromosomeBase
{
    private readonly Context.DatabaseContext _dataDatabaseContext;
    private static Random _random = new Random();

    public List<TimetableSlotChromosome> Value;

    public TimetableChromosome(Context.DatabaseContext dataDatabaseContext)
    {
        _dataDatabaseContext = dataDatabaseContext;
        Generate();
    }

    public TimetableChromosome(List<TimetableSlotChromosome> slots, Context.DatabaseContext dataDatabaseContext)
    {
        _dataDatabaseContext = dataDatabaseContext;
        Value = slots.ToList();
    }

    public override void Generate()
    {
        IEnumerable<TimetableSlotChromosome> generateRandomSlots()
        {
            List<Hour> hours = _dataDatabaseContext.Hours
                .Include(hour => hour.Teacher)
                .Include(hour => hour.Classes)
                .ToList();

            foreach (Hour hour in hours)
            {
                yield return new TimetableSlotChromosome()
                {
                    Lesson = (byte)_random.Next(1, 8),
                    Classes = hour.Classes.Select(schoolClass => schoolClass.ClassId).ToList(),
                    ClassId = hour.Id,
                    HourId = hour.Id,
                    TeacherId = hour.Teacher.Id,
                    Day = _random.Next(1, 5)
                };
            }
        }

        Value = generateRandomSlots().ToList();
    }

    public override IChromosome CreateNew()
    {
        TimetableChromosome timetableChromosome = new TimetableChromosome(_dataDatabaseContext);
        timetableChromosome.Generate();
        return timetableChromosome;
    }

    public override IChromosome Clone()
    {
        return new TimetableChromosome(Value, _dataDatabaseContext);
    }

    public override void Mutate()
    {
        int index = _random.Next(0, Value.Count - 1);
        TimetableSlotChromosome timetableSlotChromosome = Value.ElementAt(index);
        timetableSlotChromosome.Day = _random.Next(1, 5);
        timetableSlotChromosome.Lesson = (byte)_random.Next(1, 8);
        Value[index] = timetableSlotChromosome;
    }

    public override void Crossover(IChromosome pair)
    {
        int randomValue = _random.Next(0, Value.Count - 2);
        TimetableChromosome anotherChromosome = pair as TimetableChromosome;

        for (int index = randomValue; index < anotherChromosome.Value.Count; index++)
        {
            Value[index] = anotherChromosome.Value[index];
        }
    }

    public class FitnessFunction : IFitnessFunction
    {
        public double Evaluate(IChromosome chromosome)
        {
            double score = 1;
            List<TimetableSlotChromosome> values = (chromosome as TimetableChromosome).Value;
            Func<TimetableSlotChromosome, List<TimetableSlotChromosome>> GetOverlaps =
                current => values
                    .Except(new[] { current })
                    .Where(slot => slot.TeacherId == current.TeacherId)
                    .Where(slot => slot.Day == current.Day && slot.Lesson == current.Lesson)
                    .ToList();

            foreach (TimetableSlotChromosome value in values)
            {
                List<TimetableSlotChromosome> overlaps = GetOverlaps(value);
                score -= overlaps.GroupBy(slot => slot.TeacherId).Sum(x => x.Count() - 1);
                score -= overlaps.GroupBy(slot => slot.HourId).Sum(x => x.Count() - 1);
                score -= overlaps.GroupBy(slot => slot.Lesson).Sum(x => x.Count() - 1);
                score -= overlaps.Sum(item => item.Classes.Intersect(value.Classes).Count());
            }

            score -= values.GroupBy(v => v.Day).Count() * 0.5;
            return Math.Pow(Math.Abs(score), -1);
        }
    }
}