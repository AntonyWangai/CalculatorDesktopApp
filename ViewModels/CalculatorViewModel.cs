using Microsoft.UI.Xaml.Documents;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        // Constructor: initializes the command and connects logic + validation
        public CalculatorViewModel()
        {
            // RelayCommand wraps ExecuteOperation and CanExecuteOperation
            OperationCommand = new RelayCommand(ExecuteOperation, CanExecuteOperation);
        }

       //First Input
        private string _num1;

        // Number input bound from UI (TwoWay binding)
        public string Num1
        {
            get => _num1;
            set
            {
                _num1 = value;

                // Notify UI that Num1 changed
                OnPropertyChanged();

                // Force UI to re-check if buttons should be enabled/disabled
                (OperationCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        // Second input
        private string _num2;

        // Number input bound from UI (TwoWay binding)
        public string Num2
        {
            get => _num2;
            set
            {
                _num2 = value;

                // Notify UI that Num2 changed
                OnPropertyChanged();

                // Re-evaluate CanExecute (enable/disable buttons)
                (OperationCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        // Backing field for calculation result
        private string _result;

        // Result displayed in UI
        public string Result
        {
            get => _result;
            set
            {
                _result = value;

                // Notify UI that Result changed
                OnPropertyChanged();
            }
        }

        
        public string WelcomeMessage { get; set; } =
            "Welcome to the Calculator App!";

        public string ErrorMessage { get; set; } =
            "An error occurred. Please check your input and try again.";

        // Event required by INotifyPropertyChanged (updates UI automatically)
        public event PropertyChangedEventHandler? PropertyChanged;

        // Stores calculation history
        public ObservableCollection<string> History { get; } = new();

        // Command used by all calculator buttons
        public ICommand OperationCommand { get; }

        // Determines if the button should be enabled or disabled
        private bool CanExecuteOperation(object? parameter)
        {
            // Ensure both inputs are valid numbers
            if (!double.TryParse(Num1, out double A) ||
                !double.TryParse(Num2, out double B))
            {
                return false;
            }

            // Prevent division by zero
            if (parameter is string op && op == "/" && B == 0)
            {
                return false;
            }

            
            return true;
        }

        // Notifies UI about property changes 
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Main calculator logic executed when a button is clicked
        private void ExecuteOperation(object? parameter)
        {
            // Validate inputs before calculation
            if (!double.TryParse(Num1, out double A) ||
                !double.TryParse(Num2, out double B))
            {
                Result = ErrorMessage;
                return;
            }

            string expression;

            // Determine operation based on button CommandParameter
            switch (parameter)
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
                    Result = (A / B).ToString("N2");
                    expression = $"{A} / {B} = {Result}";
                    break;

                default:
                    return;
            }

            // Add result to history (auto-updates UI)
            History.Insert(0, expression);
        }
    }
}