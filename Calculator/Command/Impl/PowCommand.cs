using System;
using System.Windows.Controls;

namespace Calculator.Command
{
    public class PowCommand : AbstractCommand
    {

        private static string POW_SYMBOL = " ^ ";

        public PowCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            try
            {
                return Snapshot.Arguments.Pow();
            }
            catch (ArithmeticException exc)
            {
                throw Exception = exc;
            }
        }

        public override string GetSign()
        {
            return POW_SYMBOL;
        }
    }
}
