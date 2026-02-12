using System.Windows;
using System.Windows.Controls;

namespace ListToDo;

public partial class InputComponent : UserControl{
    public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
        nameof(LabelText), typeof(string), typeof(InputComponent), new PropertyMetadata(default(string)));

    //обозреваемое свойство 
    public string LabelText {
        get { return (string)GetValue(LabelTextProperty); }
        set { SetValue(LabelTextProperty, value); }
    }

    public static readonly DependencyProperty InputTextProperty = DependencyProperty.Register(
        nameof(InputText), typeof(string), typeof(InputComponent), new PropertyMetadata(default(string)));

    public string InputText {
        get { return (string)GetValue(InputTextProperty); }
        set { SetValue(InputTextProperty, value); }
    }
    public static readonly DependencyProperty Is_CheckedProperty = DependencyProperty.Register(
        nameof(Is_Checked), typeof(bool), typeof(InputComponent), new PropertyMetadata(default(bool)));

    public bool Is_Checked {
        get { return (bool)GetValue(Is_CheckedProperty); }
        set { SetValue(Is_CheckedProperty, value);  }
    }
  

    // public static readonly DependencyProperty isVisibility = DependencyProperty.Register(
    //     nameof(Vis), typeof(bool), typeof(InputComponent));

    // public bool Vis {
    //     get { return (bool)GetValue(isVisibility); }
    //     set { SetValue(isVisibility, value); }
    // }
    //public Visibility isVis =>  Vis? Visibility.Visible : Visibility.Hidden;
    public InputComponent() {
        InitializeComponent();
        Loaded += ((sender, args) => {
                    LabelInput.Content = LabelText;
                    Is_Checked=true;
        });
    }
}