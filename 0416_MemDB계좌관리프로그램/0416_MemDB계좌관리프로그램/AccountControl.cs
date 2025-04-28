// AccountControl.cs
using _0416_MemDB계좌관리프로그램;
using System;
using System.Collections.Generic;
using System.Data;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace _0402_계좌관리프로그램
{
    internal class AccountControl
    {
        #region 0. 싱글톤 패턴
        public static AccountControl singleton { get; } = null;

        static AccountControl() { singleton = new AccountControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private AccountControl()
        {

        }
        #endregion

        private MemoryDB db = MemoryDB.singleton;

        #region 2. 기능 메서드
        public void AccountInsert()
        {
            try
            {
                Console.WriteLine("\n[계좌 저장]\n");

                string name = WbLib.InputString("이름 입력");
                int balance = WbLib.InputNumber("입금액 입력");


                db.Account_Insert(name, balance);

                Console.WriteLine("[계좌 저장성공]");
                Console.WriteLine("거래 내열 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[계좌 개설] " + ex.Message);
            }
        }
        public void AccountPrintAll()
        {
            List<Account> accounts = db.Account_SelectAll();
            Console.WriteLine($"[저장 개수 : {accounts.Count}개]\n");

            foreach (Account acc in accounts)
            {
                acc.Print();
            }
        }
        public void AccountIoPrintAll()
        {
            List<AccountIO> accountios = db.Accountio_SelectAll();
            Console.WriteLine($"[저장 개수 : {accountios.Count}개]\n");

            foreach (AccountIO acc in accountios)
            {
                acc.Print();
            }
        }
        public void SelectNumber()
        {
            try
            {
                Console.WriteLine("\n[계좌 검색]\n");

                int number = WbLib.InputNumber("계좌번호 입력");

                Account account = db.Account_Select(number);
                //계좌 정보 출력
                account.Println();
                //거래 내역 출력

                Console.WriteLine("\n[거래 내역]");
                List<AccountIO> accountios = db.Accountio_SelectByAccountId(number);
                foreach (AccountIO accountIO in accountios)
                {
                    accountIO.Print();
                }
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

                db.Account_Update_InputMoney(number, money);

                //개설시 거래 내역 저장
                //AccountIO accountio = new AccountIO(number, money, 0, account.Balance);
                //accountios.Add(accountio);

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

                db.Account_Update_OutputMoney(number, money);

                //개설시 거래 내역 저장
                //AccountIO accountio = new AccountIO(number, 0, money, account.Balance);
                //accountios.Add(accountio);

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

                db.Account_Delete(number);

                //거래내역 전체 삭제
                //AccountIODeleteAll(number);
                //Console.WriteLine("삭제되었습니다.");
                //Console.WriteLine("거래내역 삭제 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[삭제 실패] " + ex.Message);
            }
        }

        public void AccountSendMoney()
        {
            try
            {
                Console.WriteLine("\n[계좌 이체]\n");

                int sendid = WbLib.InputNumber("보낼 계좌 입력");
                int recvid = WbLib.InputNumber("받을 계좌 입력");
                int money = WbLib.InputNumber("보낼 money 입@력");

                db.Account_sendMoney(sendid, recvid, money);

            }
            catch (Exception ex)
            {
                Console.WriteLine("[계좌 이체 실@패] " + ex.Message);
            }
        }

        #endregion

        #region 3. 내부에서 사용되는 메서드(공개 필요 X)

        #endregion
        #region 4. 시작/종료 메서드
        public void Init()
        {
            try
            {
                //db.Create_AccountTable();
                //db.Create_AccountIOTable();
                DataSet ds = MyXml.Account_Read_Xml();
                if (db.ds != null)
                {


                    db.DataSet();
                }

                db.account_table = ds.Tables["Account"];
                db.accountio_table = ds.Tables["AccountIO"];

                db.ds = ds;
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
            MyXml.Account_Write_Xml(db.ds);
        }
        #endregion
    }
}
