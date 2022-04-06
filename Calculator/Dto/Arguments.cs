using System;

namespace Calculator.Dto
{
    public class Arguments
    {
        private double firstArg;
        private double? secondArg;

        public double FirstArg { get => firstArg; set => firstArg = value; }
        public double? SecondArg { get => secondArg; set => secondArg = value; }

        public Arguments()
        {
        }

        private Arguments(double firstArg, double? secondArg)
        {
            this.firstArg = firstArg;
            this.secondArg = secondArg;
        }

        public static ArgumentsBuilder Builder()
        {
            return new ArgumentsBuilder();
        }

        public double Sum()
        {
            return firstArg + secondArg.Value;
        }

        public double Diff()
        {
            return firstArg - secondArg.Value;
        }

        public double Product()
        {
            return firstArg * secondArg.Value;
        }

        public double Fraction()
        {
            if (secondArg == 0)
            {
                throw new DivideByZeroException("Division by zero");
            }
            return firstArg / secondArg.Value;
        }

        public double Pow()
        {
            if (FirstArg <= 0)
            {
                throw new ArithmeticException("Error");
            }

            return Math.Pow(firstArg, secondArg.Value);
        }

        public class ArgumentsBuilder
        {
            private double firstArg;
            private double? secondArg;

            public ArgumentsBuilder FirstArg(double firstArg)
            {
                this.firstArg = firstArg;
                return this;
            }

            public ArgumentsBuilder SecondArg(double? secondArg)
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
