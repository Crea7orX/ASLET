using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using ASLET.ViewModels;

namespace ASLET.Models;

public class ConfigurationService
{
    public static ConfigurationService Instance { get; } = new ConfigurationService();
    // Parsed professors
    private readonly Dictionary<int, TeacherModel> _professors;

    // Parsed student groups
    private readonly Dictionary<int, StudentsGroupModel> _studentGroups;

    // Parsed courses
    private readonly Dictionary<int, SubjectModel> _courses;

    // Parsed rooms
    private readonly Dictionary<int, RoomModel> _rooms;

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
    public List<SubjectClassModel> CourseClasses { get; }

    // Returns number of parsed classes
    public int NumberOfCourseClasses => CourseClasses.Count;

    // Returns TRUE if configuration is not parsed yet
    public bool Empty { get; private set; }

    public void AddTeacher(TeacherModel teacher)
    {
        _professors.Add(teacher.Id, teacher);
        ObservableCollection<TeacherModel> teachers = new ObservableCollection<TeacherModel>();
        _professors.Values.ToList().ForEach(t => teachers.Add(t));
        TeachersViewModel.GetInstance(null).UpdateTeachers(ref teachers);
    }

    public void AddSubject(SubjectModel subject)
    {
        _courses.Add(subject.Id, subject);
        ObservableCollection<SubjectModel> subjects = new ObservableCollection<SubjectModel>();
        _courses.Values.ToList().ForEach(s => subjects.Add(s));
        SubjectsViewModel.GetInstance(null).UpdateSubjects(ref subjects);
    }

    public void AddRoom(RoomModel roomModel)
    {
        _rooms.Add(roomModel.Id, roomModel);
        ObservableCollection<RoomModel> rooms = new ObservableCollection<RoomModel>();
        _rooms.Values.ToList().ForEach(r => rooms.Add(r));
        RoomsViewModel.GetInstance(null).UpdateRooms(ref rooms);
    }

    public void AddGroup(StudentsGroupModel groupModel)
    {
        _studentGroups.Add(groupModel.Id, groupModel);
        ObservableCollection<StudentsGroupModel> groups = new ObservableCollection<StudentsGroupModel>();
        _studentGroups.Values.ToList().ForEach(g => groups.Add(g));
        ClassesViewModel.GetInstance(null).UpdateClasses(ref groups);
    }

    public void AddHour(SubjectClassModel hour)
    {
        CourseClasses.Add(hour);
        Empty = false;
        ObservableCollection<SubjectClassModel> hours = new ObservableCollection<SubjectClassModel>();
        CourseClasses.ToList().ForEach(h => hours.Add(h));
        HoursViewModel.GetInstance(null).UpdateHours(ref hours);
    }

    public void RemoveTeacher(TeacherModel teacher)
    {
        _professors.Remove(teacher.Id);
        ObservableCollection<TeacherModel> teachers = new ObservableCollection<TeacherModel>();
        _professors.Values.ToList().ForEach(t => teachers.Add(t));
        TeachersViewModel.GetInstance(null).UpdateTeachers(ref teachers);
    }

    public void RemoveSubject(SubjectModel subject)
    {
        _courses.Remove(subject.Id);
        ObservableCollection<SubjectModel> subjects = new ObservableCollection<SubjectModel>();
        _courses.Values.ToList().ForEach(s => subjects.Add(s));
        SubjectsViewModel.GetInstance(null).UpdateSubjects(ref subjects);
    }
    
    public void RemoveRoom(RoomModel roomModel)
    {
        _rooms.Remove(roomModel.Id);
        ObservableCollection<RoomModel> rooms = new ObservableCollection<RoomModel>();
        _rooms.Values.ToList().ForEach(r => rooms.Add(r));
        RoomsViewModel.GetInstance(null).UpdateRooms(ref rooms);
    }

    public void RemoveGroup(StudentsGroupModel groupModel)
    {
        _studentGroups.Remove(groupModel.Id);
        ObservableCollection<StudentsGroupModel> groups = new ObservableCollection<StudentsGroupModel>();
        _studentGroups.Values.ToList().ForEach(g => groups.Add(g));
        ClassesViewModel.GetInstance(null).UpdateClasses(ref groups);
    }

    public void RemoveHour(SubjectClassModel hour)
    {
        CourseClasses.Remove(hour);
        ObservableCollection<SubjectClassModel> hours = new ObservableCollection<SubjectClassModel>();
        CourseClasses.ToList().ForEach(h => hours.Add(h));
        HoursViewModel.GetInstance(null).UpdateHours(ref hours);
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