using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ListToDo.Models;
using Microsoft.Win32;

namespace ListToDo.ViewModels;

public class WeekTaskViewModel : ViewModelBase{
    // public WeekTaskViewModel(IMainWindowsCodeBehind codeBehind) {
    //     if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
    //
    //     _MainCodeBehind = codeBehind;
    // }

    public WeekTaskViewModel() {
        OnSearchTask();
    }

    // private IMainWindowsCodeBehind _MainCodeBehind;
    public static ObservableCollection<TaskToDo_UI> SearchRezultTasks { get; set; } =
        new ObservableCollection<TaskToDo_UI>();

    private void OnSearchTask() {
        SearchRezultTasks.Clear();
        //-----поиск в отдельном потоке-------
        Task.Run(() => {
            IEnumerable<TaskToDo_UI> results = App.ExistTasks.Where(s => {
                return s.DueDate.DayOfYear >= DateTime.Today.DayOfYear
                       && s.DueDate.DayOfYear <= DateTime.Today.DayOfYear + 7;
            });
            var taskToDoUis = results.ToList();
            if (!taskToDoUis.Any()) {
                //MessageBox.Show($"Данные не найдены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //без диспетчера ну никак(((
            foreach (var item in taskToDoUis) {
                Application.Current.Dispatcher.Invoke(() => { SearchRezultTasks.Add(item); });
            }
        });
    }

    private RelayCommand _SaveSearchTaskCommand;

    public RelayCommand SaveSearchTaskCommand {
        get
        {
            return _SaveSearchTaskCommand =
                _SaveSearchTaskCommand ?? new RelayCommand(OnSaveSearchTask, CanSaveSearchTask);
        }
    }

    private bool CanSaveSearchTask() {
        return true;
    }

    private void OnSaveSearchTask() {
        if (SearchRezultTasks.Count == 0) {
            MessageBox.Show($"Нет данных для сохранения", "Сохранение", MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        string FilePath = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true) {
            FilePath = saveFileDialog.FileName + ".txt";
        }

        //------------ЗАПИСЬ НА ДИСК-------------
        Task.Run(() => {
            using (StreamWriter file = new StreamWriter(FilePath)) //пишем настройки в файл
            {
                file.WriteLine(
                    $"Дата сохранения: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} \n");
                foreach (var task in SearchRezultTasks) {
                    file.WriteLine($"Имя задачи: {task.NameTask}");
                    file.WriteLine($"Описание: {task.DescriptionTask}");
                    file.WriteLine($"Приоритет: {task.PriorityTask} ");
                    file.WriteLine($"Дата исполнения: {task.DueDate.ToShortDateString()}");
                    file.WriteLine("----------------------");
                }
            }

            MessageBox.Show($"Данные сохранены", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        });
    }
}