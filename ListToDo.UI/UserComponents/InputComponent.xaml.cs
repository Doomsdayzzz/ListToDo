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
    
    //public string LabelText {get; set;}
    //public string InputText {get; set;}
    public InputComponent() {
        InitializeComponent();
        Loaded+=((sender, args) => LabelInput.Content = LabelText );
    }
}