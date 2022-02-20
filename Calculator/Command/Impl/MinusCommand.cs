﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator.Command
{
    public class MinusCommand : AbstractCommand
    {

        private const string MINUS = " - ";

        public MinusCommand(Button button) : base(button)
        {
        }

        public override double Calculate()
        {
            CommandRecorder.Record(this);
            return Snapshot.Arguments.diff();
        }

        public override string getSign()
        {
            return MINUS;
        }
    }
}