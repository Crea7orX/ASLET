using System;
using System.Collections.Generic;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public class SubjectHandler : IHandler
{
    public Dictionary<Guid, Subject> SubjectsDictionary { get; }
    private List<Subject> Subjects { get; }

    public SubjectHandler()
    {
        SubjectsDictionary = new Dictionary<Guid, Subject>();
        Subjects = new List<Subject>();
    }

    public void Add(Guid id, string name)
    {
        Subject newSubject = new Subject(id, name);
        Subjects.Add(newSubject);
        DictionaryUtils.Put(SubjectsDictionary, id, newSubject);
    }

    public void Remove(Guid id)
    {
        if (SubjectsDictionary.ContainsKey(id)) Subjects.Remove(SubjectsDictionary[id]);
        SubjectsDictionary.Remove(id);
    }

    public bool Contains(Guid id)
    {
        return SubjectsDictionary.ContainsKey(id);
    }
}