using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public string Num1 { get; set; }
        public string Num2 { get; set; }
        private string result { get; set; }
        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> History { get; set; } = new ObservableCollection<string>();
        public string WelcomeMessage { get; set; } = "Welcome to the Calculator App!";
        public string ErrorMessage { get; set; } = "An error occurred. Please check your input and try again.";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Operate(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn)
            {
                Result = ErrorMessage;
                return;
            }
            string? operation = btn.Tag?.ToString();


            if (double.TryParse(Num1, out double A) && double.TryParse(Num2, out double B))
            {
                string expression = "";
                switch (operation)
                {
                    case "+":
                        Result = (A + B).ToString("N2");
                        expression = $"{A} + {B} = {Result}";
                        break;
                    case "-":
                        Result = (A - B).ToString("N2");
                        expression = $"{A} - {B} = {Result}";
                        break;
                    case "*":
                        Result = (A * B).ToString("N2");
                        expression = $"{A} * {B} = {Result}";
                        break;
                    case "/":
                        Result = B == 0 ? "Cannot divide by zero" : (A / B).ToString("N2");
                        expression = B == 0 ? $"Cannot divide {A} by zero" : $"{A} / {B} = {Result}";
                        break;
                    default:
                        Result = ErrorMessage;
                        break;
                }
                History.Insert(0, expression);
            }
            else
            {
                Result = ErrorMessage;
            }
        }

    }
}

