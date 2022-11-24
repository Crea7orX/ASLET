using ASLET.Server.Context;
using ASLET.Server.Services.Timetables;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddScoped<ITimetableService, TimetableService>();

    builder.Services.AddDbContext<Context>(options =>
        options.UseMySql("server=db4free.net;uid=asletadmin;pwd=QKkRQ4b%KX5PkQiLQ1w7oBFvxib%r1",
            ServerVersion.Parse("8.0.25-mysql")));
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}