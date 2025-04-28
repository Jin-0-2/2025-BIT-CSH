using System;
using System.Threading;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
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
            UserControl.singleton.Init();
            OwnerControl.singleton.Init();
            WbLib.Logo();
        }

        public void Run()
        {
            while (true)
            {
                switch (WbLib.Choose_S_L())
                {
                    case ConsoleKey.D1: SignUp.Invoke();                     break;
                    case ConsoleKey.D2: Login.Invoke_C(); Thread.Sleep(10); ClientMenu();      break;
                    case ConsoleKey.D3: Login.Invoke_R(); RestaurantMenu();  break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n");    break;
                }
            }
        }
        public void ClientMenu()
        {
            while (UserControl.singleton.cur_login)
            {
                Console.Clear();
                R_Print.Invoke();
                Thread.Sleep(1000);
                switch (WbLib.C_MenuPrint())
                {
                    case ConsoleKey.Escape:                                 return;
                    case ConsoleKey.F1: SelectRestaurant.Invoke();          break;
                    case ConsoleKey.F2: SelectMenu.Invoke();                break;
                    case ConsoleKey.F3: Add_Money.Invoke(); /*돈 추가 */     break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n");   break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
            Console.WriteLine("로그인을 먼저 하세요");
            return;
        }
        public void RestaurantMenu()
        {
            while (true)
            {
                
                Console.Clear();
                switch (WbLib.O_MenuPrint())
                {
                    case ConsoleKey.Escape: return; //함수 종료 키워드 
                    case ConsoleKey.F1: Add_Menu.Invoke(); break;
                    case ConsoleKey.F2:                     break;
                    default: Console.WriteLine("잘못 입력하셨습니다.\n"); break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
        }

        public void Exit()
        {
            WbLib.Ending();
        }
    }
}
