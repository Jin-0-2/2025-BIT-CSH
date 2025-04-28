using System;
using System.Collections.Generic;
using WSBit41JJY.Data;

namespace _0407_계좌관리
{
    internal class PrintAllAccount
    {
        public void Invoke()        // p134
        {
            Console.WriteLine("\n[계좌 전체 출력]\n");
            try
            {
                AccountControl con = AccountControl.singleton;
                Account_PrintAll(con.Accounts, con.Accounts_Count);

                Console.WriteLine("-----------------------------------");
                AccountIO_PrintAll(con.Accountios, con.Accontios_Count);
                Console.WriteLine("-----------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void Account_PrintAll(List<Account> accounts, int count)
        {
            Console.WriteLine("계좌 저장 개수 : " + accounts.Count);
            foreach (Account account in accounts)
            {
                account.Print();
            }
        }
        private void AccountIO_PrintAll(List<AccountIO> accountios, int count)
        {
            Console.WriteLine("내역 저장 개수 : " + accountios.Count);
            foreach (AccountIO accountio in accountios)
            {
                accountio.Print();
            }
        }
    }
}
