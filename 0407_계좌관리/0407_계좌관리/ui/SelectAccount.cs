using System;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace _0407_계좌관리
{
    internal class SelectAccount
    {
        public void Invoke()        // p134
        {
            Console.WriteLine("\n[계좌 검색]\n");
            try
            {

                int number = WbLib.InputNumber("계좌번호 입력");

                AccountControl con = AccountControl.singleton;
                Account account    = con.NumberToAccount(number);
                //계좌 정보 출력
                account.Println();

                //거래 내역 출력
                Console.WriteLine("--------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
