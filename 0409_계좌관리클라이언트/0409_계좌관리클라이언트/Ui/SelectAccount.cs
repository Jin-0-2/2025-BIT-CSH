// SelectAccount.cs
using System;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace _0409_계좌관리클라이언트
{
    internal static class SelectAccount
    {
        public static void Invoke()        // p134
        {
            Console.WriteLine("\n[계좌 검색]\n");
            try
            {

                int number = WbLib.InputNumber("계좌번호 입력");

                AccountControl con = AccountControl.singleton;
                con.AccountSelect(number);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
