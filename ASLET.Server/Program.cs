using Accord.Genetic;
using ASLET.Server.Chromosomes;
using ASLET.Server.Context;
using ASLET.Server.Models;
using ASLET.Server.Services.Timetables;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddScoped<ITimetableService, TimetableService>();

    using (DatabaseContext databaseContext = new DatabaseContext())
    {
        Population population = new Population(1000, new TimetableChromosome(databaseContext),
            new TimetableChromosome.FitnessFunction(), new EliteSelection());

        int i = 0;
        while (true)
        {
            population.RunEpoch();
            i++;
            if (population.FitnessMax >= 0.99 || i >= 1000)
            {
                break;
            }
        }

        List<TimetableSlot> timetable = (population.BestChromosome as TimetableChromosome).Value.Select(chromosome =>
            new TimetableSlot()
            {
                Lesson = chromosome.Lesson,
                ClassId = "TOVA E TEST",
                TeacherId = chromosome.TeacherId,
                HourId = chromosome.HourId,
                DayOfWeek = (DayOfWeek)chromosome.Day,
                Id = Guid.NewGuid().ToString()
            }
        ).ToList();
        databaseContext.TimetableSlots.AddRange(timetable);
        databaseContext.SaveChanges();
    }
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}