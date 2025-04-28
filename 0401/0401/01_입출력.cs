//MyIO
/*
인스턴스 멤버: 생성된 객체(인스턴스)를 이용해 사용하는 멤버
클래스 멤버  : 생성된 객체로 사용할 수 없고, 클래스 이름으로 사용하는 멤버
         static이 붙은 멤버
*/
using System;


namespace Udon.Sample
{
    /// <summary>
    /// 내가 만든 입출력 예제
    /// </summary>
    internal class MyIO
    {
        /// <summary>
        /// WriteLine : 모든 타입별 오버로딩
        ///           : 문자열 형태로 변환 출력
        /// </summary>
        public static void PrintSample()
        {
            Console.WriteLine("My10.PrintSample()");
            Console.WriteLine(10);
            Console.WriteLine(10 + "문자열연결" + 20);
            Console.WriteLine(10 + 20 + "문자열연결");
        }

        // WirteLine: 인덱스 활용
        public static void PrintSample1()
        {
            Console.WriteLine("{0}, {1} 인덱스활용", 10, 'A');
            Console.WriteLine("{0} +  {1} = {2}", 10, 20, 10 + 20);
            Console.WriteLine("{0}, {1} 인덱스활용", 10, 3.14);

        }

        // WriteLine: 개행 처리가 없음
        //          : 사용 방법은 Writeln과 동일
        public static void PrintSample2()
        {
            Console.Write("문자열 출력");
            Console.Write("개행 처리는 직접\n");
            Console.Write(10);
        }

        // Console.ReadLine(): 문자열 반환
        // 기본 데이터 타입 입력
        public static void InputSample1()
        {
            Console.Write("이름을 입력하세요: ");
            string name = Console.ReadLine();

            Console.Write("나이를 입력하세요: ");  
            string temp = Console.ReadLine();
            int age = int.Parse(temp);

            Console.WriteLine("몸무게: ");
            float weight = float.Parse(Console.ReadLine());

            Console.WriteLine("성별 (남/여) : ");  // 한글도 가능(유니코드)
            char gender = char.Parse(Console.ReadLine());

            Console.WriteLine("\n\n[입력결과]");
            Console.WriteLine("이름: " + name);
            Console.WriteLine("나이: " + age);
            Console.WriteLine("몸무게: {0} ", weight);
            Console.WriteLine("성별 : {0}", gender);
        }

        // Console.ReadKey()
        // 특수 키 입력시
        public static void InputSample2()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                    Console.WriteLine("위로 이동");
                else if (key.Key == ConsoleKey.DownArrow)
                    Console.WriteLine("아래로 이동");
                else if (key.Key == ConsoleKey.F1)
                    Console.WriteLine("F1");
                else if (key.Key == ConsoleKey.Escape)
                    Console.WriteLine("Escape");
                else if (key.Key == ConsoleKey.D1)
                    Console.WriteLine("D1");
                else if (key.Key == ConsoleKey.NumPad1)
                    Console.WriteLine("NumPad1");
                else if (key.Key == ConsoleKey.X)
                    break;
            }
        }
    }
}
