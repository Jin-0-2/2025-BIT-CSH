using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal class _09_정적생성
    {
        private static int NUMBER;

        static _09_정적생성()
        {
            Console.WriteLine("정적 생성자");
            NUMBER = 10;
            // static 멤버 변수를 초기화 하기 위해 사용
        }
        public static void foo()
        {
            Console.WriteLine("정적 메서드");
            Console.WriteLine(NUMBER);
        }

        static void Main(string[] args)
        {
            _09_정적생성.foo();
        }
    }
}
