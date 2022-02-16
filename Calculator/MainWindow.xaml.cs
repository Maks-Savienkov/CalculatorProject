using Calculator.Command;
using Calculator.Dto;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool dot_flag = false;
        private bool is_number_presed = false;
        private double firstarg;
        private int secondArgumentStartIndex = 0;
        private CommandType nextCommand;

        public MainWindow()
        {
            InitializeComponent();
            Screen.Text = "0";
            HoldMenu.Width = new GridLength(0, GridUnitType.Star);
        }

        private void Num_1_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "1";
        }

        private void Num_2_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "2";
        }

        private void Num_3_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "3";
        }

        private void Num_4_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "4";
        }

        private void Num_5_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "5";
        }

        private void Num_6_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "6";
        }

        private void Num_7_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "7";
        }

        private void Num_8_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "8";
        }

        private void Num_9_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "9";
        }

        private void Num_0_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "0";
        }

        private void Num_00_Button_Click(object sender, RoutedEventArgs e)
        {
            is_number_presed = true;
            Screen.Text += "00";
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Screen.Text = "0";
        }

        private void BackSpace_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Screen.Text.Length == 1)
            {
                Screen.Text = "0";
            }
            else
            {
                Screen.Text = Screen.Text.Substring(0, Screen.Text.Length - 1);
            }
        }

        private void Equal_Button_Click(object sender, RoutedEventArgs e)
        {
            dot_flag = false;
            double secondarg = double.Parse(Screen.Text.Substring(secondArgumentStartIndex));
            switch (nextCommand)
            {
                case CommandType.Plus:
                    Screen.Text = new PlusComand(firstarg, secondarg).Calculate().ToString();
                    break;
                default:
                    break;
            }
        }

        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Screen.Text.ElementAt(Screen.Text.Length - 1) == '.')
            {
                Screen.Text += "0";
            }
            firstarg = double.Parse(Screen.Text);
            Screen.Text += " + ";
            secondArgumentStartIndex = Screen.Text.Length;
            nextCommand = CommandType.Plus;
        }

        private void CE_Button_Click(object sender, RoutedEventArgs e)
        {
            Screen.Text = "";
        }

        private void Dot_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!dot_flag && Screen.Text.Length != 0)
            {
                dot_flag = true;
                Screen.Text += ".";
            }
        }
    }
}
