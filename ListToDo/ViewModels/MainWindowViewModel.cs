using System.Collections.ObjectModel;

namespace ListToDo.ViewModels;

public class MainWindowViewModel: ViewModelBase{
    //private IMainWindowsCodeBehind _MainCodeBehind;
    private string? title;
    private string? description;

    /*public MainWindowViewModel(IMainWindowsCodeBehind codeBehind) {
        if (codeBehind == null) 
            throw new ArgumentNullException(nameof(codeBehind));
        _MainCodeBehind = codeBehind;
    }*/
    public string Title {
        get => title;
        set => SetField(ref title, value);
    }

    /*public ObservableCollection<Task> Tasks { get; set; } = new();
    private void FillTasks(IEnumerable<Task> tasks)
    {
        Tasks.Clear();
        foreach (var task in Tasks) {
            Tasks.Add(task);
        }
    }*/

}