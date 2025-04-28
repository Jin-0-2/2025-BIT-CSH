using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    // 정적 클래스 : 객체 생성 불가 new 사용 X
    // 관리할 멤버 필드가 없다.
    // 정적 멤버만 가질 수 있다.
    internal static class Smaple
    {
        public static int Add(int n1, int n2) { return n1 + n2; }

        public static int Sub(int n1, int n2) { return n1 - n2; }
    }

    internal class Start
    {
        static void Main(string[] args)
        {
           Smaple.Add(10, 20);
        }
    }
}
