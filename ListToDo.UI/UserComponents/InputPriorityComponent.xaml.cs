using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ListToDo;

public partial class InputPriorityComponent : UserControl{
    public string LabelText {get; set;}

    public static readonly DependencyProperty PriorityTaskProperty = DependencyProperty.Register(
        nameof(PriorityTask), typeof(int), typeof(InputPriorityComponent), new PropertyMetadata(default(int)));

    public int PriorityTask {
        get { return (int)GetValue(PriorityTaskProperty); }
        set { SetValue(PriorityTaskProperty, value); }
    }
    /*public ObservableCollection<int> PriorityList {
        get { return App.PriorityTasks; }
        set { App.PriorityTasks = value; }
    }*/
    public InputPriorityComponent() {
        InitializeComponent();
        Loaded += ((sender, args) => {
                    LabelInput.Content = LabelText??"Priority Task";
                    InputPriority.ItemsSource = App.PriorityTasks.ToList();
                    InputPriority.SelectedIndex = 4;
                }
            );
        
    }

    private void Input_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        var comboBox = sender as ComboBox;
        if (comboBox == null) return;
        InputPriority.SelectedIndex=comboBox.SelectedIndex;
    }
}