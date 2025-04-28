using System;
using System.Collections.Generic;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace _0407_계좌관리
{
    internal class SelectNameAll
    {
        public void Invoke()        // p134
        {
            Console.WriteLine("\n[계좌 전체 출력(이름 기반)\n");
            try
            {
                string name = WbLib.InputString("이름 입력");
                
                AccountControl con = AccountControl.singleton;
                List<Account> accounts = con.SelectAllName(name);

                Print(accounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void Print(List<Account> accounts)
        {
            foreach (Account account in accounts)
            {
                account.Print();
            }
        }

    }
}
