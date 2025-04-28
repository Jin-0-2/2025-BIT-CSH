using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal delegate void Del(int result);             // 구현? 
    internal class Sample
    {
        // private Del del;                // delegate 선언
        // 프로퍼티
        public Del DelFunc { get; set; } = null;        // delegate 속성 선언, 초기화까지
        public int Add(int a, int b)
        {
            return a + b;
        }


        public void Sub(int a, int b)
        {
            int result = a - b;
            // callback  호출 
            if(DelFunc != null)      // delegate가 null이 아닐 때  있을 때?
            {
                DelFunc.Invoke(result);      //  명시적 delegate 호출
                DelFunc(result);             //  암시적 delegate 호출
            }
            else
            {
                Console.WriteLine("Delegate is null");
            }
        }   
    }

    internal class Start
    {
        // callback 함수
        static void Result(int result)
        {
            Console.WriteLine("CallBack : " + result) ;
        }
        static void Main(string[] args)
        {
            Sample s1 = new Sample();
            Console.WriteLine(s1.Add(1, 2));

            // s1.DelFunc = new Del(Result);   delegate에 callback 함수 등록
            s1.DelFunc = Result;            // delegate에 callback 함수 등록
            s1.Sub(1, 2); 
        }
    }
}
