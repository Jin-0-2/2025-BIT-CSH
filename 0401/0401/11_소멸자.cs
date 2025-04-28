using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal class Sample  : IDisposable
    {
        public Sample()
        {
            Console.WriteLine("[외부자원]DB에 연결");
        }

        ~Sample()
        {
            Dispose();
        }

        public void Dispose()
        {
            Console.WriteLine("[외부자원]Dispose - DB 연결 해제");
            GC.SuppressFinalize(this);      // 소멸자 호출을 하지 않겠다.
        }
    }
    internal class Start
    {
        static void fun()
        {
            Sample s1 = new Sample();
            // 함수가 끝나면 s1이 소멸되고, new Sample()은 소멸 되지 않음. 참조되는 것이 없기 때문에 GC가 처리함.
            s1.Dispose();       // Dispose()를 호출하면 소멸자가 호출되지 않음. 외부자원 연결 종료 처리.
        }
        static void Main(string[] args)
        {
            fun();
            // Console.ReadKey();
        }
    }
}
