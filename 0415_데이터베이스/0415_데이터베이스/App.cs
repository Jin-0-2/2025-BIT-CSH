// App.c

using System.Threading;
using System;

namespace _0415_데이터베이스
{
    internal class App
    {
        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion

        BookControl b_con = BookControl.singleton;

        public void Init()
        {
            b_con.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            //PrintAllAccount p_account = new PrintAllAccount();
            while (true)
            {
                Console.Clear();
                b_con.BookPrintAll();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: b_con.BookInsert();      break;
                    case ConsoleKey.F2: b_con.BookSelect_Name(); break;
                    case ConsoleKey.F3: b_con.BookUpdate();      break;
                    case ConsoleKey.F4: b_con.BookDelete();      break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
        }
        public void Exit()
        {
            b_con.Exit();
            WbLib.Ending();
        }
    }
}
