using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makedll
{
    public class cal
    {
        public int Num1 { get; private set; }
        public int Num2 { get; private set; }

        public int Result { get; private set; }

        public cal(int num1 = 0, int num2 = 0)
        {
            Num1 = num1;
            Num2 = num2;
            Result = 0;
        }

        public int Add()
        {
            Result = Num1 + Num2;
            return Result;
        }
        public int Sub()
        {
            Result = Num1 - Num2;
            return Result;
        }
    }
}
