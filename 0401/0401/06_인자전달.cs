using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal class _06_인자전달
    {
        static void foo(int n1, ref int n2, out int n3)
        {
            n1 = 11;
            n2 = 22;        // 선택
            n3 = 33;        // 강제 - 안 하면 오류
        }
        private static void foo1()
        {
            int num1 = 1, num2 = 2, num3 = 3;

            // 값 전달, ref: 값을 바꿔도되고 안바꿔도 됨, out: 강제로 값을 바꿔야함. 전달
            foo(num1, ref num2, out num3);
            Console.WriteLine(num1 + ", " + num2 + ", " + num3);
        }

        static void fun1()
        {
            try
            {
                Console.Write("정수입력: ");
                int num = int.Parse(Console.ReadLine());
                Console.WriteLine(num);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        static void fun2()
        {
            Console.Write("정수입력: ");
            int num;
            if (int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine(num);
            }
            else
            {
                Console.WriteLine("잘못된 입력");
            }
        }

        static void Main(string[] args)
        {
            fun2();
        }

        
    }
}
