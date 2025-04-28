// App.c
using System;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace _0402_계좌관리프로그램
{
    internal class App
    {
        private AccountControl acc_control = AccountControl.singleton;

        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion


        public void Init()
        {
            acc_control.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                acc_control.AccountPrintAll();
                acc_control.AccountIoPrintAll();
                ConsoleKey key = WbLib.MenuPrint();
                switch (key)
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: acc_control.AccountInsert();        break;
                    case ConsoleKey.D1: acc_control.ContriAccountInsert();  break;
                    case ConsoleKey.D2: acc_control.FaithAccountInsert();   break;
                    case ConsoleKey.F2: acc_control.SelectNumber();         break;
                    case ConsoleKey.F3: acc_control.SelectNameAll();        break;
                    case ConsoleKey.F4: acc_control.AccountInput();         break;
                    case ConsoleKey.F5: acc_control.AccountOutput();        break;
                    case ConsoleKey.F6: acc_control.AccountSort_Number();   break;
                    case ConsoleKey.F7: acc_control.AccountSort_Name();     break;
                    case ConsoleKey.F8: acc_control.AccountDelete();        break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n");   break;
                }
                WbLib.Pause();
            }
        }

        public void Exit()
        {
            acc_control.Exit();
            WbLib.Ending();
        }
    }
}
