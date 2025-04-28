using System;
using WSBit41JJY.Lib;

namespace _0407_계좌관리
{
    internal class OutputAccount
    {
        public void Invoke()        // p134
        {
            try
            {
                Console.WriteLine("\n[계좌 출금]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                int balance = WbLib.InputNumber("출금액 입력");

                // 매니저 호출
                AccountControl con = AccountControl.singleton;
                con.AccountOutput(number, balance);
                Console.WriteLine("출금 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
