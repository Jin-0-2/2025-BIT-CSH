using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;
using MemeberAttendence.ui;

namespace MemeberAttendence
{
    internal class App
    {
        private MemberControl mem_control = MemberControl.singleton;

        #region 0. 싱글톤 패턴
        public static App singleton { get; } = null;

        static App() { singleton = new App(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private App() { }
        #endregion


        public void Init()
        {
            mem_control.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                mem_control.MenuMemberPrint();
                mem_control.MenuAttendencePrint();
                switch (WbLib.MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: new ui_InsertMember().Invoke();   break;
                    case ConsoleKey.F2: new ui_Attendence().Invoke();     break;
                    case ConsoleKey.F3: new ui_SelectMember().Invoke();   break;
                    case ConsoleKey.F4: new ui_DeleteMember().Invoke();   break;
                    case ConsoleKey.F5: new ui_PrintAllMember().Invoke(); break;
                    case ConsoleKey.F6: new ui_UpdateMember().Invoke();   break;
                    case ConsoleKey.F7: new ui_AttendenceRank().Invoke(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                WbLib.Pause();
            }
        }

        public void Exit()
        {
            mem_control.Exit();
            WbLib.Ending();
        }
    }
}
