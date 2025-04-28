using System;
using NetFlix.Lib;

namespace _0402_Netflix
{
    internal class App
    {
        private MovieControl mov_control = MovieControl.singleton;

        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion
        public void Init()
        {
            mov_control.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                mov_control.MoviePrintAll();
                mov_control.MovieIoPrintAll();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: mov_control.MovieInsert(); ; break;
                    case ConsoleKey.F2: mov_control.SelectMovie(); break;
                    case ConsoleKey.F3: mov_control.SelectMovieAll(); break;
                    case ConsoleKey.F4: mov_control.NumberInput(); break;
                    case ConsoleKey.F5: mov_control.MovieDelete(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                WbLib.Pause();
            }
        }

        public void Exit()
        {
            mov_control.Exit();
            WbLib.Ending();
        }
    }
}
