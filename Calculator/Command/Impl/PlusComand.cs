using Calculator.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator.Command
{
    public class PlusComand : AbstractCommand
    {

        private static string PLUS = " + ";

        public PlusComand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            return Snapshot.Arguments.sum();
        }

        public override string getSign()
        {
            return PLUS;
        }
    }
}
