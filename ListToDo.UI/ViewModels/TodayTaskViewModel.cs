using System.Collections.ObjectModel;
using System.Windows;
using ListToDo.Models;

namespace ListToDo.ViewModels;

public class TodayTaskViewModel : ViewModelBase{
    /*public TodayTaskViewModel(IMainWindowsCodeBehind codeBehind) {
        if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

        _MainCodeBehind = codeBehind;
    }*/

    public TodayTaskViewModel() {
        OnSearchTask();
    }
    /*
    private IMainWindowsCodeBehind _MainCodeBehind;
    */
    public static ObservableCollection<TaskToDo_UI> SearchRezultTasks { get; set; } = new ObservableCollection<TaskToDo_UI>();

    private void OnSearchTask() {
        SearchRezultTasks.Clear();
        IEnumerable<TaskToDo_UI> results = App.Tasks.Where(s => {
            return s.DueDate.DayOfYear == DateTime.Today.DayOfYear;
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