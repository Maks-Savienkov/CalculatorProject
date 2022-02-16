using System;
using Calculator.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Command
{
    interface ICommand
    {
        double Calculate();
        double Undo();
    }
}
