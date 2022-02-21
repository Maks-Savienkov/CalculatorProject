using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            return (double) (Snapshot.Arguments.SecondArg = Snapshot.Arguments.Pow());
        }

        public override string Undo()
        {
            return RADICAL + Snapshot.Arguments.FirstArg;
        }
    }
}
