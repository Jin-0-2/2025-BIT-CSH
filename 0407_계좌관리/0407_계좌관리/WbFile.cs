// WbFile.cs
using System;
using System.Collections.Generic;
using System.IO;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace WSBit41JJY.File
{
    internal static class WbFile
    {
        private const string ACCOUNTS_FILENAME = "accounts.txt";
        private const string ACCOUNTIOS_FILENAME = "accountios.txt";
 
        public static void Write_Account(List<Account> accounts)
        {
            StreamWriter writer = new StreamWriter(ACCOUNTS_FILENAME);
            writer.WriteLine(accounts.Count);

            for (int i = 0; i < accounts.Count; i++)
            {
                Account account = (Account)accounts[i];
                string temp = string.Empty;
                temp = account.Number + "@" + account.Name + "@" + account.Balance + "@" + account.Ctime;
                writer.WriteLine(temp);
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            writer.Dispose();
        }

        public static void Read_Accounts(List<Account> accounts)
        {
            StreamReader reader = new StreamReader(ACCOUNTS_FILENAME);
            int size = int.Parse(reader.ReadLine());

            for (int i = 0; i < size; i++)
            {
                string temp = reader.ReadLine();        // 번호@이름@잔액@날짜
                string[] sp = temp.Split('@');
                int number = int.Parse(sp[0]);
                string name = sp[1];
                int balance = int.Parse(sp[2]);
                DateTime ctime = DateTime.Parse(sp[3]);

                accounts.Add(new Account(number, name, balance, ctime));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            reader.Dispose();
        }

        public static void Write_Accountio(List<AccountIO> accountios)
        {
            StreamWriter writer = new StreamWriter(ACCOUNTIOS_FILENAME);
            writer.WriteLine(accountios.Count);

            for (int i = 0; i < accountios.Count; i++)
            {
                AccountIO accountio = (AccountIO)accountios[i];
                string temp = string.Empty;
                temp = accountio.Number + "@" + accountio.Input + "@" + accountio.Output + "@" + accountio.Balance + "@" + accountio.Ctime;
                writer.WriteLine(temp);
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            writer.Dispose();
        }

        public static void Read_Accountios(List<AccountIO> accountios)
        {
            StreamReader reader = new StreamReader(ACCOUNTIOS_FILENAME);
            int size = int.Parse(reader.ReadLine());

            for (int i = 0; i < size; i++)
            {
                string temp = reader.ReadLine();        // 번호@인풋@아웃풋@잔액@날짜
                string[] sp = temp.Split('@');
                int number = int.Parse(sp[0]);
                int input = int.Parse(sp[1]);
                int output = int.Parse(sp[2]);
                int balance = int.Parse(sp[3]);
                DateTime ctime = DateTime.Parse(sp[4]);

                accountios.Add(new AccountIO(number, input, output, balance, ctime));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            reader.Dispose();
        }
    }
}
