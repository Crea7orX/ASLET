﻿// <auto-generated />
using ASLET.Server.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASLET.Server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221126135439_AsletUser")]
    partial class AsletUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("ASLET.Models.AsletUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("AsletUsers");
                });

            modelBuilder.Entity("ASLET.Server.Models.ClassHour", b =>
                {
                    b.Property<string>("ClassId")
                        .HasColumnType("TEXT");

                    b.Property<string>("HourId")
                        .HasColumnType("TEXT");

                    b.HasKey("ClassId", "HourId");

                    b.HasIndex("HourId");

                    b.ToTable("ClassHours");
                });

            modelBuilder.Entity("ASLET.Server.Models.Hour", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeacherId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Hours");
                });

            modelBuilder.Entity("ASLET.Server.Models.SchoolClass", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<char>("Letter")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("ASLET.Server.Models.Teacher", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ASLET.Server.Models.TimetableSlot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HourId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Lesson")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeacherId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HourId");

                    b.ToTable("TimetableSlots");
                });

            modelBuilder.Entity("SchoolClassTimetableSlot", b =>
                {
                    b.Property<string>("ClassesId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TimetableSlotsId")
                        .HasColumnType("TEXT");

                    b.HasKey("ClassesId", "TimetableSlotsId");

                    b.HasIndex("TimetableSlotsId");

                    b.ToTable("SchoolClassTimetableSlot");
                });

            modelBuilder.Entity("ASLET.Server.Models.ClassHour", b =>
                {
                    b.HasOne("ASLET.Server.Models.SchoolClass", "Class")
                        .WithMany("ClassHours")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASLET.Server.Models.Hour", "Hour")
                        .WithMany("Classes")
                        .HasForeignKey("HourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Hour");
                });

            modelBuilder.Entity("ASLET.Server.Models.Hour", b =>
                {
                    b.HasOne("ASLET.Server.Models.Teacher", "Teacher")
                        .WithMany("Hours")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ASLET.Server.Models.TimetableSlot", b =>
                {
                    b.HasOne("ASLET.Server.Models.Hour", "Hour")
                        .WithMany("Slots")
                        .HasForeignKey("HourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hour");
                });

            modelBuilder.Entity("SchoolClassTimetableSlot", b =>
                {
                    b.HasOne("ASLET.Server.Models.SchoolClass", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASLET.Server.Models.TimetableSlot", null)
                        .WithMany()
                        .HasForeignKey("TimetableSlotsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ASLET.Server.Models.Hour", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Slots");
                });

            modelBuilder.Entity("ASLET.Server.Models.SchoolClass", b =>
                {
                    b.Navigation("ClassHours");
                });

            modelBuilder.Entity("ASLET.Server.Models.Teacher", b =>
                {
                    b.Navigation("Hours");
                });
#pragma warning restore 612, 618
        }
    }
}
