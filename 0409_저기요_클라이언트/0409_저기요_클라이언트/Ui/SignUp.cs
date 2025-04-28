using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
{
    internal static class SignUp
    {
        public static void Invoke()        // p134
        {
            Console.WriteLine("\n[회원 가입]\n");
            try
            {
                string id = WbLib.InputString("ID 입력");
                string pw = WbLib.InputString("PW 입력");
                string name = WbLib.InputString("이름 입력");
                int type = WbLib.InputNumber("고객(1) / 사장님(2) 입력");

                if (type == 1)
                {
                    UserControl con = UserControl.singleton;
                    con.Signup(id, pw, name, type);
                }
                else if(type == 2)
                {
                    OwnerControl con = OwnerControl.singleton;
                    con.Signup(id, pw, name, type);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
