using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Calculator.Util
{
    public static class WindowUtil
    {
        public static Brush ACTIVE_OPERATION_BRUSH = Brushes.Orange;
        public static Brush DEFAULT_OPERATION_BRUSH = Brushes.LightGray;

        public static string EMPTY = "";
        public static string ZERO = "0";
        public static string POINT = ".";

        public static ISet<Key> NUMBER_KEYS = new HashSet<Key>() 
        {
            Key.NumPad0,
            Key.NumPad1,
            Key.NumPad2,
            Key.NumPad3,
            Key.NumPad4,
            Key.NumPad5,
            Key.NumPad6,
            Key.NumPad7,
            Key.NumPad8,
            Key.NumPad9,

            Key.D0,
            Key.D1,
            Key.D2,
            Key.D3,
            Key.D4,
            Key.D5,
            Key.D6,
            Key.D7,
            Key.D8,
            Key.D9
        };
    }
}
