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
    public static readonly DependencyProperty Is_CheckedDateProperty = DependencyProperty.Register(
        nameof(Is_CheckedDate), typeof(bool), typeof(InputDateComponent), new PropertyMetadata(default(bool)));

    public bool Is_CheckedDate {
        get { return (bool)GetValue(Is_CheckedDateProperty); }
        set { SetValue(Is_CheckedDateProperty, value);  }
    }
    //public DatePicker DatePicker{get;set;}
    public InputDateComponent() {
        InitializeComponent();
        //DateTask = new DatePicker();
        Loaded += (sender, args) => {
            DateTask = DateTime.Now;
            Is_CheckedDate = true;
        };
    }

    private void InputDatePicker_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
        InputDatePicker.IsDropDownOpen = true;
    }
}