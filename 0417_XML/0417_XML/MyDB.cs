using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Security.Permissions;

namespace _0417_XML
{
    internal class MyDB
    {
        private const string server_name = "DESKTOP-PRQ0INH\\SQLEXPRESS";
        private const string db_name = "WB41";
        private const string sql_id = "aaa";
        private const string sql_pw = "1234";

        private SqlConnection scon = null;
        private SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        private DataSet db = new DataSet("account");
        public DataSet Db { get { return db; } }

        public MyDB()
        {
            string con = string.Format($"Data Source={server_name};Initial Catalog={db_name};User ID={sql_id};Password={sql_pw}");
            scon = new SqlConnection(con);        // 커넥션 연결 문자열만 있으면 됨.

            sqlDataAdapter.InsertCommand = Account_Insert();
            sqlDataAdapter.UpdateCommand = Account_Update();
            sqlDataAdapter.DeleteCommand = Account_Delete();

            // select 명령 등록
            sqlDataAdapter.SelectCommand = Account_SelectAll();
            sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            sqlDataAdapter.Fill(db, "account");

        }

        #region UPdate에 Select에  필요한 Command등록
        private SqlCommand Account_SelectAll()
        {
            string sql = "select * from account";
            SqlCommand comm = new SqlCommand(sql, scon);
            return comm;
        }
        private SqlCommand Account_Insert()
        {
            string sql = "insert into account values(@Id, @Name, @Balance, @Time)";

            SqlCommand comm = new SqlCommand(sql, scon);

            comm.Parameters.Add("@Id", SqlDbType.Int, 4, "id");
            comm.Parameters.Add("@Name", SqlDbType.VarChar, 50, "name");
            comm.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");
            comm.Parameters.Add("@Time", SqlDbType.DateTime, 8, "time");

            return comm;
        }
        private SqlCommand Account_Update()
        {
            string sql = "update account set balance = @Balance  where id = @CId;";

            SqlCommand comm = new SqlCommand(sql, scon);

            comm.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");
            comm.Parameters.Add("@CId", SqlDbType.Int, 4, "id");

            return comm;
        }
        private SqlCommand Account_Delete()
        {
            string sql = "delete from account where id= @CId;";

            SqlCommand comm = new SqlCommand(sql, scon);

            comm.Parameters.Add("@CId", SqlDbType.Int, 4, "id");

            return comm;
        }


        #endregion

        #region 기능2. Insert, Update, Delete : 논리적 DB에서 수행
        // 필요없음.
        public void AccountSelectAll(List<Account> accounts)
        {
            List<string> list = new List<string>();
            foreach (DataRow dr in db.Tables["account"].Rows)
            {
                {
                    string str = string.Empty;
                    int id = int.Parse(dr["id"].ToString());
                    string name = dr["name"].ToString();
                    int balance = int.Parse(dr["balance"].ToString());
                    DateTime time = DateTime.Parse(dr["time"].ToString());
                    accounts.Add(new Account(id, name, balance, time));
                }
            }
        }
        public void AccountInsert(int id, string name, int balance, DateTime time)
        {
            try
            {
                DataRow r = db.Tables["account"].NewRow();
                r["id"] = id;
                r["name"] = name;
                r["balance"] = balance;
                r["time"] = time;
                db.Tables["account"].Rows.Add(r);
                Console.WriteLine("DB 저장성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountUpdate_InputMoney(int cid, int input_money)
        {
            try
            {
                DataRow r = db.Tables["account"].Rows.Find(cid);
                int balance = int.Parse(r["balance"].ToString());
                balance += input_money;

                r["balance"] = balance;
                    
                Console.WriteLine("DB 입금 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountUpdate_OutputMoney(int cid, int Output_money)
        {
            try
            {
                DataRow r = db.Tables["account"].Rows.Find(cid);
                int balance = int.Parse(r["balance"].ToString());
                balance -= Output_money;

                r["balance"] = balance;

                Console.WriteLine("DB 출금 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountDelete(int cid)
        {
            try
            {
                DataRow r = db.Tables["account"].Rows.Find(cid);
                //db.Tables["s_custom"].Rows.Remove(r);            // 상태 >
                if (r != null)
                {
                    r.Delete(); // 상태를 Deleted로 변경
                    Console.WriteLine("삭제 성공!");
                }
                else
                {
                    Console.WriteLine("해당 ID를 찾을 수 없습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 기능3. Update -> 논리적 DB에서 물리적 DB로 복사
        public void MemoryToDataBase()
        {
            sqlDataAdapter.Update(db, "account");
        }
        #endregion

        #region 테이블 스키마 출력
        public void Print_TableSchema(DataTable dt)
        {
            Console.WriteLine("------------------------------------------------");
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
    }
}
