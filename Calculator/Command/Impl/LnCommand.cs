using System;
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
            CommandRecorder.Record(this);
            if (Snapshot.Arguments.FirstArg <= 0)
            {
                throw Exception = new ArithmeticException("Error");
            }

            return (double)(Snapshot.Arguments.SecondArg = Math.Log(Snapshot.Arguments.FirstArg));
        }

        public override string Undo()
        {
            return string.Format(LN, Snapshot.Arguments.FirstArg);
        }
    }
}
