using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0421_HarryPotter
{
    internal class App
    {
        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion

        MemberControl con = MemberControl.singleton;

        public void Init()
        {
            con.Init();

            WbLib.Logo();
        }

        public void Run()
        {

            while (true)
            {
                Console.Clear();
                con.MemberPrintAll();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: con.New_MemberInsert(); break;
                    case ConsoleKey.F2: con.MemberSelect(); break;        // 아이디 아이디 점수
                    case ConsoleKey.F3: con.HouseSelect(); break;
                    case ConsoleKey.F4: con.GenderSelect(); break;
                    case ConsoleKey.F5: con.SpeciesSelect(); break;
                    case ConsoleKey.F6: con.AliverSelect(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
        }
        public void Exit()
        {
            con.Exit();
            WbLib.Ending();
        }
    }
}
