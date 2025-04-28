using _0415_데이터베이스;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0417_XML
{
    internal class AccountControl
    {
        #region 0. 싱글톤 패턴
        public static AccountControl singleton { get; } = null;

        static AccountControl() { singleton = new AccountControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private AccountControl()
        {
            // logmanager = new LogManager(); 이 상황은 만들어지지 않은 AccountControl의 객체를 접근함.
        }
        #endregion

        private List<Account> accounts = new List<Account>();

        public List<Account> Accounts { get; }

        private MyDB db = new MyDB();

        

        #region 1. 기능 메서드
        public void AccountInsert()
        {
            try
            {
                int id      = WbLib.InputNumber("계좌번호 입력");
                string name = WbLib.InputString("이름 입력");
             
                Account acc = new Account(id, name, 0, DateTime.Now);
                accounts.Add(acc);
                db.AccountInsert(id, name, 0, DateTime.Now);

                db.MemoryToDataBase();
                Console.WriteLine("계좌 저장 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AccountUpdate_InputMoney()
        {
            try
            {
                int id = WbLib.InputNumber("계좌번호 입력");
                int input_money = WbLib.InputNumber("입금액 입력");

                Account acc = accounts.Find(x => x.Id == id);
                acc.Input_Money(input_money);

                db.AccountUpdate_InputMoney(id, input_money);

                db.MemoryToDataBase();
                Console.WriteLine("계좌 입금 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AccountUpdate_OutputMoney()
        {
            try
            {
                int id = WbLib.InputNumber("계좌번호 입력");
                int input_money = WbLib.InputNumber("출금액 입력");

                Account acc = accounts.Find(x => x.Id == id);
                acc.Output_Money(input_money);

                db.AccountUpdate_OutputMoney(id, input_money);

                db.MemoryToDataBase();
                Console.WriteLine("계좌 저장 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Account_Delete()
        {
            int number = WbLib.InputNumber("삭제 할 계좌번호 입력");

            int idx = NumberToIdx(number);

            accounts.RemoveAt(idx);
            db.AccountDelete(number);
            db.MemoryToDataBase();
        }
        public void AccountPrintAll()
        {
            try
            {
                
                foreach (Account acc in accounts)
                {
                    acc.Print();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaveXml()
        {
            WriteXml wxml =     new WriteXml();
            wxml.Writer_Xml(accounts);
            Console.WriteLine("저장 완료! 확인하러 ㄱㄱ");
        }
        public void ReadXml()
        {
            ReadXml rxml = new ReadXml();
            rxml.Read_Xml(accounts);
            Console.WriteLine("읽기 완료!");
        }
        #endregion
        private int NumberToIdx(int id)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                Account account = (Account)accounts[i];
                if (account.Id == id)
                    return i;
            }
            throw new Exception("없는 계좌번호입니다.");
        }

        #region 2. 시작/종료 메서드
        public void Init()
        {
            db.AccountSelectAll(accounts);
        }
        public void Exit()
        {

        }
        #endregion
    }
}
