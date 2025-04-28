using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
{
    internal static class Login
    {
        public static void Invoke_C()        // p134
        {
            Console.WriteLine("\n[로그인]\n");
            UserControl con = UserControl.singleton;
            try
            {
                string id = WbLib.InputString("ID 입력");
                string pw = WbLib.InputString("PW 입력");
                con.Login(id, pw);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Invoke_R()        // p134
        {
            Console.WriteLine("\n[로그인]\n");
            try
            {
                string id = WbLib.InputString("ID 입력");
                string pw = WbLib.InputString("PW 입력");

                OwnerControl con = OwnerControl.singleton;
                con.Login(id, pw);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
