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
        return results.ToList();
    }
    
    public async Task<List<TeacherModelDb>> GetAllTeachers()
    {
        IMongoCollection<TeacherModelDb> teachersCollection = ConnectToMongo<TeacherModelDb>(TeachersCollection);
        IAsyncCursor<TeacherModelDb> results = await teachersCollection.FindAsync(_ => true);
        return results.ToList();
    }
    
    public async Task<List<SubjectModelDb>> GetAllSubjects()
    {
        IMongoCollection<SubjectModelDb> subjectsCollection = ConnectToMongo<SubjectModelDb>(SubjectsCollection);
        IAsyncCursor<SubjectModelDb> results = await subjectsCollection.FindAsync(_ => true);
        return results.ToList();
    }
    
    public async Task<List<HourModelDb>> GetAllHours()
    {
        IMongoCollection<HourModelDb> hoursCollection = ConnectToMongo<HourModelDb>(HoursCollection);
        IAsyncCursor<HourModelDb> results = await hoursCollection.FindAsync(_ => true);
        return results.ToList();
    }
    
    public async Task<List<RoomModelDb>> GetAllRooms()
    {
        IMongoCollection<RoomModelDb> roomsCollection = ConnectToMongo<RoomModelDb>(RoomsCollection);
        IAsyncCursor<RoomModelDb> results = await roomsCollection.FindAsync(_ => true);
        return results.ToList();
    }
    
    public async Task<List<TimetableHourModelDb>> GetAllTimetable()
    {
        IMongoCollection<TimetableHourModelDb> timetablesCollection = ConnectToMongo<TimetableHourModelDb>(TimetablesCollection);
        IAsyncCursor<TimetableHourModelDb> results = await timetablesCollection.FindAsync(_ => true);
        return results.ToList();
    }

    public Task CreateClass(ClassModelDb @class)
    {
        IMongoCollection<ClassModelDb> classesCollection = ConnectToMongo<ClassModelDb>(ClassesCollection);
        return classesCollection.InsertOneAsync(@class);
    }
    
    public Task CreateTeacher(TeacherModelDb teacher)
    {
        IMongoCollection<TeacherModelDb> teachersCollection = ConnectToMongo<TeacherModelDb>(TeachersCollection);
        return teachersCollection.InsertOneAsync(teacher);
    }
    
    public Task CreateSubject(SubjectModelDb subject)
    {
        IMongoCollection<SubjectModelDb> subjectsCollection = ConnectToMongo<SubjectModelDb>(SubjectsCollection);
        return subjectsCollection.InsertOneAsync(subject);
    }
    
    public Task CreateHour(HourModelDb hour)
    {
        IMongoCollection<HourModelDb> hoursCollection = ConnectToMongo<HourModelDb>(HoursCollection);
        return hoursCollection.InsertOneAsync(hour);
    }
    
    public Task CreateRoom(RoomModelDb room)
    {
        IMongoCollection<RoomModelDb> roomsCollection = ConnectToMongo<RoomModelDb>(RoomsCollection);
        return roomsCollection.InsertOneAsync(room);
    }
    
    public Task CreateTimetable(TimetableHourModelDb timetable)
    {
        IMongoCollection<TimetableHourModelDb> timetablesCollection = ConnectToMongo<TimetableHourModelDb>(TimetablesCollection);
        return timetablesCollection.InsertOneAsync(timetable);
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