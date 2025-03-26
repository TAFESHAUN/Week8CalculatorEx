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

namespace Week8CalculatorEx;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string _currentOperator;
    private double _firstNumberValue;
    private bool _secondNumberValue;
    private string _displayCalculation;

    public MainWindow()
    {
        InitializeComponent();
        _displayCalculation = string.Empty;
    }

    private void OnNumberClicked(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var number = button.Content.ToString();

        if (_secondNumberValue)
        {
            displayRes.Text = number;
            _secondNumberValue = false;
        }
        else
        {
            displayRes.Text = displayRes.Text == "0" ? number : displayRes.Text + number;
        }

        _displayCalculation += number;
        UpdateDisplayValue();
    }

    private void OnOperatorClicked(string operatorSymbol)
    {
        _currentOperator = operatorSymbol;
        _firstNumberValue = double.Parse(displayRes.Text);
        _secondNumberValue = true;

        _displayCalculation += " " + operatorSymbol + " ";
        UpdateDisplayValue();
    }

    private void OnMultiplyClicked(object sender, RoutedEventArgs e) => OnOperatorClicked("*");
    private void OnDivideClicked(object sender, RoutedEventArgs e) => OnOperatorClicked("/");
    private void OnMinusClicked(object sender, RoutedEventArgs e) => OnOperatorClicked("-");
    private void OnPlusClicked(object sender, RoutedEventArgs e) => OnOperatorClicked("+");

    private void OnClearClicked(object sender, RoutedEventArgs e)
    {
        _firstNumberValue = 0;
        _secondNumberValue = false;
        _displayCalculation = string.Empty;
        displayRes.Text = "0";
    }

    private void OnEqualsClicked(object sender, RoutedEventArgs e)
    {
        double secondValOtherSide = GetSecondNumberValue();
        double calcTotal = 0;

        switch (_currentOperator)
        {
            case "+": calcTotal = _firstNumberValue + secondValOtherSide; break;
            case "-": calcTotal = _firstNumberValue - secondValOtherSide; break;
            case "*": calcTotal = _firstNumberValue * secondValOtherSide; break;
            case "/":
                calcTotal = secondValOtherSide != 0 ? _firstNumberValue / secondValOtherSide : 0;
                break;
        }

        displayRes.Text = calcTotal.ToString();
        _displayCalculation = string.Empty;
    }

    private double GetSecondNumberValue()
    {
        int operatorSelect = _displayCalculation.IndexOf(_currentOperator);
        string secondNumberInCurrCalc = _displayCalculation.Substring(operatorSelect + 2);
        return double.TryParse(secondNumberInCurrCalc, out double result) ? result : 0;
    }

    private void UpdateDisplayValue()
    {
        displayRes.Text = _displayCalculation;
    }
}