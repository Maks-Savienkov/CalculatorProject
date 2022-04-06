using System;
using System.Windows.Controls;

namespace Calculator.Command
{
    public class RadicalCommand : AbstractCommand
    {
        private static string RADICAL = "√";

        public RadicalCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            Snapshot.Arguments.SecondArg = 0.5;

            try
            {
                return (double)(Snapshot.Arguments.SecondArg = Snapshot.Arguments.Pow());
            }
            catch (ArithmeticException exc)
            {
                Snapshot.Arguments.SecondArg = null;
                throw Exception = exc;
            }
        }

        public override string Undo()
        {
            return RADICAL + Snapshot.Arguments.FirstArg;
        }
    }
}
