using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요
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
            ServerControl.singleton.Init();
            WbLib.Logo();
        }

        public void Exit()
        {
            ServerControl.singleton.Exit();
            WbLib.Ending();
        }
    }
}
