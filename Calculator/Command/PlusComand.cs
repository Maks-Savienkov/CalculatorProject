using Calculator.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Command
{
    class PlusComand : ICommand
    {
        double a, b;

        public PlusComand(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public PlusComand() { }

        public double Calculate()
        {
            CommandRecorder.Record(this);
            return a + b;
        }

        public double Undo()
        {
            return a;
        }
    }
}
