// AccountControl.cs
using System;
using System.Collections.Generic;
using System.Globalization;
using WSBit41JJY.Data;
using WSBit41JJY.File;
using WSBit41JJY.Lib;

namespace _0402_계좌관리프로그램
{
    internal class AccountControl
    {
        private List<Account> accounts = new List<Account>();           // 계좌저장
        private List<AccountIO> accountios = new List<AccountIO>();     // 거래내역 저장

        #region 0. 싱글톤 패턴
        public static AccountControl singleton { get; } = null;

        static AccountControl() { singleton = new AccountControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private AccountControl()
        {

        }
        #endregion


        #region 1. 임시 데이터
        public void Temp()
        {
            accounts.Add(new Account(1000, "홍길동", 1000));
            accountios.Add(new AccountIO(1000, 1000, 0, 1000));

            accounts.Add(new Account(1010, "김길동", 2000));
            accountios.Add(new AccountIO(1010, 2000, 0, 2000));

            accounts.Add(new Account(1020, "고길동", 3000));
            accountios.Add(new AccountIO(1020, 3000, 0, 3000));
        }
        #endregion

        #region 2. 기능 메서드
        public void AccountInsert()
        {
            try
            {
                Console.WriteLine("\n[계좌 저장]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                string name = WbLib.InputString("이름 입력");
                int balance = WbLib.InputNumber("입금액 입력");

                //계좌 저장
                Account account = new Account(number, name, balance);
                accounts.Add(account);

                //개설시 거래 내역 저장
                AccountIO accountio = new AccountIO(number, balance, 0, balance);
                accountios.Add(accountio);

                Console.WriteLine("[계좌 저장성공]");
                Console.WriteLine("거래 내열 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[계좌 개설] " + ex.Message);
            }
        }
        public void ContriAccountInsert()
        {
            try
            {
                Console.WriteLine("\n[기부 계좌 저장]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                string name = WbLib.InputString("이름 입력");
                int balance = WbLib.InputNumber("입금액 입력");

                //계좌 저장
                Account account = new ContriAccount(number, name, balance);
                accounts.Add(account);

                //개설시 거래 내역 저장
                AccountIO accountio = new AccountIO(number, balance, 0, account.Balance);
                accountios.Add(accountio);

                Console.WriteLine("[기부 계좌 저장성공]");
                Console.WriteLine("거래 내역 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[기부 계좌 개설] " + ex.Message);
            }
        }
        public void FaithAccountInsert()
        {
            try
            {
                Console.WriteLine("\n[신용 계좌 저장]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                string name = WbLib.InputString("이름 입력");
                int balance = WbLib.InputNumber("입금액 입력");

                //계좌 저장
                Account account = new FaithAccount(number, name, balance);
                accounts.Add(account);

                //개설시 거래 내역 저장
                AccountIO accountio = new AccountIO(number, balance, 0, account.Balance);
                accountios.Add(accountio);

                Console.WriteLine("[신용 계좌 저장성공]");
                Console.WriteLine("거래 내역 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[신용 계좌 개설] " + ex.Message);
            }
        }
        public void AccountPrintAll()
        {
            Console.WriteLine("[저장 개수 : {0}개]\n", accounts.Count);
            for (int i = 0; i < accounts.Count; i++)
            {
                Account account = accounts[i];
                account.Print();
            }
        }
        public void AccountIoPrintAll()
        {
            Console.WriteLine("[저장 개수 : {0}개]\n", accountios.Count);
            Console.WriteLine("계좌번호    입금액          출금액    잔액      거래시간");
            for (int i = 0; i < accountios.Count; i++)
            {
                AccountIO accountio = (AccountIO)accountios[i];
                accountio.Print();
            }
        }
        public void SelectNumber()
        {
            try
            {
                Console.WriteLine("\n[계좌 검색]\n");

                int number = WbLib.InputNumber("계좌번호 입력");

                Account account = NumberToAccount(number);
                //계좌 정보 출력
                account.Println();
                //거래 내역 출력
                Console.WriteLine("--------------------------------------------");
                AccountIOPrint(number);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[계좌 검색 실패] " + ex.Message);
            }
        }
        public void AccountInput()
        {
            try
            {
                Console.WriteLine("\n[계좌 입금]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                int money = WbLib.InputNumber("입금액 입력");

                Account account = NumberToAccount(number);
                account.Input_Money(money);

                //개설시 거래 내역 저장
                AccountIO accountio = new AccountIO(number, money, 0, account.Balance);
                accountios.Add(accountio);

                Console.WriteLine("입금 성공");
                Console.WriteLine("거래 내역 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[입금 실패] " + ex.Message);
            }
        }
        public void AccountOutput()
        {
            try
            {
                Console.WriteLine("\n[계좌 출금]\n");

                int number = WbLib.InputNumber("계좌번호 입력");
                int money = WbLib.InputNumber("출금액 입력");

                Account account = NumberToAccount(number);
                account.Output_Money(money);

                //개설시 거래 내역 저장
                AccountIO accountio = new AccountIO(number, 0, money, account.Balance);
                accountios.Add(accountio);

                Console.WriteLine("출금 성공");
                Console.WriteLine("거래 내역 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[출금 실패] " + ex.Message);
            }
        }

        public void AccountDelete()
        {
            try
            {
                Console.WriteLine("\n[계좌 삭제]\n");

                int number = WbLib.InputNumber("계좌번호 입력");

                int idx = NumberToIdx(number);

                //계좌 삭제
                accounts.RemoveAt(idx);

                //거래내역 전체 삭제
                AccountIODeleteAll(number);
                Console.WriteLine("삭제되었습니다.");
                Console.WriteLine("거래내역 삭제 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[삭제 실패] " + ex.Message);
            }
        }

        public void SelectNameAll()
        {
            try
            {
                Console.WriteLine("\n[계좌 검색(이름)]\n");

                string name = WbLib.InputString("이름 입력");

                for (int i = 0; i < accounts.Count; i++)
                {
                    Account account = (Account)accounts[i];
                    if (account.Name == name)
                    {
                        account.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[검색 실패] " + ex.Message);
            }
        }
        public void AccountSort_Number()
        {
            accounts.Sort(new NumberComparer());
            Console.WriteLine("계좌번호 순으로 정렬 완료잇!");
        }

        public void AccountSort_Name()
        {
            accounts.Sort(new NameComparer());
            Console.WriteLine("이름 순으로 정렬 완료잇!");
        }

        #endregion

        #region 3. 내부에서 사용되는 메서드(공개 필요 X)
        private int NumberToIdx(int number)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                Account account = (Account)accounts[i];
                if (account.Number == number)
                    return i;
            }
            throw new Exception("없는 계좌번호입니다.");
        }

        private Account NumberToAccount(int number)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                Account account = (Account)accounts[i];
                if (account.Number == number)
                    return account;
            }
            throw new Exception("없는 계좌번호입니다.");
        }

        private void AccountIOPrint(int number)
        {
            for (int i = 0; i < accountios.Count; i++)
            {
                AccountIO accountio = (AccountIO)accountios[i];
                if (accountio.Number == number)
                {
                    accountio.Print();
                }
            }
        }

        private void AccountIODeleteAll(int number)     // 심각한 버그..!
        {
            // for (int i = 0; i < accountios.Count; i++)    최초 코드
            for (int i = accountios.Count -1; i >= 0; i--)
            {
                AccountIO accountio = (AccountIO)accountios[i];
                if (accountio.Number == number)
                {
                    accountios.RemoveAt(i);
                }
            }
        }
        #endregion
        #region 4. 시작/종료 메서드
        public void Init()
        {
            try
            {
                WbFile.Read_Accounts(accounts);
                WbFile.Read_Accountios(accountios);
                Console.WriteLine("파일 로드 성공....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("파일 로드 실패(최초실행).... ");
                Console.WriteLine(ex.Message);
            }
            WbLib.Pause();
        }
        public void Exit()
        {
            WbFile.Write_Account(accounts);
            WbFile.Write_Accountio(accountios);
        }
        #endregion
    }
}
