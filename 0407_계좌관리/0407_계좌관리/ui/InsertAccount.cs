using System;
using WSBit41JJY.Lib;

namespace _0407_계좌관리
{
    // 사용자 입력 -> 매니저 호출
    internal class InsertAccount
    {
        public void Invoke()        // p134
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
                Console.WriteLine("계좌 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
