using System;
using ASLET.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace ASLET.Views;

public partial class ClassesDialogWindow : ReactiveWindow<ClassesDialogViewModel>
{
    public ClassesDialogWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.AddClassCommand.Subscribe(Close)));
        this.WhenActivated(d => d(ViewModel!.CancelCommand.Subscribe(Close)));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}