using System;
using WSBit41JJY.Lib;

namespace _0407_계좌관리
{
    internal class InputAccount
    {

        public void Invoke()        // p134
        {
            try
            {
                Console.WriteLine("\n[계좌 입금]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                int balance = WbLib.InputNumber("입금액 입력");

                // 매니저 호출
                AccountControl con = AccountControl.singleton;
                con.AccountInput(number, balance);
                Console.WriteLine("입금 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

    }
}
