// App.c
using System;
using System.Threading;
using WSBit41JJY.Lib;

namespace _0409_계좌관리클라이언트
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
            //PrintAllAccount p_account = new PrintAllAccount();
            while (true)
            {
                Console.Clear();
                PrintAllAccount.Invoke();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: InsertAccount.Invoke();             break;
                    case ConsoleKey.F2: SelectAccount.Invoke();             break;
                    case ConsoleKey.F3: InputOutputAccount.Invoke();        break;
                    case ConsoleKey.F4: InputAccount.Invoke();              break;
                    case ConsoleKey.F5: OutputAccount.Invoke();             break;
                    case ConsoleKey.F6: DeleteAccount.Invoke();             break;
                    case ConsoleKey.F7: PrintAllAccount.Invoke();          break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n");   break;
                }
                Thread.Sleep(1000);
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
