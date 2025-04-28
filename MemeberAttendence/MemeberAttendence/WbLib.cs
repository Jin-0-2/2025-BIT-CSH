// WbLib.cs
using System;


namespace JJY.Lib
{
    internal static class WbLib
    {
        #region 1. 로고, 메뉴, 종료 출력 메시지
        public static void Pause()
        {
            Console.WriteLine("\n\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
        }
        public static void Logo()
        {
            Console.Clear();
            Console.WriteLine("*************************************************************************");
            Console.WriteLine(" 2025학년도 1학기 비트 고급과정 41기 ");
            Console.WriteLine(" C#언어" );
            Console.WriteLine(" Go To The Fxcking Gym 관리 프로그램");
            Console.WriteLine(" 2025-04-07");
            Console.WriteLine(" JJY");
            Console.WriteLine("*************************************************************************");
            Pause();
        }
        

        public static ConsoleKey MenuPrint()
        {
            Console.WriteLine("*************************************************************************");
            Console.WriteLine("[ESC] 프로그램 종료\n");
            Console.WriteLine("[F1] 회원 가입\n");
            Console.WriteLine("[F2] 출석 체크(회원 이름)\n");
            Console.WriteLine("[F3] 회원 검색(회원 번호)\n");
            Console.WriteLine("[F4] 회원 삭제(회원 번호)\n");
            Console.WriteLine("[F5] 회원 전체 출력\n");
            Console.WriteLine("[F6] 회원 정보 수정(회원 이름)\n");
            Console.WriteLine("[F7] 꾸준 회원 찾기\n");
            Console.WriteLine("*************************************************************************");
            return Console.ReadKey().Key;
        }

        public static void Ending()
        {
            Console.Clear();
            Console.WriteLine("*************************************************************************");
            Console.WriteLine(" 프로그램을 종료합니다.\n");
            Console.WriteLine("*************************************************************************");
            Pause();
        }
        #endregion

        #region 2. 입력
        public static int InputNumber(string msg)
        {
            Console.Write(msg + " : ");

            return int.Parse(Console.ReadLine());
        }

        public static float InputFloat(string msg)
        {
            Console.Write(msg + " : ");

            return float.Parse(Console.ReadLine());
        }

        public static char InputChar(string msg)
        {
            Console.Write(msg + " : ");

            return char.Parse(Console.ReadLine());
        }

        public static string InputString(string msg)
        {
            Console.Write(msg + " : ");

            return Console.ReadLine();
        }
        #endregion

        #region 3. 날짜/시간 문자열로 변환
        public static string Get_Date(DateTime time)
        {
            return string.Format("{0}-{1}-{2}", time.Year, time.Month, time.Day);
        }

        public static string Get_Time(DateTime time)
        {
            return string.Format("{0}:{1}:{2}", time.Hour, time.Minute, time.Second);
        }
        #endregion
    }
}
