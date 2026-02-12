using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ListToDo.Models;
using Microsoft.Win32;

namespace ListToDo.ViewModels{
    public class SearchTaskViewModel : ViewModelBase{
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        /*
        private IMainWindowsCodeBehind _MainCodeBehind;
        */
        public static ObservableCollection<TaskToDo_UI> SearchRezultTasks { get; } = [];
        //public static ObservableCollection<TaskToDo_UI> SearchRezultTasks { get; set; }
        //ctor
        /*public SearchTaskViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }*/

        //Properties

        #region Properties

        //поле поиска по имени задачи
        private string _InputNameSearchTask;

        public string InputNameSearchTask {
            get { return _InputNameSearchTask; }
            set
            {
                _InputNameSearchTask = value;
                OnPropertyChanged(nameof(InputNameSearchTask));
            }
        }

        // Описание задачи
        private string _InputDescriptionSearchTask;

        public string InputDescriptionSearchTask {
            get { return _InputDescriptionSearchTask; }
            set
            {
                _InputDescriptionSearchTask = value;
                OnPropertyChanged(nameof(InputDescriptionSearchTask));
            }
        }

        //Дата исполнения задачи
        private DateTime _InputSearchDate;

        public DateTime InputSearchDate {
            get { return _InputSearchDate; }
            set
            {
                _InputSearchDate = value;
                OnPropertyChanged(nameof(InputSearchDate));
            }
        }

        //Приоритет
        private int _InputSearchPriority;

        public int InputSearchPriority {
            get { return _InputSearchPriority; }
            set
            {
                _InputSearchPriority = value;
                OnPropertyChanged(nameof(InputSearchPriority));
            }
        }

        //хранит статус переключателя 
        private bool _IsCheckedToggleName;

        public bool Is_CheckedToggleName {
            get { return _IsCheckedToggleName; }
            set
            {
                _IsCheckedToggleName = value;
                OnPropertyChanged(nameof(Is_CheckedToggleName));
            }
        }

        private bool _IsCheckedToggleDes;

        public bool Is_CheckedToggleDes {
            get { return _IsCheckedToggleDes; }
            set
            {
                _IsCheckedToggleDes = value;
                OnPropertyChanged(nameof(Is_CheckedToggleDes));
            }
        }

        private bool _IsCheckedToggleDate;

        public bool Is_CheckedToggleDate {
            get { return _IsCheckedToggleDate; }
            set
            {
                _IsCheckedToggleDate = value;
                OnPropertyChanged(nameof(Is_CheckedToggleDate));
            }
        }

        private bool _IsCheckedTogglePrior;

        public bool Is_CheckedTogglePrior {
            get { return _IsCheckedTogglePrior; }
            set
            {
                _IsCheckedTogglePrior = value;
                OnPropertyChanged(nameof(Is_CheckedTogglePrior));
            }
        }

        #endregion


        //Commands

        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        private RelayCommand _SearchTaskCommand;

        public RelayCommand SearchTaskCommand {
            get
            {
                return _SearchTaskCommand = _SearchTaskCommand ??
                                            new RelayCommand(OnSearchTask, CanSearchTask);
            }
        }

        private bool CanSearchTask() {
            return true;
        }

        private void OnSearchTask() {
            SearchRezultTasks.Clear();
            var task = new TaskToDo_UI {
                NameTask = InputNameSearchTask,
                DescriptionTask = InputDescriptionSearchTask,
                PriorityTask = InputSearchPriority,
                DueDate = InputSearchDate
            };
            //-----поиск в отдельном потоке-------
            var t = Task.Run(() => {
                IEnumerable<TaskToDo_UI> results = App.ExistTasks.Where(s => {
                    return (Check(s.NameTask, task.NameTask, Is_CheckedToggleName) &&
                            Check(s.DescriptionTask, task.DescriptionTask, Is_CheckedToggleDes) &&
                            Check(s.PriorityTask, task.PriorityTask, Is_CheckedTogglePrior) &&
                            Check(s.DueDate, task.DueDate, Is_CheckedToggleDate)
                        );
                    // return (s.NameTask == task.NameTask && Is_CheckedToggleName) ||
                    //        (s.DescriptionTask == task.DescriptionTask && Is_CheckedToggleDes) ||
                    //        (s.PriorityTask == task.PriorityTask && Is_CheckedTogglePrior) ||
                    //        (s.DueDate == task.DueDate && Is_CheckedToggleDate);
                });
                var taskToDoUis = results.ToList();
                if (!taskToDoUis.Any()) {
                    MessageBox.Show($"Данные не найдены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                //без диспетчера ну никак(((
                foreach (var item in taskToDoUis) {
                    Application.Current.Dispatcher.Invoke(() => { SearchRezultTasks.Add(item); });
                }

                //проверка совпадения полей при условии отмеченного чекбокса
                bool Check<T>(T va1, T va2, bool param) {
                    if (!param) {
                        return true;
                    }
                    else if (va1 == null && va2 == null) {
                        return true;
                    }
                    else if (va1 == null || va2 == null) {
                        return false;
                    }
                    else if (typeof(T) == typeof(string)) {
                        return va1.ToString().ToLower().Contains(va2.ToString().ToLower());
                    }
                    else return va1 != null && va1.Equals(va2);
                }
            });
            //t.Wait();
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
                MessageBox.Show($"Нет данных для сохранения", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string FilePath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true) {
                FilePath = saveFileDialog.FileName+".txt";
            }

            //------------ЗАПИСЬ НА ДИСК-------------
            Task.Run(() => {
                using (StreamWriter file = new StreamWriter(FilePath)) //пишем настройки в файл
                {
                    file.WriteLineAsync($"Дата сохранения: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} \n");
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
}