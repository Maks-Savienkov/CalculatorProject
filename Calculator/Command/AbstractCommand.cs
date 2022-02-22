using System;
using Calculator.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator.Command
{
    public abstract class AbstractCommand
    {
        private const string SPACE = " ";

        private ContextSnapshot snapshot;
        private Button button;

        public ArithmeticException Exception { get; protected set; }

        protected AbstractCommand(Button button)
        {
            this.button = button;
        }

        public ContextSnapshot Snapshot { get => snapshot; set => snapshot = value; }
        public Button Button { get => button; set => button = value; }

        public abstract double Calculate();

        public virtual string Undo() 
        {
            if (GetSign() == null) 
            {
                return snapshot.Arguments.SecondArg.ToString();
            }

            return snapshot.Arguments.FirstArg + string.Join(GetSign(), SPACE, SPACE);
        }

        public virtual string GetSign()
        {
            return null;
        }
    }
}
