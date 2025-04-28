// App.c
using WSBit41JJY.Lib;

namespace _0409_계좌관리서버
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

        public void Exit()
        {
            AccountControl.singleton.Exit();
            WbLib.Ending();
        }
    }
}
