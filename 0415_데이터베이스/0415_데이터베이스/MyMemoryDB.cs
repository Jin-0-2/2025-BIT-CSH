// MyMemoryDB.cs
// 메모리 DB를 직접 생성하고 사용!
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스
{
    internal class MyMemoryDB
    {
        private DataTable account_table = null;
        public DataTable Account_Table { get { return account_table; } }

        #region 테이블 생성 및 테이블 스키마 출력
        public void Create_AccountTable()
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
            col_balance.AllowDBNull=false;
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

        public void Print_TableSchema(DataTable dt)
        {
            Console.WriteLine($"[테이블 명] {dt.TableName}");
            Console.WriteLine($"[컬럼 개수] {dt.Columns.Count}");
            Console.WriteLine($"[로우 데이터 개수] {dt.Rows.Count}");
            Console.WriteLine($"[기본키(PK)] {dt.PrimaryKey}");

            foreach (DataColumn col in dt.Columns)
            {
                Console.WriteLine($"{col.ColumnName}\t{col.DataType}\t" +
                    $"{col.AllowDBNull}\t{col.DefaultValue}\t{col.AutoIncrement}");
            }
        }
        #endregion
    }
}
