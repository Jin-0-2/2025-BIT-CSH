using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
{
    internal static class Add_Menu
    {
        public static void Invoke()        // p134
        {
            Console.WriteLine("\n[메뉴 추가]\n");
            try
            {
                string m_name = WbLib.InputString("메뉴 이름 입력");
                int price = WbLib.InputNumber("가격 입력");

                OwnerControl con = OwnerControl.singleton;
                con.Food_Insert(m_name, price);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}