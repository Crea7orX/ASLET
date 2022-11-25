using Accord.Genetic;

namespace ASLET.Server.Chromosomes;

public struct TimetableSlotChromosome
{
    public int Lesson { get; set; }
    public string ClassId { get; set; }
    public string TeacherId { get; set; }
    public string HourId { get; set; }
    public List<string> Classes { get; set; }
    public int Day { get; set; }
}