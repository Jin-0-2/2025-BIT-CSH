using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Custom
{
    internal class App
    {
        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion

        CustomControl c_con = CustomControl.singleton;

        public void Init()
        {
            c_con.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            //PrintAllAccount p_account = new PrintAllAccount();
            while (true)
            {
                Console.Clear();
                c_con.CustomPrintAll();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: c_con.CustomInsert(); break;
                    case ConsoleKey.F2: c_con.CustomSelect_Name(); break;
                    case ConsoleKey.F3: c_con.CustomUpdate(); break;
                    case ConsoleKey.F4: c_con.CustomDelete(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
        }
        public void Exit()
        {
            c_con.Exit();
            WbLib.Ending();
        }
    }
}
