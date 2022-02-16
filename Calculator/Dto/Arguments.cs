using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Dto
{
    public class Arguments
    {
        private double firstArg, secondArg;

        private Arguments(double firstArg, double secondArg)
        {
            this.firstArg = firstArg;
            this.secondArg = secondArg;
        }

        public static ArgumentsBuilder Builder()
        {
            return new ArgumentsBuilder();
        }
        
        public class ArgumentsBuilder
        {
            private double firstArg, secondArg;

            public ArgumentsBuilder FirstArg(double firstArg)
            {
                this.firstArg = firstArg;
                return this;
            }

            public ArgumentsBuilder SecondArg(double secondArg)
            {
                this.secondArg = secondArg;
                return this;
            }

            public Arguments Build()
            {
                return new Arguments(firstArg, secondArg);
            }
        }
    }

    
}
