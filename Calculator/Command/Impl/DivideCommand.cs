using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator.Command
{
    public class DivideCommand : AbstractCommand
    {

        private const string DIVIDE_SIGN = " / ";

        public DivideCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            return Snapshot.Arguments.fraction();
        }

        public override string getSign()
        {
            return DIVIDE_SIGN;
        }
    }
}
