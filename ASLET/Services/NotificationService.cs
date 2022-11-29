using Avalonia.Controls;
using MessageBox.Avalonia.Enums;

namespace ASLET.Services;

public static class NotificationService
{
    public static void ShowSuccess(Window parent, string message)
    {
        MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Успешно", message, ButtonEnum.Ok, Icon.Success,
            WindowStartupLocation.CenterOwner).ShowDialog(parent);
    }
    
    public static void ShowWarning(Window parent, string message)
    {
        MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Внимание", message, ButtonEnum.Ok, Icon.Warning,
            WindowStartupLocation.CenterOwner).ShowDialog(parent);
    }

    public static void ShowError(Window parent, string message)
    {
        MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Грешка", message, ButtonEnum.Ok, Icon.Error,
            WindowStartupLocation.CenterOwner).ShowDialog(parent);
    }
}