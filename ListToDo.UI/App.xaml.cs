using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ListToDo.BLL;
using ListToDo.Models;

namespace ListToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        public App() {
            /*CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            //culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd HH:mm:ss";
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy HH:mm";
            culture.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = culture;*/
            LoadTasks();
            this.Exit += Saving;

        }
        //экземпляр обращений к данным (вызов обращений к базе)
        private readonly Service _service = new Service();
        //коллекция задач полученная из базы
        public static ObservableCollection<TaskToDo_UI> ExistTasks { get; set; } = new ObservableCollection<TaskToDo_UI>();
        //коллекция задач для добавления в базу
        public static ObservableCollection<TaskToDo_UI> NewTasks { get; set; } = new ObservableCollection<TaskToDo_UI>();
        public static ObservableCollection<int> PriorityTasks { get; set; } = new ObservableCollection<int>(){1,2,3,4,5};
        public event PropertyChangedEventHandler? PropertyChanged;

        void  LoadTasks() {
            var tasks = TaskToDo_UI.Load(_service);
            if (tasks == null) return;
            foreach (var task in tasks) {
                ExistTasks.Add(task);
            }
            
        }
        private void Saving(object sender, EventArgs e) {
            TaskToDo_UI.Save(_service,  NewTasks);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
