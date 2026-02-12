using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListToDo.Models;

namespace ListToDo.ViewModels
{
    public class MenuViewModel
    {
        //ctor
        //public MenuViewModel() { }
        public IMainWindowsCodeBehind CodeBehind { get; set; }
        /// <summary>
        /// Переход к первой вьюшке
        /// </summary>
       
        
        //---------добавление задач-----------
        private RelayCommand _LoadAddTaskCommand;
        public RelayCommand LoadAddTaskCommand {
            get { return _LoadAddTaskCommand = _LoadAddTaskCommand ?? new RelayCommand(OnLoadAddTask, CanLoadAddTask); } 
        }
        private bool CanLoadAddTask() { return true; }
        private void OnLoadAddTask() { CodeBehind.LoadView(ViewType.AddTask); }

        //---------поиск задач-----------
        private RelayCommand _LoadSearchTaskCommand;
        public RelayCommand LoadSearchTaskCommand {
            get { return _LoadSearchTaskCommand = _LoadSearchTaskCommand ?? new RelayCommand(OnLoadSearchTask, CanLoadSearchTask);
            }
        }
        private bool CanLoadSearchTask() { return true; }
        private void OnLoadSearchTask() { CodeBehind.LoadView(ViewType.SearchTask); }
        
        //---------задачи на сегодня-----------
        private RelayCommand _LoadTodayTaskCommand;

        public RelayCommand LoadTodayTaskCommand 
            => _LoadTodayTaskCommand= _LoadTodayTaskCommand ?? new RelayCommand(OnLoadTodayTask, CanLoadTodayTask);

        private bool CanLoadTodayTask() { return true; }
        private void OnLoadTodayTask() { CodeBehind.LoadView(ViewType.TodayTask); }
        
        //---------задачи на неделю---------
        private RelayCommand _LoadWeekTaskCommand;

        public RelayCommand LoadWeekTaskCommand
            => _LoadWeekTaskCommand = _LoadWeekTaskCommand ?? new RelayCommand(OnLoadWeekTask, CanLoadWeekTask);
        private bool CanLoadWeekTask() { return true; }
        private void OnLoadWeekTask() { CodeBehind.LoadView(ViewType.WeekTask); }

        //---------задачи на месяц---------
        private RelayCommand _LoadMonthTaskCommand;
        public RelayCommand LoadMonthTaskCommand
            => _LoadMonthTaskCommand = _LoadMonthTaskCommand ?? new RelayCommand(OnLoadMonthTask, CanLoadMonthTask);
        private bool CanLoadMonthTask() { return true; }
        private void OnLoadMonthTask() { CodeBehind.LoadView(ViewType.MonthTask); }
        
        /// <summary>
        /// Возвращение к главной вьюшке
        /// </summary>
        /*private RelayCommand _LoadMainUCCommand;
        public RelayCommand LoadMainUCCommand {
            get { return _LoadMainUCCommand = _LoadMainUCCommand ?? new RelayCommand(OnLoadMainUC, CanLoadMainUC);
            }
        }
        private bool CanLoadMainUC() {
            return true;
        }
        private void OnLoadMainUC() {
            CodeBehind.LoadView(ViewType.Main);
        }*/

    }
}
