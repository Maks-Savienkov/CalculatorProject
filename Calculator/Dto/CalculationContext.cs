﻿namespace Calculator.Dto
{
    public class CalculationContext
    {
        public bool IsDotPressed { get; set; }
        public bool ShouldReplaceResult { get; set; }
        public bool IsArithmeticOperationPressed { get; set; }

        public Arguments Arguments { get; set; }
    }
}
