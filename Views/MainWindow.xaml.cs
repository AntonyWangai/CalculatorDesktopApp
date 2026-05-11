using Calculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;



namespace Calculator.Views
{

    public sealed partial class MainWindow : Window
    {
        public CalculatorViewModel ViewModel { get;} = new();
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new CalculatorViewModel();

        }

       
    }
}
