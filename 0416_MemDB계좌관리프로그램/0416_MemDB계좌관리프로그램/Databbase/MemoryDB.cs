using _0402_계좌관리프로그램;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Data;

namespace _0416_MemDB계좌관리프로그램
{
    internal class MemoryDB
    {
        public DataTable account_table { get; set; }
        public DataTable accountio_table { get; set; }

        public DataSet ds = null;

        #region 0. 싱글톤 패턴
        public static MemoryDB singleton { get; } = null;

        static MemoryDB() { singleton = new MemoryDB(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private MemoryDB() { }
        #endregion


        #region 테이블 생성 및 테이블 스키마 출력

        #region 테이블 생성
        public void Create_AccountTable()       // 리턴 방식으로 바꿔보기
        {
            if (account_table != null)
                account_table.Dispose();

            account_table = new DataTable("Account");

            DataColumn col_id = new DataColumn("id", typeof(int));
            col_id.AutoIncrement = true;        // 자동 증가
            col_id.AutoIncrementSeed = 1000;    // 시작 값
            col_id.AutoIncrementStep = 10;      // 증가 폭
            account_table.Columns.Add(col_id);

            DataColumn col_name = new DataColumn("name", typeof(string));
            col_name.AllowDBNull = false;
            account_table.Columns.Add(col_name);

            DataColumn col_balance = new DataColumn("balance", typeof(int));
            col_balance.AllowDBNull = false;
            col_balance.DefaultValue = 0;
            account_table.Columns.Add(col_balance);

            DataColumn col_date = new DataColumn("date", typeof(DateTime));
            col_date.AllowDBNull = false;
            col_date.DefaultValue = DateTime.Now;
            account_table.Columns.Add(col_date);

            DataColumn[] pkyes = new DataColumn[1];
            pkyes[0] = col_id;      // 기본키 지정
            account_table.PrimaryKey = pkyes;


        }    
        public void Create_AccountIOTable()     // 리턴 방식으로 바꿔보기
        {
            if (accountio_table != null)
                accountio_table.Dispose();

            accountio_table = new DataTable("AccountIO");

            DataColumn col_Aid = new DataColumn("Aid", typeof(int));
            col_Aid.AutoIncrement = true;        // 자동 증가
            col_Aid.AutoIncrementSeed = 10;    // 시작 값
            col_Aid.AutoIncrementStep = 10;      // 증가 폭
            accountio_table.Columns.Add(col_Aid);

            DataColumn col_id = new DataColumn("id", typeof(int));
            col_id.AllowDBNull = false;
            accountio_table.Columns.Add(col_id);

            DataColumn col_input = new DataColumn("input", typeof(int));
            col_input.AllowDBNull = false;
            accountio_table.Columns.Add(col_input);

            DataColumn col_output = new DataColumn("output", typeof(int));
            col_output.AllowDBNull = false;
            accountio_table.Columns.Add(col_output);

            DataColumn col_balance = new DataColumn("balance", typeof(int));
            col_balance.AllowDBNull = false;
            accountio_table.Columns.Add(col_balance);

            DataColumn col_date = new DataColumn("date", typeof(DateTime));
            col_date.AllowDBNull = false;
            col_date.DefaultValue = DateTime.Now;
            accountio_table.Columns.Add(col_date);

            //DataColumn[] pkyes = new DataColumn[1];
            //pkyes[0] = col_id;      // 기본키 지정
            //account_table.PrimaryKey = pkyes;
        }
        #endregion

        public void DataSet()
        {
            ds.Tables.Add(account_table);
            ds.Tables.Add(accountio_table);

            DataRelation dr = new DataRelation("계좌 내역", account_table.Columns["id"], accountio_table.Columns["id"]);
            ds.Relations.Add(dr);

            DataColumn p = ds.Relations[0].ParentColumns[0];
            DataColumn c = ds.Relations[0].ChildColumns[0];

            Console.WriteLine("부모:{0}-{1}", p.Table.TableName, p.ColumnName);
            Console.WriteLine("자식:{0}-{1}", c.Table.TableName, c.ColumnName);
        }
        public void Print_TableSchema(DataTable dt)
        {
            Console.WriteLine("테이블 정보 출력");
            Console.WriteLine($"[테이블 명] {dt.TableName}");
            Console.WriteLine($"[컬럼 개수] {dt.Columns.Count}");
            Console.WriteLine($"[로우 데이터 개수] {dt.Rows.Count}");
            Console.WriteLine($"[기본키(PK)] {dt.PrimaryKey[0]}");

            foreach (DataColumn col in dt.Columns)
            {
                Console.WriteLine($"{col.ColumnName}\t{col.DataType}\t" +
                    $"{col.AllowDBNull}\t{col.DefaultValue}\t{col.AutoIncrement}");
            }
        }
        #endregion

        #region Account_Table : Insert, Update, Delete
        #region 저장
        public void Account_Insert(string name, int balance)
        {
            try
            {
                DataRow r = account_table.NewRow();
                r["name"] = name;
                r["balance"] = balance;
                account_table.Rows.Add(r);

                int _id = int.Parse(r["id"].ToString());
                Accountio_Insert(_id, balance, 0, balance);
                Console.WriteLine("저장성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Accountio_Insert(int id, int input, int output, int balance)
        {
            try
            {
                DataRow r = accountio_table.NewRow();
                r["id"] = id;
                r["input"] = input;
                r["output"] = output;
                r["balance"] = balance;
                accountio_table.Rows.Add(r);
                Console.WriteLine("저장성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Account_Insert(string name, int balance, DateTime dt)
        {
            try
            {
                DataRow r = account_table.NewRow();
                r["name"] = name;
                r["balance"] = balance;
                r["date"] = dt;
                account_table.Rows.Add(r);
                Console.WriteLine("저장성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Account_Insert(string name)
        {
            try
            {
                DataRow r = account_table.NewRow();
                r["name"] = name;

                account_table.Rows.Add(r);
                Console.WriteLine("저장성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        public void Account_Delete(int id)
        {
            try
            {
                DataRow r = account_table.Rows.Find(id);        // Find(PK) > 기본키로만 검색.

                Accountio_Delete(id);                           // 자식 먼저 삭제.
                account_table.Rows.Remove(r);                   // 부모 삭제
                Console.WriteLine("삭제됨. ㅋ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Accountio_Delete(int id)
        {
            try
            {
                DataRow[] rows = accountio_table.Select($"id = {id}");
                foreach (DataRow row in rows)
                {
                    accountio_table.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Account_Update_InputMoney(int id, int money)
        {
            try
            {
                DataRow r = account_table.Rows.Find(id);                // Find(PK) > 기본키로만 검색.
                if (r == null)
                    throw new Exception("없는 계좌번호");
                int balance = int.Parse(r["balance"].ToString());       // 잔액
                balance = balance + money;
                r["balance"] = balance;

                Accountio_Insert(int.Parse(r["id"].ToString()), money, 0, int.Parse(r["balance"].ToString()));

                Console.WriteLine("입금!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("없는 계좌번호");
            }
        }
        public void Account_Update_OutputMoney(int id, int money)
        {
            try
            {
                DataRow r = account_table.Rows.Find(id);                // Find(PK) > 기본키로만 검색.
                if (r == null)
                    throw new Exception("없는 계좌번호");

                int balance = int.Parse(r["balance"].ToString());       // 잔액
                if (money > balance)
                    throw new Exception("잔액부족!!");

                balance = balance - money;
                r["balance"] = balance;

                Accountio_Insert(int.Parse(r["id"].ToString()), 0, money, int.Parse(r["balance"].ToString()));


                Console.WriteLine("출금!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("잔액부족!!");
            }
        }

        public void Account_sendMoney(int sendid, int recvid, int money)
        {
            ds.AcceptChanges();         // 세이브 포인트
            try
            {
                Account_Update_OutputMoney(sendid, money);
                Account_Update_InputMoney(recvid, money);
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ds.RejectChanges();     // 로드 
                
            }
        }
        #endregion

        #region Account Table Select 관련 기능
        public List<Account> Account_SelectAll()
        {
            List<Account> accounts = new List<Account>();
            foreach (DataRow r in account_table.Rows)
            {
                int id          = int.Parse(r["id"].ToString());
                string name     = r["name"].ToString();
                int balance     = int.Parse(r["balance"].ToString());
                DateTime time   = DateTime.Parse(r["date"].ToString());
                accounts.Add(new Account(id, name, balance, time));
            }

            return accounts;
        }


        public List<AccountIO> Accountio_SelectAll()
        {
            List<AccountIO> accountios = new List<AccountIO>();

            foreach (DataRow r in accountio_table.Rows)
            {
                int aid         = int.Parse(r["Aid"].ToString());
                int id          = int.Parse(r["id"].ToString());
                int input       = int.Parse(r["input"].ToString());
                int output      = int.Parse(r["output"].ToString());
                int balance     = int.Parse(r["balance"].ToString());
                DateTime time   = DateTime.Parse(r["date"].ToString());
                accountios.Add(new AccountIO(aid, id, input, output, balance, time));
            }

            return accountios;
        }

        public Account Account_Select(int id)
        {
            try
            {
                DataRow r = account_table.Rows.Find(id);        // Find(PK) > 기본키로만 검색.

                string name = r["name"].ToString();
                int balance = int.Parse(r["balance"].ToString());
                DateTime time = DateTime.Parse(r["date"].ToString());

                return new Account(id, name, balance, time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<AccountIO> Accountio_SelectByAccountId(int accountId)
        {
            List<AccountIO> accountios = new List<AccountIO>();

            try
            {
                // 조건에 맞는 행들을 필터링
                DataRow[] rows = accountio_table.Select($"id = {accountId}");

                foreach (DataRow r in rows)
                {
                    int aid = int.Parse(r["Aid"].ToString());
                    int id = int.Parse(r["id"].ToString());
                    int input = int.Parse(r["input"].ToString());
                    int output = int.Parse(r["output"].ToString());
                    int balance = int.Parse(r["balance"].ToString());
                    DateTime time = DateTime.Parse(r["date"].ToString());

                    accountios.Add(new AccountIO(aid, id, input, output, balance, time));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
            }

            return accountios;
        }

        #endregion


    }
}
