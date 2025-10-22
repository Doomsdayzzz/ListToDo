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
    /// <summary>
    /// Показ сообщения для пользователя
    /// </summary>
    /// <param name="message">текст сообщения</param>
    //void ShowMessage(string message);

    /// <summary>
    /// Загрузка нужной View
    /// </summary>
    /// <param name="view">экземпляр UserControl</param>
    void LoadView(ViewType typeView);
}
/// <summary>
/// Типы вьюшек для загрузки
/// </summary>
public enum ViewType
{
    Main,
    AddTask,
    SearchTask,
    TodayTask,
    WeekTask
}
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window,  IMainWindowsCodeBehind{
    public MainWindow() {
        InitializeComponent();
        this.Loaded += MainWindow_Loaded;
    }
    
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //загрузка вьюмодел для кнопок меню
        MenuViewModel vm = new MenuViewModel();
        //даем доступ к этому кодбихайнд
        vm.CodeBehind = this;
        //делаем эту вьюмодел контекстом данных
        this.DataContext = vm;

        //загрузка стартовой View
        LoadView(ViewType.TodayTask);
    }



    public void LoadView(ViewType typeView)
    {
        switch (typeView)
        {
            case ViewType.Main:
                //загружаем вьюшку, ее вьюмодель
                MainUC view = new MainUC();
                MainViewModel vm = new MainViewModel(this);
                //связываем их м/собой
                view.DataContext = vm;
                //отображаем
                this.OutputView.Content = view;
                break;
            case ViewType.AddTask:
                AddTask viewF = new AddTask();
                AddTaskViewModel vmF = new AddTaskViewModel(this);
                viewF.DataContext = vmF;
                this.OutputView.Content = viewF;
                break;
            case ViewType.SearchTask:
                SearchTask viewS = new SearchTask();
                SearchTaskViewModel vmS = new SearchTaskViewModel(this);
                viewS.DataContext = vmS;
                this.OutputView.Content = viewS;
                break;
            case ViewType.TodayTask:
                TodayTasks viewToday = new TodayTasks();
                TodayTaskViewModel vmToday = new TodayTaskViewModel(this);
                viewToday.DataContext = vmToday;
                this.OutputView.Content = viewToday;
                break;
            case ViewType.WeekTask:
                WeekTasks viewWeek = new WeekTasks();
                WeekTaskViewModel vmWeek = new WeekTaskViewModel(this);
                viewWeek.DataContext = vmWeek;
                this.OutputView.Content = viewWeek;
                break;
        }

            
    }
}