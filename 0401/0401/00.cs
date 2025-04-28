using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    class Man
    {
        internal virtual void Work()
        {
            Console.WriteLine("일한다.");
        }
    }

    class Student: Man
    {
        internal override void Work()
        {
            Console.WriteLine("공부한다.");
        }
    }
    class professor : Man
    {
        internal override void Work()
        {
            Console.WriteLine("가르친다.");
        }
    }

    class Program
    {
        static void Main()
        {
            Man man = new Student();
            man.Work();
            man = new professor();
            man.Work();
        }
    }
}
