using System;
using System.Collections.ObjectModel;
using ASLET.Models;
using ASLET.Services.Handlers;
using ASLET.ViewModels;
using DynamicData;

namespace ASLET.Services;

public static class TimetableService
{
    private static ObservableCollection<ClassModel> _classModels = new();
    private static ObservableCollection<TeacherModel> _teacherModels = new();
    private static ObservableCollection<SubjectModel> _subjectModels = new();
    private static ObservableCollection<HourModel> _hourModels = new();

    public static void AddClass(ClassModel classToAdd)
    {
        GlobalSpace.ClassController.Add(classToAdd.ToString());
        _classModels.Add(classToAdd);
        ClassesViewModel.GetInstance(null).UpdateClasses(ref _classModels);
        TimetablesViewModel.GetInstance(null).FillClasses();
    }

    public static void RemoveClass(ClassModel classToRemove)
    {
        GlobalSpace.ClassController.Remove(classToRemove.ToString());
        _classModels.Remove(classToRemove);
        ClassesViewModel.GetInstance(null).UpdateClasses(ref _classModels);
        CheckAndUpdateHours(classToRemove);
        TimetablesViewModel.GetInstance(null).FillClasses();
    }

    public static void AddTeacher(TeacherModel teacherToAdd)
    {
        GlobalSpace.TeacherController.Add(teacherToAdd.ToString());
        _teacherModels.Add(teacherToAdd);
        TeachersViewModel.GetInstance(null).UpdateTeachers(ref _teacherModels);
    }

    public static void RemoveTeacher(TeacherModel teacherToRemove)
    {
        GlobalSpace.TeacherController.Remove(teacherToRemove.ToString());
        _teacherModels.Remove(teacherToRemove);
        TeachersViewModel.GetInstance(null).UpdateTeachers(ref _teacherModels);
        CheckAndUpdateHours(teacherToRemove);
    }

    public static void AddSubject(SubjectModel subjectToAdd)
    {
        GlobalSpace.SubjectController.Add(subjectToAdd.ToString());
        _subjectModels.Add(subjectToAdd);
        SubjectsViewModel.GetInstance(null).UpdateSubjects(ref _subjectModels);
    }

    public static void RemoveSubject(SubjectModel subjectToRemove)
    {
        GlobalSpace.SubjectController.Remove(subjectToRemove.ToString());
        _subjectModels.Remove(subjectToRemove);
        SubjectsViewModel.GetInstance(null).UpdateSubjects(ref _subjectModels);
        CheckAndUpdateHours(subjectToRemove);
    }

    public static void AddHour(HourModel hourToAdd)
    {
        GlobalSpace.ClassController.AddSubject(hourToAdd.ClassToString(), hourToAdd.GetSubject(), hourToAdd.HoursAWeek);
        _hourModels.Add(hourToAdd);
        HoursViewModel.GetInstance(null).UpdateHours(ref _hourModels);
    }

    public static void RemoveHour(HourModel hourToRemove)
    {
        GlobalSpace.ClassController.RemoveSubject(hourToRemove.ClassToString(), hourToRemove.GetSubject());
        _hourModels.Remove(hourToRemove);
        HoursViewModel.GetInstance(null).UpdateHours(ref _hourModels);
    }

    public static void GenerateTimetable()
    {
        GlobalSpace.MakePlan();
    }

    public static ObservableCollection<ClassModel> GetClasses()
    {
        return _classModels;
    }

    public static ObservableCollection<TeacherModel> GetTeachers()
    {
        return _teacherModels;
    }

    public static ObservableCollection<SubjectModel> GetSubjects()
    {
        return _subjectModels;
    }

    private static void CheckAndUpdateHours<TModel>(TModel removedModel)
    {
        ObservableCollection<HourModel> hoursToRemove = new ObservableCollection<HourModel>();
        foreach (HourModel currentHour in _hourModels)
        {
            if (removedModel!.Equals(currentHour))
            {
                hoursToRemove.Add(currentHour);
            }
        }
        _hourModels.RemoveMany(hoursToRemove);
        HoursViewModel.GetInstance(null).UpdateHours(ref _hourModels);
    }
}