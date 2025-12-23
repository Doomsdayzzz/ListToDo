using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ListToDo;

public partial class InputDateComponent : UserControl{
    public static readonly DependencyProperty DateTaskProperty = DependencyProperty.Register(
        nameof(DateTask), typeof(DateTime), typeof(InputDateComponent), new PropertyMetadata(default(DateTime)));

    public DateTime DateTask {
        get { return (DateTime)GetValue(DateTaskProperty); }
        set { SetValue(DateTaskProperty, value); }
    }
    public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
        nameof(LabelText), typeof(string), typeof(InputDateComponent), new PropertyMetadata(default(string)));

    public string LabelText {
        get { return (string)GetValue(LabelTextProperty) ?? string.Empty; }
        set{ SetValue(LabelTextProperty, value); }
    }

    //public DatePicker DatePicker{get;set;}
    public InputDateComponent() {
        InitializeComponent();
        //DateTask = new DatePicker();
        Loaded += (sender, args) => {
            DateTask = DateTime.Now;
            //DateTask.DisplayDate = DateTime.Now;
            //InputDatePicker=DateTask;
            //LabelInput.Content = LabelText;
        };
    }

    private void InputDatePicker_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
        InputDatePicker.IsDropDownOpen = true;
    }
}