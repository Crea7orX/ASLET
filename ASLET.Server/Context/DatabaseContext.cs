using ASLET.Models;
using ASLET.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ASLET.Server.Context;

public class DatabaseContext : DbContext
{
    public DbSet<ClassHour> ClassHours { get; set; }
    public DbSet<SchoolClass> Classes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Hour> Hours { get; set; }
    public DbSet<TimetableSlot> TimetableSlots { get; set; }
    public DbSet<AsletUser> AsletUsers { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Server=(localdb)\\MSSQLLocalDB;Database=ASLET;Integrated Security=True"); // TODO
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassHour>().HasKey("ClassId", "HourId");
        modelBuilder.Entity<SchoolClass>().HasMany(schoolClass => schoolClass.ClassHours);
        modelBuilder.Entity<Hour>().HasMany(hour => hour.Classes);
        modelBuilder.Entity<SchoolClass>().HasMany<TimetableSlot>(schoolClass => schoolClass.TimetableSlots);
        modelBuilder.Entity<TimetableSlot>().HasMany<SchoolClass>(slot => slot.Classes);
        
        base.OnModelCreating(modelBuilder);
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
}