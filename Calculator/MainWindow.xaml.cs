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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CalculationContext context;
        private AbstractCommand nextCommand;

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
            context = new CalculationContext();
            context.Arguments = new Arguments();
            // HoldMenu.Width = new GridLength(0, GridUnitType.Star);
        }

        private void Num_1_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("1");
        }

        private void Num_2_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("2");
        }

        private void Num_3_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("3");
        }

        private void Num_4_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("4");
        }

        private void Num_5_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("5");
        }

        private void Num_6_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("6");
        }

        private void Num_7_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("7");
        }

        private void Num_8_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("8");
        }

        private void Num_9_Button_Click(object sender, RoutedEventArgs e)
        {
            handleNumberButtonClick("9");
        }

        private void Num_0_Button_Click(object sender, RoutedEventArgs e)
        {
            handleZeroButtonClick(WindowUtil.ZERO);
        }

        private void Num_00_Button_Click(object sender, RoutedEventArgs e)
        {
            handleZeroButtonClick("00");
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            MainScreen.Text = WindowUtil.ZERO;
            ExpressionScreen.Text = WindowUtil.EMPTY;

            revertLatestArithmeticOperationButtonColor();
            nextCommand = null;

            context.IsDotPressed = false;
            context.IsArithmeticOperationPressed = false;
            context.ShouldReplaceResult = false;
        }

        private void BackSpace_Button_Click(object sender, RoutedEventArgs e)
        {
            handleBackspaceButtonClick();
        }

        private void Equal_Button_Click(object sender, RoutedEventArgs e)
        {
            handleEqualButtonClick();
        }

        private void CE_Button_Click(object sender, RoutedEventArgs e)
        {
            revertLatestArithmeticOperationButtonColor();
            nextCommand = CommandRecorder.Revert();
            if (nextCommand != null)
            {
                nextCommand.Button.Background = WindowUtil.ACTIVE_OPERATION_BRUSH;
                ExpressionScreen.Text = nextCommand.Undo();
                double? lastArgument = nextCommand.Snapshot.Arguments.SecondArg == null ? 
                    nextCommand.Snapshot.Arguments.FirstArg : nextCommand.Snapshot.Arguments.SecondArg;
                MainScreen.Text = lastArgument.ToString();
                context = ContextConverter.toContext(nextCommand.Snapshot);
            }
        }

        private void Dot_Button_Click(object sender, RoutedEventArgs e)
        {
            handleDotButtonClick();
        }

        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            handleTwoArgArithmeticOperation(new PlusComand(PlusButton));
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            handleTwoArgArithmeticOperation(new MinusCommand(MinusButton));
        }

        private void Multiply_Button_Click(object sender, RoutedEventArgs e)
        {
            handleTwoArgArithmeticOperation(new MultiplyCommand(MultiplyButton));
        }

        private void Divide_Button_Click(object sender, RoutedEventArgs e)
        {
            handleTwoArgArithmeticOperation(new DivideCommand(DivideButton));
        }

        private void Pi_Button_Click(object sender, RoutedEventArgs e)
        {
            handleConstantButtonClick(Math.PI);
        }

        private void E_Button_Click(object sender, RoutedEventArgs e)
        {
            handleConstantButtonClick(Math.E);
        }

        private void Radical_Button_Click(object sender, RoutedEventArgs e)
        {
            handleSingleArgArithmeticOperation(new RadicalCommand(RadicalButton));
        }

        private void Pow_Button_Click(object sender, RoutedEventArgs e)
        {
            handleTwoArgArithmeticOperation(new PowCommand(PowButton));
        }

        private void Ln_Button_Click(object sender, RoutedEventArgs e)
        {
            handleSingleArgArithmeticOperation(new LnCommand(LnButton));
        }

        private void handleNumberButtonClick(string number)
        {
            if (context.ShouldReplaceResult || MainScreen.Text == WindowUtil.ZERO)
            {
                context.ShouldReplaceResult = false;
                MainScreen.Text = number;
            }
            else
            {
                MainScreen.Text += number;
            }
        }

        private void handleZeroButtonClick(string text)
        {
            if (context.ShouldReplaceResult)
            {
                MainScreen.Text = text;
            }
            else if (MainScreen.Text != WindowUtil.ZERO)
            {
                MainScreen.Text += text;
            }
        }

        private void handleConstantButtonClick(double value)
        {
            context.ShouldReplaceResult = true;
            MainScreen.Text = value.ToString();
        }

        private void handleSingleArgArithmeticOperation(AbstractCommand command)
        {
            command.Button.Background = WindowUtil.ACTIVE_OPERATION_BRUSH;
            context.ShouldReplaceResult = true;
            context.Arguments.FirstArg = double.Parse(MainScreen.Text);
            command.Snapshot = ContextConverter.toSnapshot(context);
            executeCommand(command);
        }

        private void handleEqualButtonClick()
        {
            if (nextCommand == null)
            {
                return;
            }

            revertLatestArithmeticOperationButtonColor();

            context.IsArithmeticOperationPressed = false;
            context.ShouldReplaceResult = true;

            context.Arguments.SecondArg = double.Parse(MainScreen.Text);
            nextCommand.Snapshot = ContextConverter.toSnapshot(context);

            ExpressionScreen.Text = WindowUtil.EMPTY;
            executeCommand(nextCommand);

            nextCommand = null;
            context.Arguments.SecondArg = null;
        }

        private void executeCommand(AbstractCommand command)
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

        private void handleTwoArgArithmeticOperation(AbstractCommand command)
        {
            if (context.IsArithmeticOperationPressed)
            {
                handleEqualButtonClick();
            }

            command.Button.Background = WindowUtil.ACTIVE_OPERATION_BRUSH;
            context.IsArithmeticOperationPressed = true;
            context.ShouldReplaceResult = false;
            context.IsDotPressed = false;

            if (MainScreen.Text.ElementAt(MainScreen.Text.Length - 1) == '.')
            {
                MainScreen.Text += WindowUtil.ZERO;
            }

            context.Arguments.FirstArg = double.Parse(MainScreen.Text);
            ExpressionScreen.Text = MainScreen.Text + command.getSign();
            MainScreen.Text = WindowUtil.ZERO;
            nextCommand = command;
        }

        private void revertLatestArithmeticOperationButtonColor() 
        {
            if (nextCommand != null)
            {
                nextCommand.Button.Background = WindowUtil.DEFAULT_OPERATION_BRUSH;
            }
        }

        private void handleBackspaceButtonClick() 
        {
            if (MainScreen.Text == WindowUtil.ZERO)
            {
                ExpressionScreen.Text = WindowUtil.EMPTY;
                revertLatestArithmeticOperationButtonColor();
                nextCommand = null;
            }
            else if (MainScreen.Text.Length == 1 || !double.TryParse(MainScreen.Text, out double value))
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

        private void handleDotButtonClick() 
        {
            if (!context.IsDotPressed && !context.ShouldReplaceResult)
            {
                context.IsDotPressed = true;
                MainScreen.Text += WindowUtil.POINT;
            }
        }

        private void Screen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                handleBackspaceButtonClick();
            }
            else if (WindowUtil.NUMBER_KEYS.Contains(e.Key))
            {
                string key = e.Key.ToString();
                handleNumberButtonClick(key.Substring(key.Length - 1));
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                handleDotButtonClick();
            }

            MainScreen.SelectionStart = MainScreen.Text.Length;
            e.Handled = true;
        }
    }
}
