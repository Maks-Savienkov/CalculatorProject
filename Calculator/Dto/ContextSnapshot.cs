namespace Calculator.Dto
{
    public class ContextSnapshot
    {
        private ContextSnapshot(bool isDotPressed, bool shouldReplaceResult, bool isArithmeticOperationPressed, Arguments arguments)
        {
            this.isDotPressed = isDotPressed;
            this.shouldReplaceResult = shouldReplaceResult;
            this.isArithmeticOperationPressed = isArithmeticOperationPressed;
            this.arguments = arguments;
        }

        private bool isDotPressed;
        private bool shouldReplaceResult;
        private bool isArithmeticOperationPressed;

        private Arguments arguments;

        public bool IsDotPressed { get => isDotPressed; set => isDotPressed = value; }
        public bool ShouldReplaceResult { get => shouldReplaceResult; set => shouldReplaceResult = value; }
        public bool IsArithmeticOperationPressed { get => isArithmeticOperationPressed; set => isArithmeticOperationPressed = value; }

        public Arguments Arguments { get => arguments; set => arguments = value; }

        public static ContextSnapshotBuilder Builder()
        {
            return new ContextSnapshotBuilder();
        }

        public class ContextSnapshotBuilder
        {
            private bool isDotPressed;
            private bool shouldReplaceResult;
            private bool isArithmeticOperationPressed;

            private Arguments arguments;

            public ContextSnapshotBuilder SetIsDotPressed(bool isDotPressed)
            {
                this.isDotPressed = isDotPressed;
                return this;
            }

            public ContextSnapshotBuilder SetIsConstantPressed(bool isConstantPressed)
            {
                this.shouldReplaceResult = isConstantPressed;
                return this;
            }

            public ContextSnapshotBuilder SetIsArithmeticOperationPressed(bool isArithmeticOperationPressed)
            {
                this.isArithmeticOperationPressed = isArithmeticOperationPressed;
                return this;
            }

            public ContextSnapshotBuilder SetArguments(Arguments arguments)
            {
                this.arguments = arguments;
                return this;
            }

            public ContextSnapshot Build()
            {
                return new ContextSnapshot(isDotPressed, shouldReplaceResult, isArithmeticOperationPressed, arguments);
            }
        }
    }
}
