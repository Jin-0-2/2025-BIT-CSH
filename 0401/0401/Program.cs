using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udon.Sample;

namespace _0401
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Member member = new Member("홍길동", "010-1111-1111");
            member.Print();

            Console.WriteLine(member.ToString());
            Console.WriteLine(member);

            Member member2 = new Member("홍길동", "010-1111-1111");

            if(member == member2)
            {
                Console.WriteLine("같다");
            }

            if(member.Equals(member2) == true)
            {
                Console.WriteLine("값이 같다.");
            }
        }

        private static void NewMethod()
        {
            Udon.Sample.MyIO.PrintSample();
            MyIO.InputSample2();

            System.Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
        }
    }
}
