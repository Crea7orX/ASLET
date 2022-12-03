using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ASLET.Models.Database;
using ASLET.Services;
using ASLET.ViewModels;
using ReactiveUI;

namespace ASLET.Models;

public class ConfigurationService
{
    public static ConfigurationService Instance { get; } = new ConfigurationService();
    // Parsed professors
    private Dictionary<int, TeacherModel> _professors;

    // Parsed student groups
    private Dictionary<int, StudentsGroupModel> _studentGroups;

    // Parsed courses
    private Dictionary<int, SubjectModel> _courses;

    // Parsed rooms
    private Dictionary<int, RoomModel> _rooms;

    // Generate a random number  
    private static Random _random = new(DateTime.Now.Millisecond);

    // Initialize data
    public ConfigurationService()
    {
        Empty = true;
        _professors = new();
        _studentGroups = new();
        _courses = new();
        _rooms = new();
        CourseClasses = new();
    }

    // Returns teacher with specified ID
    // If there is no teacher with such ID method returns NULL
    TeacherModel GetProfessorById(int id)
    {
        if (!_professors.ContainsKey(id))
            return null;
        return _professors[id];
    }

    // Returns number of parsed professors
    public int NumberOfProfessors => _professors.Count;

    // Returns student group with specified ID
    // If there is no student group with such ID method returns NULL
    StudentsGroupModel GetStudentsGroupById(int id)
    {
        if (!_studentGroups.ContainsKey(id))
            return null;
        return _studentGroups[id];
    }

    // Returns number of parsed student groups
    public int NumberOfStudentGroups => _studentGroups.Count;

    // Returns course with specified ID
    // If there is no course with such ID method returns NULL
    SubjectModel GetCourseById(int id)
    {
        if (!_courses.ContainsKey(id))
            return null;
        return _courses[id];
    }

    public int NumberOfCourses => _courses.Count;

    // Returns room with specified ID
    // If there is no room with such ID method returns NULL
    public RoomModel GetRoomById(int id)
    {
        if (!_rooms.ContainsKey(id))
            return null;
        return _rooms[id];
    }

    // Returns number of parsed rooms
    public int NumberOfRooms => _rooms.Count;

    // Returns reference to list of parsed classes
    public List<SubjectClassModel> CourseClasses { get; set; }

    // Returns number of parsed classes
    public int NumberOfCourseClasses => CourseClasses.Count;

    // Returns TRUE if configuration is not parsed yet
    public bool Empty { get; private set; }

    public async void AddTeacher(TeacherModel teacher, bool addToDatabase = true)
    {
        _professors.Add(teacher.Id, teacher);
        _professors = new Dictionary<int, TeacherModel>(_professors.OrderBy(t => t.Value.Name));
        ObservableCollection<TeacherModel> teachers = new ObservableCollection<TeacherModel>();
        _professors.Values.ToList().ForEach(t => teachers.Add(t));
        TeachersViewModel.GetInstance(null).UpdateTeachers(ref teachers);
        if (addToDatabase)
        {
            (Task, string) returnedValue =
                DatabaseService.Instance.CreateTeacher(new TeacherModelDb(teacher.UniqueId, teacher.Name));
            await returnedValue.Item1;
            teacher.UniqueId = returnedValue.Item2;
        }
    }

    public async void AddSubject(SubjectModel subject, bool addToDatabase = true)
    {
        _courses.Add(subject.Id, subject);
        _courses = new Dictionary<int, SubjectModel>(_courses.OrderBy(s => s.Value.Name));
        ObservableCollection<SubjectModel> subjects = new ObservableCollection<SubjectModel>();
        _courses.Values.ToList().ForEach(s => subjects.Add(s));
        SubjectsViewModel.GetInstance(null).UpdateSubjects(ref subjects);
        if (addToDatabase)
        {
            (Task, string) returnedValue =
                DatabaseService.Instance.CreateSubject(new SubjectModelDb(subject.UniqueId, subject.Name));
            await returnedValue.Item1;
            subject.UniqueId = returnedValue.Item2;
        }
    }

    public async void AddRoom(RoomModel roomModel, bool addToDatabase = true)
    {
        _rooms.Add(roomModel.Id, roomModel);
        _rooms = new Dictionary<int, RoomModel>(_rooms.OrderBy(r => r.Value.Name));
        ObservableCollection<RoomModel> rooms = new ObservableCollection<RoomModel>();
        _rooms.Values.ToList().ForEach(r => rooms.Add(r));
        RoomsViewModel.GetInstance(null).UpdateRooms(ref rooms);
        if (addToDatabase)
        {
            (Task, string) returnedValue = DatabaseService.Instance.CreateRoom(new RoomModelDb(roomModel.UniqueId,
                roomModel.Name, roomModel.Lab, roomModel.NumberOfSeats));
            await returnedValue.Item1;
            roomModel.UniqueId = returnedValue.Item2;
        }
    }

    public async void AddGroup(StudentsGroupModel groupModel, bool addToDatabase = true)
    {
        _studentGroups.Add(groupModel.Id, groupModel);
        _studentGroups = new Dictionary<int, StudentsGroupModel>(_studentGroups.OrderBy(g => g.Value.Name));
        ObservableCollection<StudentsGroupModel> groups = new ObservableCollection<StudentsGroupModel>();
        _studentGroups.Values.ToList().ForEach(g => groups.Add(g));
        ClassesViewModel.GetInstance(null).UpdateClasses(ref groups);
        if (addToDatabase)
        {
            (Task, string) returnedValue = DatabaseService.Instance.CreateClass(new ClassModelDb(groupModel.UniqueId,
                groupModel.Name, groupModel.Grade, groupModel.Letter,
                groupModel.NumberOfStudents));
            await returnedValue.Item1;
            groupModel.UniqueId = returnedValue.Item2;
        }
    }

    public async void AddHour(SubjectClassModel hour, bool addToDatabase = true)
    {
        CourseClasses.Add(hour);
        CourseClasses = new List<SubjectClassModel>(CourseClasses.OrderBy(h => h.Groups[0].Name));
        Empty = false;
        ObservableCollection<SubjectClassModel> hours = new ObservableCollection<SubjectClassModel>();
        CourseClasses.ToList().ForEach(h => hours.Add(h));
        HoursViewModel.GetInstance(null).UpdateHours(ref hours);
        if (addToDatabase)
        {
            (Task, string) returnedValue = DatabaseService.Instance.CreateHour(new HourModelDb(hour.UniqueId,
                hour.NumberOfSeats, hour.LabRequired, hour.Duration,
                new TeacherModelDb(hour.TeacherModel.UniqueId, hour.TeacherModel.Name),
                new SubjectModelDb(hour.SubjectModel.UniqueId, hour.SubjectModel.Name),
                new ClassModelDb(hour.Groups[0].UniqueId, hour.Groups[0].Name, hour.Groups[0].Grade,
                    hour.Groups[0].Letter, hour.Groups[0].NumberOfStudents)));
            await returnedValue.Item1;
            hour.UniqueId = returnedValue.Item2;
        }
    }

    public async void RemoveTeacher(TeacherModel teacher)
    {
        _professors.Remove(teacher.Id);
        ObservableCollection<TeacherModel> teachers = new ObservableCollection<TeacherModel>();
        _professors.Values.ToList().ForEach(t => teachers.Add(t));
        TeachersViewModel.GetInstance(null).UpdateTeachers(ref teachers);
        CheckToRemove(teacher);
        await DatabaseService.Instance.DeleteTeacher(new TeacherModelDb(teacher.UniqueId, teacher.Name));
    }

    public async void RemoveSubject(SubjectModel subject)
    {
        _courses.Remove(subject.Id);
        ObservableCollection<SubjectModel> subjects = new ObservableCollection<SubjectModel>();
        _courses.Values.ToList().ForEach(s => subjects.Add(s));
        SubjectsViewModel.GetInstance(null).UpdateSubjects(ref subjects);
        CheckToRemove(subject);
        await DatabaseService.Instance.DeleteSubject(new SubjectModelDb(subject.UniqueId, subject.Name));
    }
    
    public async void RemoveRoom(RoomModel roomModel)
    {
        _rooms.Remove(roomModel.Id);
        ObservableCollection<RoomModel> rooms = new ObservableCollection<RoomModel>();
        _rooms.Values.ToList().ForEach(r => rooms.Add(r));
        RoomsViewModel.GetInstance(null).UpdateRooms(ref rooms);
        await DatabaseService.Instance.DeleteRoom(new RoomModelDb(roomModel.UniqueId, roomModel.Name, roomModel.Lab, roomModel.NumberOfSeats));
    }

    public async void RemoveGroup(StudentsGroupModel groupModel)
    {
        _studentGroups.Remove(groupModel.Id);
        ObservableCollection<StudentsGroupModel> groups = new ObservableCollection<StudentsGroupModel>();
        _studentGroups.Values.ToList().ForEach(g => groups.Add(g));
        ClassesViewModel.GetInstance(null).UpdateClasses(ref groups);
        CheckToRemove(groupModel);
        await DatabaseService.Instance.DeleteClass(new ClassModelDb(groupModel.UniqueId, groupModel.Name, groupModel.Grade, groupModel.Letter,
            groupModel.NumberOfStudents));
    }

    public async void RemoveHour(SubjectClassModel hour)
    {
        CourseClasses.Remove(hour);
        ObservableCollection<SubjectClassModel> hours = new ObservableCollection<SubjectClassModel>();
        CourseClasses.ToList().ForEach(h => hours.Add(h));
        HoursViewModel.GetInstance(null).UpdateHours(ref hours);
        await DatabaseService.Instance.DeleteHour(new HourModelDb(hour.UniqueId, hour.NumberOfSeats, hour.LabRequired, hour.Duration, new TeacherModelDb(hour.TeacherModel.UniqueId, hour.TeacherModel.Name), new SubjectModelDb(hour.SubjectModel.UniqueId, hour.SubjectModel.Name), new ClassModelDb(hour.Groups[0].UniqueId, hour.Groups[0].Name, hour.Groups[0].Grade, hour.Groups[0].Letter, hour.Groups[0].NumberOfStudents)));
    }

    public ObservableCollection<TeacherModel> GetTeachers()
    {
        ObservableCollection<TeacherModel> returnValue = new ObservableCollection<TeacherModel>();
        _professors.Values.ToList().ForEach(p => returnValue.Add(p));
        return returnValue;
    }

    public ObservableCollection<SubjectModel> GetSubjects()
    {
        ObservableCollection<SubjectModel> returnValue = new ObservableCollection<SubjectModel>();
        _courses.Values.ToList().ForEach(c => returnValue.Add(c));
        return returnValue;
    }

    public ObservableCollection<RoomModel> GetRooms()
    {
        ObservableCollection<RoomModel> returnValue = new ObservableCollection<RoomModel>();
        _rooms.Values.ToList().ForEach(r => returnValue.Add(r));
        return returnValue;
    }

    public ObservableCollection<StudentsGroupModel> GetGroups()
    {
        ObservableCollection<StudentsGroupModel> returnValue = new ObservableCollection<StudentsGroupModel>();
        _studentGroups.Values.ToList().ForEach(g => returnValue.Add(g));
        return returnValue;
    }

    public ObservableCollection<SubjectClassModel> GetHours()
    {
        ObservableCollection<SubjectClassModel> returnValue = new ObservableCollection<SubjectClassModel>();
        CourseClasses.ToList().ForEach(h => returnValue.Add(h));
        return returnValue;
    }

    private void CheckToRemove<TModel>(TModel model)
    {
        if (model is TeacherModel teacher)
        {
            foreach(SubjectClassModel hour in CourseClasses.Where(hour => hour.TeacherModel.Equals(teacher)).ToList())
            {
                CourseClasses.Remove(hour);
                DatabaseService.Instance.DeleteHour(new HourModelDb(hour.UniqueId, hour.NumberOfSeats, hour.LabRequired, hour.Duration, new TeacherModelDb(hour.TeacherModel.UniqueId, hour.TeacherModel.Name), new SubjectModelDb(hour.SubjectModel.UniqueId, hour.SubjectModel.Name), new ClassModelDb(hour.Groups[0].UniqueId, hour.Groups[0].Name, hour.Groups[0].Grade, hour.Groups[0].Letter, hour.Groups[0].NumberOfStudents)));
            }
        } else if (model is SubjectModel subject)
        {
            foreach(SubjectClassModel hour in CourseClasses.Where(hour => hour.SubjectModel.Equals(subject)).ToList())
            {
                CourseClasses.Remove(hour);
                DatabaseService.Instance.DeleteHour(new HourModelDb(hour.UniqueId, hour.NumberOfSeats, hour.LabRequired, hour.Duration, new TeacherModelDb(hour.TeacherModel.UniqueId, hour.TeacherModel.Name), new SubjectModelDb(hour.SubjectModel.UniqueId, hour.SubjectModel.Name), new ClassModelDb(hour.Groups[0].UniqueId, hour.Groups[0].Name, hour.Groups[0].Grade, hour.Groups[0].Letter, hour.Groups[0].NumberOfStudents)));
            }
        } else if (model is StudentsGroupModel studentsGroup)
        {
            foreach(SubjectClassModel hour in CourseClasses.Where(hour => hour.Groups[0].Equals(studentsGroup)).ToList())
            {
                CourseClasses.Remove(hour);
                DatabaseService.Instance.DeleteHour(new HourModelDb(hour.UniqueId, hour.NumberOfSeats, hour.LabRequired, hour.Duration, new TeacherModelDb(hour.TeacherModel.UniqueId, hour.TeacherModel.Name), new SubjectModelDb(hour.SubjectModel.UniqueId, hour.SubjectModel.Name), new ClassModelDb(hour.Groups[0].UniqueId, hour.Groups[0].Name, hour.Groups[0].Grade, hour.Groups[0].Letter, hour.Groups[0].NumberOfStudents)));
            }
        }
        ObservableCollection<SubjectClassModel> hours = new ObservableCollection<SubjectClassModel>();
        CourseClasses.ToList().ForEach(h => hours.Add(h));
        HoursViewModel.GetInstance(null).UpdateHours(ref hours);
    }

    public async void LoadData()
    {
        List<ClassModelDb> classes = await DatabaseService.Instance.GetAllClasses();
        List<TeacherModelDb> teachers = await DatabaseService.Instance.GetAllTeachers();
        List<SubjectModelDb> subjects = await DatabaseService.Instance.GetAllSubjects();
        List<HourModelDb> hours = await DatabaseService.Instance.GetAllHours();
        List<RoomModelDb> rooms = await DatabaseService.Instance.GetAllRooms();
        List<TimetableHourModelDb> timetable = await DatabaseService.Instance.GetAllTimetable();

        Dictionary<string, StudentsGroupModel> addedClasses = new Dictionary<string, StudentsGroupModel>();
        Dictionary<string, TeacherModel> addedTeachers = new Dictionary<string, TeacherModel>();
        Dictionary<string, SubjectModel> addedSubjects = new Dictionary<string, SubjectModel>();

        foreach (ClassModelDb @class in classes)
        {
            StudentsGroupModel newClass =
                new StudentsGroupModel(@class.Grade, @class.Letter, @class.NumberOfStudents, true, @class.Id);
            addedClasses.Add(newClass.UniqueId, newClass);
            AddGroup(newClass, false);
        }

        foreach (TeacherModelDb teacher in teachers)
        {
            TeacherModel newTeacher = new TeacherModel(teacher.Name, true, teacher.Id);
            addedTeachers.Add(newTeacher.UniqueId, newTeacher);
            AddTeacher(newTeacher, false);
        }

        foreach (SubjectModelDb subject in subjects)
        {
            SubjectModel newSubject = new SubjectModel(subject.Name, true, subject.Id);
            addedSubjects.Add(newSubject.UniqueId, newSubject);
            AddSubject(newSubject, false);
        }

        foreach (HourModelDb hour in hours)
        {
            StudentsGroupModel[] studentsGroupModels = {
                addedClasses[hour.Class.Id]
            };
            AddHour(new SubjectClassModel(addedTeachers[hour.Teacher.Id], addedSubjects[hour.Subject.Id], hour.LabRequired, hour.HoursAWeek, true, hour.Id, studentsGroupModels), false);
        }

        foreach (RoomModelDb room in rooms)
        {
            AddRoom(new RoomModel(room.Name, room.Lab, room.NumberOfSeats, true, room.Id), false);
        }
    }

    public static int Rand()
    {
        return _random.Next(0, 32767);
    }

    public static double Random()
    {
        return _random.NextDouble();
    }

    public static int Rand(int size)
    {
        return _random.Next(size);
    }

    public static int Rand(int min, int max)
    {
        return min + Rand(max - min + 1);
    }

    public static double Rand(double min, double max)
    {
        return min + _random.NextDouble() * (max - min);
    }

    public static void Seed()
    {
        _random = new Random(DateTime.Now.Millisecond);
    }
}