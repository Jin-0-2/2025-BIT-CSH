// App.c
using System;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace WSBit41JJY_0404
{
    internal class App
    {
        private BookControl book_control = BookControl.singleton;

        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion


        public void Init()
        {
            book_control.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                book_control.BookPrintAll();
                ConsoleKey key = WbLib.MenuPrint();
                switch (key)
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: book_control.BookInsert();        break;
                    case ConsoleKey.F2: book_control.SelectBook();         break;
                    case ConsoleKey.F3: book_control.BookUpdate();        break;
                    case ConsoleKey.F4: book_control.BookPrintAll();         break;
                    case ConsoleKey.F5: book_control.BookSort();         break;
                    case ConsoleKey.F6: book_control.BookDelete();        break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n");   break;
                }
                WbLib.Pause();
            }
        }

        public void Exit()
        {
            book_control.Exit();
            WbLib.Ending();
        }
    }
}
