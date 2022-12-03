using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASLET.Models;
using ASLET.Models.Database;
using MongoDB.Driver;

namespace ASLET.Services;

public class DatabaseService
{
    public static DatabaseService Instance { get; } = new DatabaseService();

    private const string ConnectionString = "mongodb://127.0.0.1:27017";
    private const string DatabaseName = "asletdb";
    private const string ClassesCollection = "classes";
    private const string TeachersCollection = "teachers";
    private const string SubjectsCollection = "subjects";
    private const string HoursCollection = "hours";
    private const string RoomsCollection = "rooms";
    private const string TimetablesCollection = "timetables";

    private IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        MongoClient client = new MongoClient(ConnectionString);
        IMongoDatabase? database = client.GetDatabase(DatabaseName);
        return database.GetCollection<T>(collection);
    }

    public async Task<List<ClassModelDb>> GetAllClasses()
    {
        IMongoCollection<ClassModelDb> classesCollection = ConnectToMongo<ClassModelDb>(ClassesCollection);
        IAsyncCursor<ClassModelDb> results = await classesCollection.FindAsync(_ => true);
        List<ClassModelDb> returnValue = results.ToList();
        returnValue.Sort((g1, g2) => String.Compare(g1.Name, g2.Name, StringComparison.Ordinal));
        return returnValue;
    }
    
    public async Task<List<TeacherModelDb>> GetAllTeachers()
    {
        IMongoCollection<TeacherModelDb> teachersCollection = ConnectToMongo<TeacherModelDb>(TeachersCollection);
        IAsyncCursor<TeacherModelDb> results = await teachersCollection.FindAsync(_ => true);
        List<TeacherModelDb> returnValue = results.ToList();
        returnValue.Sort((t1, t2) => String.Compare(t1.Name, t2.Name, StringComparison.Ordinal));
        return returnValue;
    }
    
    public async Task<List<SubjectModelDb>> GetAllSubjects()
    {
        IMongoCollection<SubjectModelDb> subjectsCollection = ConnectToMongo<SubjectModelDb>(SubjectsCollection);
        IAsyncCursor<SubjectModelDb> results = await subjectsCollection.FindAsync(_ => true);
        List<SubjectModelDb> returnValue = results.ToList();
        returnValue.Sort((s1, s2) => String.Compare(s1.Name, s2.Name, StringComparison.Ordinal));
        return returnValue;
    }
    
    public async Task<List<HourModelDb>> GetAllHours()
    {
        IMongoCollection<HourModelDb> hoursCollection = ConnectToMongo<HourModelDb>(HoursCollection);
        IAsyncCursor<HourModelDb> results = await hoursCollection.FindAsync(_ => true);
        List<HourModelDb> returnValue = results.ToList();
        returnValue.Sort((h1, h2) => String.Compare(h1.Class.Name, h2.Class.Name, StringComparison.Ordinal));
        return returnValue;
    }
    
    public async Task<List<RoomModelDb>> GetAllRooms()
    {
        IMongoCollection<RoomModelDb> roomsCollection = ConnectToMongo<RoomModelDb>(RoomsCollection);
        IAsyncCursor<RoomModelDb> results = await roomsCollection.FindAsync(_ => true);
        List<RoomModelDb> returnValue = results.ToList();
        returnValue.Sort((r1, r2) => String.Compare(r1.Name, r2.Name, StringComparison.Ordinal));
        return returnValue;
    }
    
    public async Task<List<TimetableHourModelDb>> GetAllTimetable()
    {
        IMongoCollection<TimetableHourModelDb> timetablesCollection = ConnectToMongo<TimetableHourModelDb>(TimetablesCollection);
        IAsyncCursor<TimetableHourModelDb> results = await timetablesCollection.FindAsync(_ => true);
        return results.ToList();
    }

    public (Task, string) CreateClass(ClassModelDb @class)
    {
        IMongoCollection<ClassModelDb> classesCollection = ConnectToMongo<ClassModelDb>(ClassesCollection);
        return (classesCollection.InsertOneAsync(@class), @class.Id);
    }
    
    public (Task, string) CreateTeacher(TeacherModelDb teacher)
    {
        IMongoCollection<TeacherModelDb> teachersCollection = ConnectToMongo<TeacherModelDb>(TeachersCollection);
        return (teachersCollection.InsertOneAsync(teacher), teacher.Id);
    }
    
    public (Task, string) CreateSubject(SubjectModelDb subject)
    {
        IMongoCollection<SubjectModelDb> subjectsCollection = ConnectToMongo<SubjectModelDb>(SubjectsCollection);
        return (subjectsCollection.InsertOneAsync(subject), subject.Id);
    }
    
    public (Task, string) CreateHour(HourModelDb hour)
    {
        IMongoCollection<HourModelDb> hoursCollection = ConnectToMongo<HourModelDb>(HoursCollection);
        return (hoursCollection.InsertOneAsync(hour), hour.Id);
    }
    
    public (Task, string) CreateRoom(RoomModelDb room)
    {
        IMongoCollection<RoomModelDb> roomsCollection = ConnectToMongo<RoomModelDb>(RoomsCollection);
        return (roomsCollection.InsertOneAsync(room), room.Id);
    }
    
    public (Task, string) CreateTimetable(TimetableHourModelDb timetable)
    {
        IMongoCollection<TimetableHourModelDb> timetablesCollection = ConnectToMongo<TimetableHourModelDb>(TimetablesCollection);
        return (timetablesCollection.InsertOneAsync(timetable), timetable.Id);
    }
    
    public Task DeleteClass(ClassModelDb @class)
    {
        IMongoCollection<ClassModelDb> classesCollection = ConnectToMongo<ClassModelDb>(ClassesCollection);
        return classesCollection.DeleteOneAsync(c => c.Id == @class.Id);
    }
    
    public Task DeleteTeacher(TeacherModelDb teacher)
    {
        IMongoCollection<TeacherModelDb> teachersCollection = ConnectToMongo<TeacherModelDb>(TeachersCollection);
        return teachersCollection.DeleteOneAsync(t => t.Id == teacher.Id);
    }
    
    public Task DeleteSubject(SubjectModelDb subject)
    {
        IMongoCollection<SubjectModelDb> subjectsCollection = ConnectToMongo<SubjectModelDb>(SubjectsCollection);
        return subjectsCollection.DeleteOneAsync(s => s.Id == subject.Id);
    }
    
    public Task DeleteHour(HourModelDb hour)
    {
        IMongoCollection<HourModelDb> hoursCollection = ConnectToMongo<HourModelDb>(HoursCollection);
        return hoursCollection.DeleteOneAsync(h => h.Id == hour.Id);
    }
    
    public Task DeleteRoom(RoomModelDb room)
    {
        IMongoCollection<RoomModelDb> roomsCollection = ConnectToMongo<RoomModelDb>(RoomsCollection);
        return roomsCollection.DeleteOneAsync(r => r.Id == room.Id);
    }
    
    public Task DeleteTimetable(TimetableHourModelDb timetable)
    {
        IMongoCollection<TimetableHourModelDb> timetablesCollection = ConnectToMongo<TimetableHourModelDb>(TimetablesCollection);
        return timetablesCollection.DeleteOneAsync(t => t.Id == timetable.Id);
    }
}