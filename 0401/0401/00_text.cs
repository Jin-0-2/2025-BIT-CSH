using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    class MyInt
    {
        public int Value { get; set; }
        public MyInt(int value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    internal class _00_text
    {
        static void Main(string[] args)
        {
            char[] chArry = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
            string s2 = new string('a', 4);
            Console.WriteLine(s2);
            
        }
    }

   
}
