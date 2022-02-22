using Calculator.Command;
using Calculator.Dto;
using Calculator.Converter;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator.Util;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private CalculationContext context;
        private AbstractCommand nextCommand;

        private bool isBurgerButtonActivated;

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
            context = new CalculationContext();
            context.Arguments = new Arguments();
            HoldMenu.Width = new GridLength(0, GridUnitType.Star);
        }

        private void Num_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberButtonClick(((Button) sender).Content.ToString());
        }

        private void Num_0_Button_Click(object sender, RoutedEventArgs e)
        {
            if (context.ShouldReplaceResult)
            {
                MainScreen.Text = WindowUtil.ZERO;
            }
            else if (MainScreen.Text != WindowUtil.ZERO)
            {
                MainScreen.Text += WindowUtil.ZERO;
            }
        }

        private void Num_00_Button_Click(object sender, RoutedEventArgs e)
        {
            if (context.ShouldReplaceResult)
            {
                MainScreen.Text = WindowUtil.ZERO;
            }
            else if (MainScreen.Text != WindowUtil.ZERO)
            {
                MainScreen.Text += "00";
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            MainScreen.Text = WindowUtil.ZERO;
            ExpressionScreen.Text = WindowUtil.EMPTY;

            RevertLatestArithmeticOperationButtonColor();
            nextCommand = null;

            context.IsDotPressed = false;
            context.IsArithmeticOperationPressed = false;
            context.ShouldReplaceResult = false;
        }

        private void BackSpace_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleBackspaceButtonClick();
        }

        private void Equal_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleEqualButtonClick();
        }

        private void CE_Button_Click(object sender, RoutedEventArgs e)
        {
            RevertLatestArithmeticOperationButtonColor();
            nextCommand = CommandRecorder.Revert();
            if (nextCommand != null)
            {
                nextCommand.Button.Background = WindowUtil.ACTIVE_OPERATION_BRUSH;
                ExpressionScreen.Text = nextCommand.Undo();
                MainScreen.Text = nextCommand.Snapshot.Arguments.SecondArg == null 
                    ? nextCommand.Exception.Message 
                    : nextCommand.Snapshot.Arguments.SecondArg.ToString();
                context = ContextConverter.ToContext(nextCommand.Snapshot);
            }
        }

        private void Dot_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleDotButtonClick();
        }

        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleTwoArgArithmeticOperation(new PlusComand(PlusButton));
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleTwoArgArithmeticOperation(new MinusCommand(MinusButton));
        }

        private void Multiply_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleTwoArgArithmeticOperation(new MultiplyCommand(MultiplyButton));
        }

        private void Divide_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleTwoArgArithmeticOperation(new DivideCommand(DivideButton));
        }

        private void Pi_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleConstantButtonClick(Math.PI);
        }

        private void E_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleConstantButtonClick(Math.E);
        }

        private void Radical_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleSingleArgArithmeticOperation(new RadicalCommand(RadicalButton));
        }

        private void Pow_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleTwoArgArithmeticOperation(new PowCommand(PowButton));
        }

        private void Ln_Button_Click(object sender, RoutedEventArgs e)
        {
            HandleSingleArgArithmeticOperation(new LnCommand(LnButton));
        }

        private void Screen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                HandleBackspaceButtonClick();
            }
            else if (WindowUtil.NUMBER_KEYS.Contains(e.Key))
            {
                string key = e.Key.ToString();
                HandleNumberButtonClick(key.Substring(key.Length - 1));
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                HandleDotButtonClick();
            }

            MainScreen.SelectionStart = MainScreen.Text.Length;
            e.Handled = true;
        }

        private void BurgerButton_Click(object sender, RoutedEventArgs e)
        {
            double wdth = CalculatorWindow.MinWidth;
            if (!isBurgerButtonActivated)
            {
                CalculatorWindow.MinWidth += wdth / 5;
                HoldMenu.Width = new GridLength(1, GridUnitType.Star);
                isBurgerButtonActivated = true;
            }
            else
            {
                HoldMenu.Width = new GridLength(0, GridUnitType.Star);
                isBurgerButtonActivated = false;
                CalculatorWindow.MinWidth = WindowUtil.INITIAL_MIN_WIDTH;
                if (CalculatorWindow.Width <= wdth + 1)
                {
                    CalculatorWindow.Width = WindowUtil.INITIAL_MIN_WIDTH;
                }
            }
        }

        private void HandleNumberButtonClick(string number)
        {
            if (context.ShouldReplaceResult) 
            {
                context.ShouldReplaceResult = false;
                ExpressionScreen.Text = WindowUtil.EMPTY;
                MainScreen.Text = number;
            } 
            else if (MainScreen.Text == WindowUtil.ZERO)
            {
                MainScreen.Text = number;
            }
            else
            {
                MainScreen.Text += number;
            }
        }

        private void HandleConstantButtonClick(double value)
        {
            context.ShouldReplaceResult = true;
            MainScreen.Text = value.ToString();
        }

        private void HandleSingleArgArithmeticOperation(AbstractCommand command)
        {
            command.Button.Background = WindowUtil.ACTIVE_OPERATION_BRUSH;
            context.ShouldReplaceResult = true;

            if (double.TryParse(MainScreen.Text, out double arg)) 
            {
                context.Arguments.FirstArg = arg;
                command.Snapshot = ContextConverter.ToSnapshot(context);
                ExecuteCommand(command);
            }
        }

        private void HandleEqualButtonClick()
        {
            if (nextCommand == null)
            {
                return;
            }

            RevertLatestArithmeticOperationButtonColor();

            context.IsArithmeticOperationPressed = false;
            context.ShouldReplaceResult = true;

            context.Arguments.SecondArg = double.Parse(MainScreen.Text);
            nextCommand.Snapshot = ContextConverter.ToSnapshot(context);

            ExpressionScreen.Text = WindowUtil.EMPTY;
            ExecuteCommand(nextCommand);

            nextCommand = null;
            context.Arguments.SecondArg = null;
        }

        private void ExecuteCommand(AbstractCommand command)
        {
            try
            {
                MainScreen.Text = command.Calculate().ToString();
            }
            catch (ArithmeticException exc)
            {
                MainScreen.Text = exc.Message;
            }
        }

        private void HandleTwoArgArithmeticOperation(AbstractCommand command)
        {
            if (context.IsArithmeticOperationPressed)
            {
                HandleEqualButtonClick();
            }

            if (!double.TryParse(MainScreen.Text, out double arg)) 
            {
                return;
            }
                
            context.Arguments.FirstArg = arg;

            command.Button.Background = WindowUtil.ACTIVE_OPERATION_BRUSH;
            context.IsArithmeticOperationPressed = true;
            context.ShouldReplaceResult = false;
            context.IsDotPressed = false;

            if (MainScreen.Text.ElementAt(MainScreen.Text.Length - 1) == '.')
            {
                MainScreen.Text += WindowUtil.ZERO;
            }

            ExpressionScreen.Text = MainScreen.Text + command.GetSign();
            MainScreen.Text = WindowUtil.ZERO;
            nextCommand = command;
        }

        private void RevertLatestArithmeticOperationButtonColor() 
        {
            if (nextCommand != null)
            {
                nextCommand.Button.Background = WindowUtil.DEFAULT_OPERATION_BRUSH;
            }
        }

        private void HandleBackspaceButtonClick() 
        {
            if (MainScreen.Text == WindowUtil.ZERO)
            {
                ExpressionScreen.Text = WindowUtil.EMPTY;
                RevertLatestArithmeticOperationButtonColor();
                nextCommand = null;
            }
            else if (MainScreen.Text.Length == 1 || nextCommand.Exception != null)
            {
                MainScreen.Text = WindowUtil.ZERO;
            }
            else
            {
                if (MainScreen.Text.ElementAt(MainScreen.Text.Length - 1) == '.')
                {
                    context.IsDotPressed = false;
                }
                MainScreen.Text = MainScreen.Text.Substring(0, MainScreen.Text.Length - 1);
            }
        }

        private void HandleDotButtonClick() 
        {
            if (!context.IsDotPressed && !context.ShouldReplaceResult)
            {
                context.IsDotPressed = true;
                MainScreen.Text += WindowUtil.POINT;
            }
        }
    }
}
