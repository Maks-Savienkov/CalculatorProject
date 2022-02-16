using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Expression
    {
        private double firstArg, secondArg;
        private char operation;

        char Operation
        {
            get => operation;
            set
            {
                //sqrt cod?; pow cod?; 
                if (value == '+' || value == '-' || value == '*' || value == '/' || value == 's' || value == 'p' || value == 'l')
                {
                    operation = value;
                }
            }
        }

        public Expression(double firstArg, double secondArg, char operation)
        {
            this.firstArg = firstArg;
            this.secondArg = secondArg;
            Operation = operation;
        }
    }
}
