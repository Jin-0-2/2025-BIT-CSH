using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
{
    internal static class SelectMenu_R
    {
        public static void Invoke(string msg)        // p134
        {
            string[] sp = msg.Split('#');
            string m_name = sp[0];
            int count = int.Parse(sp[1]);
            string c_id = sp[2];

            int y_n = WbLib.InputNumber("주문수락: (1: 예, 2: 아니오)");
            if (y_n == 1)
            {
                try
                {
                    OwnerControl con = OwnerControl.singleton;
                    int cook_t = WbLib.InputNumber("조리 시간 입력: ");
                    con.Select_M(true, c_id, cook_t);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (y_n == 2)
            {
                // 주문 취소
                OwnerControl con = OwnerControl.singleton;
                con.Select_M(false, null, 0);
                Console.WriteLine("주문이 취소되었습니다.");
            }
            else
            {
                // 잘못된 입력
                {
                    Console.WriteLine("주문이 취소되었습니다.");
                }
            }
        }
    }
}
