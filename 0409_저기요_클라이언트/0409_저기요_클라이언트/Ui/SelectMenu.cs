using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
{
    internal static class SelectMenu
    {
        public static void Invoke()        // p134
        {
            UserControl con = UserControl.singleton;
            if (con.cur_r)
            {
                Console.WriteLine("\n[메뉴 선택]\n");
                try
                {
                    string m_name = WbLib.InputString("메뉴 입력");
                    int count = WbLib.InputNumber("수량 입력");

                    con.Select_M(m_name, count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("가게를 먼저 선택하세요");
                return;
            }
        }
    }
}
