using Calculator.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Converter
{
    public static class ContextConverter
    {
        public static ContextSnapshot ToSnapshot(CalculationContext context) 
        {
            return ContextSnapshot
                .Builder()
                .SetIsConstantPressed(context.ShouldReplaceResult)
                .SetIsArithmeticOperationPressed(context.IsArithmeticOperationPressed)
                .SetIsDotPressed(context.IsDotPressed)
                .SetArguments(Arguments
                    .Builder()
                    .FirstArg(context.Arguments.FirstArg)
                    .SecondArg(context.Arguments.SecondArg)
                    .Build())
                .Build();
        }

        public static CalculationContext ToContext(ContextSnapshot snapshot)
        {
            CalculationContext context = new CalculationContext();
            context.ShouldReplaceResult = snapshot.ShouldReplaceResult;
            context.IsArithmeticOperationPressed = snapshot.IsArithmeticOperationPressed;   
            context.IsDotPressed = snapshot.IsDotPressed;   
            context.Arguments = snapshot.Arguments;

            return context;
        }
    }
}
