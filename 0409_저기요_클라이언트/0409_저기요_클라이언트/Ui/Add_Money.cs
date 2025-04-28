using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
{
    internal static class Add_Money
    {
        public static void Invoke()        // p134
        {
            Console.WriteLine("\n[잔액 추가]\n");
            try
            {
                int input_money = WbLib.InputNumber("금액 입력");

                UserControl con = UserControl.singleton;
                con.Add_money(input_money);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
