using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0401
{
    internal class Sample
    {
        public void Thfunc()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"출력 : {i}");
                Thread.Sleep(500); // 0.5초 대기
            }
        }
        public void Thfunc2(object obj)
        {
            int max = (int)obj;
            for (int i = 0; i < max; i++)
            {
                Console.WriteLine($"출력2 : {i}");
                Thread.Sleep(500); // 0.5초 대기
            }
        }

    }

    internal class Start
    {
        static void Main(string[] args)
        {
            Sample s1 = new Sample();
            //Thread tr = new Thread(s1.Thfunc()); // Thread 생성
            Thread tr = new Thread(new ParameterizedThreadStart(s1.Thfunc2));
            tr.IsBackground = true;         // 기본적으로 ForeGround  > 스레드가 끝나기 전까지 Main이 끝나지 않음   
            tr.Start(10);                   // 스레드 시작    
            tr.Join();                      // 스레드가 끝날 때까지 대기
        }
    }
}
