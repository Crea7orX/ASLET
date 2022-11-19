using System.Collections.Generic;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public class SubjectHandler : IHandler
{
    public Dictionary<string, Subject> SubjectsDictionary { get; }
    private List<Subject> Subjects { get; }

    public SubjectHandler()
    {
        SubjectsDictionary = new Dictionary<string, Subject>();
        Subjects = new List<Subject>();
    }

    public void Add(string name)
    {
        Subject newSubject = new Subject(name);
        Subjects.Add(newSubject);
        DictionaryUtils.Put(SubjectsDictionary, name, newSubject);
    }

    public void Remove(string name)
    {
        Subjects.Remove(SubjectsDictionary[name]);
        SubjectsDictionary.Remove(name);
    }

    public bool Contains(string name)
    {
        return SubjectsDictionary.ContainsKey(name);
    }
}