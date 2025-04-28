using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace _0409_계좌관리클라이언트
{
    internal class InputOutputAccount
    {
        public static void Invoke()        // p134
        {
            Console.WriteLine("\n[계좌 이체]\n");
            try
            {
                int output_number = WbLib.InputNumber("보내는 계좌번호 입력");
                int input_number = WbLib.InputNumber("받는 계좌번호 입력");
                int transfer_ammount = WbLib.InputNumber("이체 금액 입력");

                AccountControl con = AccountControl.singleton;
                con.AccountInputOutput(input_number, output_number, transfer_ammount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
