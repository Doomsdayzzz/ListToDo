using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ListToDo.Models;

namespace ListToDo.ViewModels
{
    public class AddTaskViewModel : ViewModelBase
    {
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        /*private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public AddTaskViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            }*/

        //Properties

        /// <summary>
        /// Введенная строка в TextBox
        /// </summary>
        private string _InputNameNewTask;
        public string InputNameNewTask
        {
            get { return _InputNameNewTask; }
            set {
                _InputNameNewTask = value;
                OnPropertyChanged(nameof(InputNameNewTask));
            }
        }
        
       // Описание задачи
       private string _InputDescriptionNewTask;
        public string InputDescriptionNewTask
        {
            get { return _InputDescriptionNewTask; }
            set {
                _InputDescriptionNewTask = value;
                OnPropertyChanged(nameof(InputDescriptionNewTask));
            }
        }
        //Дата исполнения задачи
        private DateTime _InputDueDate;

        public DateTime InputDueDate {
            get { return _InputDueDate; }
            set
            {
                _InputDueDate = value;
                OnPropertyChanged(nameof(InputDueDate));
            }
        }

        //Приоритет
        private int _InputPriority;
        public int InputPriority {
            get { return _InputPriority; }
            set
            {
                _InputPriority = value;
                OnPropertyChanged(nameof(InputPriority));
            }
        }
        //Commands

        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        private RelayCommand _AddTaskCommand;
        public RelayCommand AddTaskCommand
        {
            get
            {
                return _AddTaskCommand = _AddTaskCommand ??
                  new RelayCommand(OnAddTask, CanAddTask);
            }
        }
        private bool CanAddTask()
        {
            return true;
        }
        private void OnAddTask() {
            //var date = InputDueDate.SelectedDate ?? DateTime.Now;
           //InputDueDate = DateTime.Now;
            App.Tasks.Add(new TaskToDo_UI {
               NameTask = InputNameNewTask, 
               DescriptionTask = InputDescriptionNewTask, 
               PriorityTask = InputPriority, 
               DueDate = InputDueDate
            });
           
           MessageBox.Show($"" +
                           $"\n{InputNameNewTask}" +
                           $"\n{InputDescriptionNewTask}" +
                           $"\n{InputPriority}" +
                           $"\n{InputDueDate.ToShortDateString()}", "Задача добавлена!", MessageBoxButton.OK, MessageBoxImage.Information);
           InputNameNewTask = string.Empty;
           InputDescriptionNewTask = string.Empty;
           InputPriority = 5;
           InputDueDate = DateTime.Now;
           //var dPicker = new DatePicker();
           /*InputDueDate.DisplayDate = DateTime.Now;
           InputDueDate.SelectedDate = InputDueDate.DisplayDate;*/
           //InputDueDate =  dPicker;
           //InputDueDate.DisplayDate = DateTime.Now;
           //InputDueDate.SelectedDate = DateTime.Now;
        }

    }
}
