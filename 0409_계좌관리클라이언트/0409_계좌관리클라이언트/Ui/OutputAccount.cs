// OutputAccount.cs
using System;
using WSBit41JJY.Lib;

namespace _0409_계좌관리클라이언트
{
    internal static class OutputAccount
    {
        public static void Invoke()        // p134
        {
            try
            {
                Console.WriteLine("\n[계좌 출금]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                int balance = WbLib.InputNumber("출금액 입력");

                // 매니저 호출
                AccountControl con = AccountControl.singleton;
                con.AccountOutput(number, balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
