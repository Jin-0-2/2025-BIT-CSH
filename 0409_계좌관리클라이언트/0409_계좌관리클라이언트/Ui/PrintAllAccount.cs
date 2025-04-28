// PrintAllAccount.cs
using System;
using System.Collections.Generic;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace _0409_계좌관리클라이언트
{
    internal static class PrintAllAccount
    {
        public static void Invoke()        // p134
        {
            Console.WriteLine("\n[계좌 전체 출력]\n");
            try
            { 
                AccountControl con = AccountControl.singleton;
                con.AccountPrintAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private static void Account_PrintAll(List<Account> accounts, int count)
        {
            Console.WriteLine("계좌 저장 개수 : " + accounts.Count);
            foreach (Account account in accounts)
            {
                account.Print();
            }
        }
        private static void AccountIO_PrintAll(List<AccountIO> accountios, int count)
        {
            Console.WriteLine("내역 저장 개수 : " + accountios.Count);
            foreach (AccountIO accountio in accountios)
            {
                accountio.Print();
            }
        }
    }
}
