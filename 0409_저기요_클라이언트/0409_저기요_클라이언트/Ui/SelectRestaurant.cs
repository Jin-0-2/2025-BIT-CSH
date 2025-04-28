using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_저기요_클라이언트
{
    internal class SelectRestaurant
    {
        public static void Invoke()        // p134
        {
            Console.WriteLine("\n[가게 선택]\n");
            try
            {
                string r_name = WbLib.InputString("가게 이름 입력");

                UserControl con = UserControl.singleton;
                con.Select_R(r_name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
