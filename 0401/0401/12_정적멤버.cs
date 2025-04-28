using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal class Sample
    {
        // 클래스 멤버 필드
        // 모든 인스턴스들이 공유!
        static int s_number = 1000;

        private int number;     // 인스턴스 멤버 -> 인스턴스 생성시 생성

        public Sample()         // 객체 초기화(인스턴스 멤버의 초기화)
        {
            number = s_number;
            s_number = s_number + 10;
        }

        // 클래스 멤버 메서드는 클래스 멤버만 사용 가능.
        public static void s_Function()
        {
            // Funtion();
        }

        public void Function()
        {
            s_Function();
        }

        override public string ToString()
        {
            return number.ToString();
        }
    }
    internal class Start
    {
        static void Main(string[] args)
        {
            Sample s1 = new Sample();       // 객체(인스턴스) 생성
            Sample s2 = new Sample();       // 객체(인스턴스) 생성

            Console.WriteLine(s1);
            Console.WriteLine(s2);

            s1.Function();                  // 인스턴스 멤버 호출 -> 인스턴스에서 접근
            Sample.s_Function();            // 클래스 멤버 호출 -> 클래스에서 접근
        }
    }
}
