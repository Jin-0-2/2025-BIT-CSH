using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0401
{
    internal delegate int DelFun(string msg, int a, int b); // 비동기 호출할 대상의 함수와 매개변수가 일치하게. (= int Sum())
    internal class  Sample
    {
        // 동기 방식 >  한 함수가 끝나기 전까지 다른게 실행 안됨.
        public void Example1()
        {
            int result1 = Sum("첫 번째 호출", 0, 300);
            int result2 = Sum("두 번째 호출", 0, 200);
            int result3 = Sum("세 번째 호출", 0, 100);

            Console.WriteLine(result1 + ", " + result2 + ", " + result3);
        }

        // 비동기 방식 -> 대리자 사용
        public void Example2()
        {
            DelFun f1 = Sum;
            DelFun f2 = Sum;
            DelFun f3 = Sum;

            f1.BeginInvoke("f1", 0, 300, EndSum, "f1");
            f2.BeginInvoke("f2", 0, 200, EndSum, "f2");
            f3.BeginInvoke("f3", 0, 100, EndSum, "f3");
            Console.ReadLine(); // 대기
        }

        // 비동기 호출이 끝났을 때 호출되는 함수
        public void EndSum(IAsyncResult iar)
        {
            string str = (string)iar.AsyncState; // 비동기 호출 시 전달한 매개변수   > "f1" / "f2" / "f3"
            Console.Write(str);

            AsyncResult ar = iar as AsyncResult;        // 비동기 작업에 사용된 델리게이트 함수에 접근하기 위한 캐스팅
            DelFun dele = ar.AsyncDelegate as DelFun;   // 비동기 호출의 대리자를 as로 가져옴 
            int result = dele.EndInvoke(ar);            // 비동기 호출의 결과를 가져옴
            Console.WriteLine(result);
        }

        private int Sum(string msg, int n1, int n2)
        {
            int sum = 0;
            for (; n1 <= n2; n1++)
            {
                sum += n1;
                Console.WriteLine("{0} -> Sum{1}", msg, n1);
                Thread.Sleep(100);
            }

            return sum;                                 // > EndSum의 int result로 감.
        }
    }
    
    internal class Start
    {
        static void Main(string[] args)
        {
            Sample s1 = new Sample();
            s1.Example2();
        }
    }
}
