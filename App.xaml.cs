using Microsoft.UI.Xaml;



namespace Calculator
{
   
    public partial class App : Application
    {
        private Window? _window;

        
        public App()
        {
            InitializeComponent();
        }

       
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new Views.MainWindow();
            _window.Activate();
        }
    }
}
