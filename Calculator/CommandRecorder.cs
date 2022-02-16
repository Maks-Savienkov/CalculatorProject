using Calculator.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static class CommandRecorder
    {
        public static Stack<ICommand> data = new Stack<ICommand>();

        public static void Record(ICommand command)
        {
            data.Push(command);
        }

        public static ICommand Revert()
        {
            return data.Pop();
        }
    }
}
