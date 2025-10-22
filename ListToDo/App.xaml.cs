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

        
        public static ObservableCollection<TaskToDo> Tasks { get; set; } = new ObservableCollection<TaskToDo>();
        public static ObservableCollection<int> PriorityTasks { get; set; } = new ObservableCollection<int>(){1,2,3,4,5};
        public event PropertyChangedEventHandler? PropertyChanged;

        void LoadTasks() {
            var tasks = TaskToDo.Load();
            if (tasks == null) return;
            foreach (var task in tasks) {
                Tasks.Add(task);
            }
        }
        private void Saving(object sender, EventArgs e) {
            TaskToDo.Save(Tasks);
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
