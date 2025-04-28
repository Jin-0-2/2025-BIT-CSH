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
        private const string CONTRIACCOUNTS_FILENAME = "contriaccounts.txt";
        private const string FAITHCCOUNTS_FILENAME = "faithaccounts.txt";
        private const string ACCOUNTIOS_FILENAME = "accountios.txt";

        public static void Write_Account(List<Account> accounts)
        {
            StreamWriter a_writer = new StreamWriter(ACCOUNTS_FILENAME);
            StreamWriter c_writer = new StreamWriter(CONTRIACCOUNTS_FILENAME);
            StreamWriter f_writer = new StreamWriter(FAITHCCOUNTS_FILENAME);

            int a_count = 0, c_count = 0, f_count = 0;
            
            for(int i = 0;i < accounts.Count;i++)
            {
                if (accounts[i] is ContriAccount)
                    c_count++;
                else if(accounts[i] is FaithAccount)
                    f_count++;
                else
                    a_count++;
            }

            a_writer.WriteLine(a_count);
            c_writer.WriteLine(c_count);
            f_writer.WriteLine(f_count);

            for (int i = 0; i < accounts.Count; i++)
            {
                Account account = (Account)accounts[i];
                string temp = string.Empty;
                if (account is ContriAccount)
                {
                    ContriAccount c_acc = (ContriAccount)account;
                    temp = c_acc.Number + "@" + c_acc.Name + "@" + c_acc.Balance + "@" + c_acc.Ctime + "@" + c_acc.Contribution;
                    c_writer.WriteLine(temp);
                }
                else if (account is FaithAccount)
                {
                    temp = account.Number + "@" + account.Name + "@" + account.Balance + "@" + account.Ctime;
                    f_writer.WriteLine(temp);
                }
                else
                {
                    temp = account.Number + "@" + account.Name + "@" + account.Balance + "@" + account.Ctime;
                    a_writer.WriteLine(temp);
                }
            }

            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            a_writer.Dispose();
            c_writer.Dispose();
            f_writer.Dispose();
        }

        public static void Read_Accounts(List<Account> accounts)
        {
            StreamReader a_reader = new StreamReader(ACCOUNTS_FILENAME);
            StreamReader c_reader = new StreamReader(CONTRIACCOUNTS_FILENAME);
            StreamReader f_reader = new StreamReader(FAITHCCOUNTS_FILENAME);

            int a_size = int.Parse(a_reader.ReadLine());
            int c_size = int.Parse(c_reader.ReadLine());
            int f_size = int.Parse(f_reader.ReadLine());
            for (int i = 0; i < a_size; i++)
            {
                string temp = a_reader.ReadLine();        // 번호@이름@잔액@날짜
                string[] sp = temp.Split('@');
                int number = int.Parse(sp[0]);
                string name = sp[1];
                int balance = int.Parse(sp[2]);
                DateTime ctime = DateTime.Parse(sp[3]);

                accounts.Add(new Account(number, name, balance, ctime));
            }
            for (int i = 0; i < c_size; i++)
            {
                string temp = c_reader.ReadLine();        // 번호@이름@잔액@날짜@기부금  쌈@뽕하네
                string[] sp = temp.Split('@');
                int number = int.Parse(sp[0]);
                string name = sp[1];
                int balance = int.Parse(sp[2]);
                DateTime ctime = DateTime.Parse(sp[3]);
                int contribution = int.Parse((string)sp[4]);

                accounts.Add(new ContriAccount(number, name, balance, ctime, contribution));
            }
            for (int i = 0; i < f_size; i++)
            {
                string temp = f_reader.ReadLine();        // 번호@이름@잔액@날짜
                string[] sp = temp.Split('@');
                int number = int.Parse(sp[0]);
                string name = sp[1];
                int balance = int.Parse(sp[2]);
                DateTime ctime = DateTime.Parse(sp[3]);

                accounts.Add(new FaithAccount(number, name, balance, ctime));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            a_reader.Dispose();
            c_reader.Dispose();
            f_reader.Dispose();
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
