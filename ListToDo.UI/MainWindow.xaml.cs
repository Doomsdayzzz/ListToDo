using ListToDo.Views;
using ListToDo.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListToDo;
public interface IMainWindowsCodeBehind
{
   // интерфейс для реализации метода загрузки нужной View из списка ViewType
    void LoadView(ViewType typeView);
}

// Типы вьюшек для загрузки
public enum ViewType
{
    Main,
    AddTask,
    SearchTask,
    TodayTask,
    WeekTask,
    MonthTask,
}

public partial class MainWindow : Window,  IMainWindowsCodeBehind{
    public MainWindow() {
        InitializeComponent();
        this.Loaded += MainWindow_Loaded;
    }
    
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        MenuViewModel vm = new MenuViewModel(); //загрузка вьюмодел для кнопок меню
        vm.CodeBehind = this;   //даем доступ к этому внутреннему коду
        this.DataContext = vm;  //делаем MenuViewModel контекстом данных текущего главного окна

        LoadView(ViewType.TodayTask); //загрузка стартовой View
    }



    public void LoadView(ViewType typeView) //реализация по контракту интерфейса
    {
        switch (typeView)
        {
            // case ViewType.Main:
            //     MainUC view = new MainUC(); 
            //     MainViewModel vm = new MainViewModel(/*this*/);
            //     view.DataContext = vm;
            //     this.OutputView.Content = view;
            //     break;
            case ViewType.AddTask:
                AddTask viewF = new AddTask();  //создаем мини-окно(вьюшку), ее вьюмодель
                AddTaskViewModel vmF = new AddTaskViewModel(/*this*/);
                viewF.DataContext = vmF;  //связываем их м/собой
                this.OutputView.Content = viewF; //отображаем пользовательское мини-окно в главном окне через поле ContentPresenter
                break;
            case ViewType.SearchTask:
                SearchTask viewS = new SearchTask();
                SearchTaskViewModel vmS = new SearchTaskViewModel(/*this*/);
                viewS.DataContext = vmS;
                this.OutputView.Content = viewS;
                break;
            case ViewType.TodayTask:
                TodayTasks viewToday = new TodayTasks();
                TodayTaskViewModel vmToday = new TodayTaskViewModel(/*this*/);
                viewToday.DataContext = vmToday;
                this.OutputView.Content = viewToday;
                break;
            case ViewType.WeekTask:
                WeekTasks viewWeek = new WeekTasks();
                WeekTaskViewModel vmWeek = new WeekTaskViewModel(/*this*/);
                viewWeek.DataContext = vmWeek;
                this.OutputView.Content = viewWeek;
                break;
            case ViewType.MonthTask:
                MonthTasks viewMonth = new MonthTasks();
                MonthTaskViewModel vmMonth = new MonthTaskViewModel(/*this*/);
                viewMonth.DataContext = vmMonth;
                this.OutputView.Content = viewMonth;
                break;
        }

            
    }
}