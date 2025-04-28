using System;
using WSBit41JJY.Lib;

namespace _0407_계좌관리
{
    internal class DeleteAccount
    {
        public void Invoke()        // p134
        {
            Console.WriteLine("\n[계좌 삭제]\n");
            try
            {

                int number = WbLib.InputNumber("계좌번호 입력");

                AccountControl con = AccountControl.singleton;
                con.AccountDelete(number);
                Console.WriteLine("삭제 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
