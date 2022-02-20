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
        public static Stack<AbstractCommand> commands = new Stack<AbstractCommand>();

        public static void Record(AbstractCommand command)
        {
            commands.Push(command);
        }

        public static AbstractCommand Revert()
        {
            return commands.Count() == 0 ? null : commands.Pop();
        }
    }
}
