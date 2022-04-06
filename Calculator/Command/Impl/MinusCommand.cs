using System.Windows.Controls;

namespace Calculator.Command
{
    public class MinusCommand : AbstractCommand
    {

        private const string MINUS = " - ";

        public MinusCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            return Snapshot.Arguments.Diff();
        }

        public override string GetSign()
        {
            return MINUS;
        }
    }
}
