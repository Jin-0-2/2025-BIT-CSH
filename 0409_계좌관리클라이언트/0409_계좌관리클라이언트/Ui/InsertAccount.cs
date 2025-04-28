// InsertAccount.cs
using System;
using WSBit41JJY.Lib;

namespace _0409_계좌관리클라이언트
{
    // 사용자 입력 -> 매니저 호출
    internal static class InsertAccount
    {
        public static void Invoke()        // p134
        {
            try
            {
                Console.WriteLine("\n[계좌 저장]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                string name = WbLib.InputString("이름 입력");
                int balance = WbLib.InputNumber("입금액 입력");

                // 매니저 호출
                AccountControl con = AccountControl.singleton;
                con.AccountInsert(number, name, balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
