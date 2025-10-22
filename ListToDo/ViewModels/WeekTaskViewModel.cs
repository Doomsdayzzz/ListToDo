using System.Collections.ObjectModel;
using System.Windows;
using ListToDo.Models;

namespace ListToDo.ViewModels;

public class WeekTaskViewModel : ViewModelBase{
    public WeekTaskViewModel(IMainWindowsCodeBehind codeBehind) {
        if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

        _MainCodeBehind = codeBehind;
        OnSearchTask();
    }

    private IMainWindowsCodeBehind _MainCodeBehind;
    public static ObservableCollection<TaskToDo> SearchRezultTasks { get; set; } = new ObservableCollection<TaskToDo>();

    private void OnSearchTask() {
        SearchRezultTasks.Clear();
        IEnumerable<TaskToDo> results = App.Tasks.Where(s => {
            return s.DueDate.DayOfYear >= DateTime.Today.DayOfYear
                && s.DueDate.DayOfYear <= DateTime.Today.DayOfYear+7;
        });
        if (results.Count() == 0) {
            //MessageBox.Show($"Данные не найдены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        foreach (var taskToDo in results) {
            SearchRezultTasks.Add(taskToDo);
        }
        
    }
}