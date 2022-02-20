using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator.Command
{
    public class LnCommand : AbstractCommand
    {

        private static string LN = "ln({0})";

        public LnCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            if (Snapshot.Arguments.FirstArg <= 0) 
            {
                throw new ArithmeticException("Error");
            }

            return Math.Log(Snapshot.Arguments.FirstArg);
        }

        public override string Undo()
        {
            return string.Format(LN, Snapshot.Arguments.FirstArg);
        }
    }
}
