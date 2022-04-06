using System.Windows.Controls;

namespace Calculator.Command
{
    public class PlusComand : AbstractCommand
    {
        private static string PLUS = " + ";

        public PlusComand(Button button) : base(button) { }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            return Snapshot.Arguments.Sum();
        }

        public override string GetSign()
        {
            return PLUS;
        }
    }
}
