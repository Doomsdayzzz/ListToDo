using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ListToDo.Models;

namespace ListToDo.ViewModels
{
    public class SearchTaskViewModel : ViewModelBase
    {
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        /*
        private IMainWindowsCodeBehind _MainCodeBehind;
        */
        public static ObservableCollection<TaskToDo_UI> SearchRezultTasks { get; set; } = new ObservableCollection<TaskToDo_UI>();
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
        public string InputNameSearchTask
        {
            get { return _InputNameSearchTask; }
            set {
                _InputNameSearchTask = value;
                OnPropertyChanged(nameof(InputNameSearchTask));
            }
        }

        // Описание задачи
        private string _InputDescriptionSearchTask;
        public string InputDescriptionSearchTask
        {
            get { return _InputDescriptionSearchTask; }
            set {
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
        #endregion
        
        

        
        //Commands

        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        private RelayCommand _SearchTaskCommand;
        public RelayCommand SearchTaskCommand
        {
            get
            {
                return _SearchTaskCommand = _SearchTaskCommand ??
                  new RelayCommand(OnSearchTask, CanSearchTask);
            }
        }
        private bool CanSearchTask()
        {
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
            IEnumerable<TaskToDo_UI> results = App.Tasks.Where (s => {
                return s.NameTask == task.NameTask ||
                       s.DescriptionTask == task.DescriptionTask ||
                       s.PriorityTask == task.PriorityTask ||
                       s.DueDate == task.DueDate;
            });
            if (results.Count() == 0) {
                MessageBox.Show($"Данные не найдены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            foreach (var taskToDo in results) {
                SearchRezultTasks.Add(taskToDo);
            }
            //_MainCodeBehind.ShowMessage($"Вы выбрали: {SelectedNumber}");
        }
    }
}
