// 계좌관리 프로그램
using System;


namespace _0407_계좌관리
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = App.singleton;
            app.Init();
            app.Run();
            app.Exit();
        }
    }
}
