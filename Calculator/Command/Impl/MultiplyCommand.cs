using System.Windows.Controls;

namespace Calculator.Command
{
    public class MultiplyCommand : AbstractCommand
    {

        private const string MULTIPLY_SIGN = " × ";

        public MultiplyCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            return Snapshot.Arguments.Product();
        }

        public override string GetSign()
        {
            return MULTIPLY_SIGN;
        }
    }
}
