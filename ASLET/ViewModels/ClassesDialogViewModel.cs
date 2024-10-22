﻿using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class ClassesDialogViewModel : ViewModelBase
{
    private readonly string _alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦШЩЪЬЮЯ";
    public ReactiveCommand<Unit, StudentsGroupModel> AddClassCommand { get; }
    public ReactiveCommand<Unit, StudentsGroupModel?> CancelCommand { get; }

    public ObservableCollection<byte> Grades { get; } = new();
    public ObservableCollection<char> Letters { get; } = new();

    private byte _selectedGrade;
    public byte SelectedGrade
    {
        get => _selectedGrade;
        private set => this.RaiseAndSetIfChanged(ref _selectedGrade, value);
    }

    private char _selectedLetter;
    public char SelectedLetter
    {
        get => _selectedLetter;
        private set => this.RaiseAndSetIfChanged(ref _selectedLetter, value);
    }
    
    private int _classSize;

    public int ClassSize
    {
        get => _classSize;
        private set => this.RaiseAndSetIfChanged(ref _classSize, value);
    }

    #region DarkMode

    private bool _darkMode;
    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }
    #endregion

    public ClassesDialogViewModel(bool darkMode)
    {
        // TODO CHECKERS FOR VALID INPUT
        AddClassCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new StudentsGroupModel(_selectedGrade, _selectedLetter, _classSize)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<StudentsGroupModel?>(null));

        DarkMode = darkMode;

        FillGrades();
        FillLetters();

        ClassSize = 26;
    }

    private void FillGrades()
    {
        for (byte i = 1; i <= 12; i++)
        {
            Grades.Add(i);
        }

        SelectedGrade = Grades[0];
    }

    private void FillLetters()
    {
        // TODO CHECK FOR EXISTING LETTERS IN CERTAIN GRADE
        foreach (char character in _alphabet)
        {
            Letters.Add(character);
        }

        SelectedLetter = Letters[0];
    }
}