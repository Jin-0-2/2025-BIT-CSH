// 21_이벤트
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    #region 게시자와 구독자가 모두 알고 있는 정보
    // 1. Eventing
    internal class CalArgs : EventArgs
    {
        public int Num1 {  get; private set; }
        public int Num2 {  get; private set; }
        public char Oper {  get; private set; }
        public int Result {  get; private set; }

        public CalArgs(int num1, int num2, char oper, int result)
        {
            Num1 = num1;
            Num2 = num2;
            Oper = oper;
            Result = result;
        }
    }

    // 2. Delegate
    internal delegate void CalDel(object obj, CalArgs e);       // 리턴  void , 개시자(이벤트 발생자), 이벤트 정보 
    #endregion

    // [이벤트 게시자]
    internal class Sample
    {
        // 3.1 이벤트 선언
        public event CalDel DelEvent = null;        // delegate 속성 선언, 초기화까지
        public void Add(int a, int b)
        {
            int result = a + b;
            //  이벤트 발생(게시)
            if (DelEvent != null)      // DelEvent가 null이 아닐 때
            {
                DelEvent(this, new CalArgs(a, b, '+', result));     // 이벤트 개시 > ResultHandler을 호출
            }
            else
            {
                Console.WriteLine("Delegate is null");
            }
        }

        public void Sub(int a, int b)
        {
            int result = a - b;
            //  이벤트 발생(게시)
            if (DelEvent != null)      // DelEvent가 null이 아닐 때
            {
                DelEvent(this, new CalArgs(a, b, '-', result));     // 이벤트 개시 > ResultHandler을 호출   
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
        static void ResultHandler(object obj, CalArgs e)
        {
            Console.WriteLine("{0} {1} {2} = {3}", e.Num1, e.Oper, e.Num2, e.Result);
        }
        static void Main(string[] args)
        {
            Sample s1 = new Sample();
            s1.DelEvent += ResultHandler;

            s1.Add(10, 20);
            s1.Sub(10, 20);
        }
    }
}
