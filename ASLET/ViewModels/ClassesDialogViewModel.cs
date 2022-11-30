using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ASLET.Views;
using Avalonia.Controls;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace ASLET.ViewModels;

public class ClassesDialogViewModel : ViewModelBase
{
    private readonly string _alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦШЩЪЬЮЯ";
    public ReactiveCommand<Unit, StudentsGroupModel> AddClassCommand { get; }
    
    private bool _addClassEnabled;
    public bool AddClassEnabled
    {
        get => _addClassEnabled;
        private set => this.RaiseAndSetIfChanged(ref _addClassEnabled, value);
    }

    public ReactiveCommand<Unit, StudentsGroupModel?> CancelCommand { get; }

    public ObservableCollection<byte> Grades { get; } = new();
    public ObservableCollection<char> Letters { get; } = new();

    private byte _selectedGrade;
    public byte SelectedGrade
    {
        get => _selectedGrade;
        private set
        {
            this.RaiseAndSetIfChanged(ref _selectedGrade, value);
            ValidateInput();
            Letters.Clear();
            FillLetters();
        }
    }

    private char _selectedLetter;
    public char SelectedLetter
    {
        get => _selectedLetter;
        private set
        {
            this.RaiseAndSetIfChanged(ref _selectedLetter, value);
            ValidateInput();
        }
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
        AddClassCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new StudentsGroupModel(_selectedGrade, _selectedLetter, _classSize)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<StudentsGroupModel?>(null));

        DarkMode = darkMode;

        FillGrades();
        FillLetters();

        ClassSize = 26;
    }

    private void ValidateInput()
    {
        if (_selectedGrade != 0 && _selectedLetter != 0) AddClassEnabled = true;
        else AddClassEnabled = false;
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
        ObservableCollection<StudentsGroupModel> studentsGroups = ConfigurationService.Instance.GetGroups();
        foreach (char character in _alphabet)
        {
            if (studentsGroups.Any(group => group.Grade == _selectedGrade && group.Letter == character)) continue;
            Letters.Add(character);
        }

        SelectedLetter = Letters[0];
    }
}