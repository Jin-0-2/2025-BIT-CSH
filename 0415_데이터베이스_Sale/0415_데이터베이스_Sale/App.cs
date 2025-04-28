using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Sale
{
    internal class App
    {
        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion

        SaleContorl s_con = SaleContorl.singleton;

        public void Init()
        {
            s_con.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            //PrintAllAccount p_account = new PrintAllAccount();
            while (true)
            {
                Console.Clear();
                s_con.BookPrintAll();
                s_con.CustomPrintAll();
                s_con.SalePrintAll();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: s_con.SaleInsert(); break;
                    case ConsoleKey.F2: s_con.SaleDelete(); break;
                    case ConsoleKey.F3: s_con.SaleSelect_Name(); break;
                    case ConsoleKey.F4: s_con.SaleUpdate(); break;
                    case ConsoleKey.F5: s_con.BestSeller(); break;
                    case ConsoleKey.F6: s_con.TotalPrice(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
        }
        public void Exit()
        {
            s_con.Exit();
            WbLib.Ending();
        }
    }
}
