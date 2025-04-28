using _0415_데이터베이스;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0417_XML
{
    internal class App
    {
        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion

        AccountControl a_con = AccountControl.singleton;

        public void Init()
        {
            a_con.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            //PrintAllAccount p_account = new PrintAllAccount();
            while (true)
            {
                Console.Clear();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: a_con.AccountInsert();              break;
                    case ConsoleKey.F2: a_con.AccountUpdate_InputMoney();   break;
                    case ConsoleKey.F3: a_con.AccountUpdate_OutputMoney();   break;
                    case ConsoleKey.F4: a_con.Account_Delete();   break;
                    case ConsoleKey.F5: a_con.AccountPrintAll(); break;
                    case ConsoleKey.F6: a_con.SaveXml(); break;
                    case ConsoleKey.F7: a_con.ReadXml(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
        }
        public void Exit()
        {
            a_con.Exit();
            WbLib.Ending();
        }
    }
}
