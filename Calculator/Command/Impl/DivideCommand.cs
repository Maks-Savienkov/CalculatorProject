using System;
using System.Windows.Controls;

namespace Calculator.Command
{
    public class DivideCommand : AbstractCommand
    {

        private const string DIVIDE_SIGN = " ÷ ";

        public DivideCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);

            try
            {
                return Snapshot.Arguments.Fraction();
            }
            catch (DivideByZeroException exc)
            {
                throw Exception = exc;
            }
        }

        public override string GetSign()
        {
            return DIVIDE_SIGN;
        }
    }
}
