// App.c
using System;
using WSBit41JJY.Lib;

namespace _0407_계좌관리
{
    internal class App
    {
        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion


        public void Init()
        {
            AccountControl.singleton.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            PrintAllAccount p_account = new PrintAllAccount();
            while (true)
            {
                Console.Clear();
                p_account.Invoke();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: new InsertAccount().Invoke(); break;
                    case ConsoleKey.F2: new SelectAccount().Invoke(); break;
                    case ConsoleKey.F3: new SelectNameAll().Invoke(); break;
                    case ConsoleKey.F4: new InputAccount().Invoke();  break;
                    case ConsoleKey.F5: new OutputAccount().Invoke(); break;
                    case ConsoleKey.F6: new DeleteAccount().Invoke(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                WbLib.Pause();
            }
        }

        public void Exit()
        {
            AccountControl.singleton.Exit();
            WbLib.Ending();
        }
    }
}
