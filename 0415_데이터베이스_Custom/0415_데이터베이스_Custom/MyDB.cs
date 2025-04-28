using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Custom
{
    internal class MyDB
    {
        #region 0. 싱글톤 패턴
        public static MyDB singleton { get; } = null;

        static MyDB() { singleton = new MyDB(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private MyDB()
        {

        }
        #endregion

        private const string server_name = "DESKTOP-PRQ0INH\\SQLEXPRESS";
        private const string db_name = "WB41";
        private const string sql_id = "aaa";
        private const string sql_pw = "1234";

        private SqlConnection scon = null;
        private SqlCommand scmd = null;

        #region 연결, 종료
        public bool Connect()
        {
            try
            {
                string con = string.Format($"Data Source={server_name};Initial Catalog={db_name};User ID={sql_id};Password={sql_pw}");
                scon = new SqlConnection(con);        // 커넥션 연결 문자열만 있으면 됨.
                scmd = new SqlCommand(con);
                scon.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                scon.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Insert, Update Delete Test

        public bool Insert_Custom(string name, string phone, string addr)
        {
            string sql = string.Format($"insert into s_custom values('{name}', '{phone}', '{addr}');");
            return ExSqlCommand(sql);
        }

        public bool Update_Custom1(int pid, string phone)        // 일반적으로 유니크 값으로 많이 사용
        {
            string sql = string.Format($"update s_custom set phone = {phone} where cid = {pid};");
            return ExSqlCommand(sql);
        }

        public bool Update_Custom2(string name, int price)
        {
            string sql = string.Format($"update s_custom set price = {price} where cname = '{name}';");
            return ExSqlCommand(sql);
        }

        public bool Delete_Custom1(int pid)
        {
            string sql = string.Format($"delete from s_custom where cid = {pid};");
            return ExSqlCommand(sql);
        }

        public bool Delete_Custom2(string name)
        {
            string sql = string.Format($"delete from s_custom where cname = '{name}';");
            return ExSqlCommand(sql);
        }

        #endregion

        #region 하나의 값을 반환하는 Select
        public int Get_TotalPrice()
        {
            string sql = string.Format($"select SUM(price) from s_custom;");
            object obj = ExSqScalar_Command(sql);
            if (obj == null)
                throw new Exception("오류 발생");
            return (int)obj;
        }

        public int Get_MaxPrice()
        {
            string sql = string.Format($"select MAX(price) from s_custom;");
            object obj = ExSqScalar_Command(sql);
            if (obj == null)
                throw new Exception("오류 발생");
            return (int)obj;
        }
        #endregion

        #region 다중 로우 데이터를 반환하는 Select
        // select * from s_custom;
        public List<Custom> SelectAll()
        {
            List<Custom> books = new List<Custom>();
            string sql = string.Format($"select * from s_custom;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int number = int.Parse(r["CID"].ToString());      // r[0];      문자열 화 후 파싱
                    string name = r["CNAME"].ToString();
                    string phone = r["PHONE"].ToString();
                    string addr = r["ADDR"].ToString();

                    Custom b = new Custom()
                    {
                        Number = number,
                        Name = name,
                        Phone = phone,
                        Addr = addr
                    };
                    books.Add(b);
                }
                r.Close();
            }   // cmd.Dispose();

            return books;
        }

        // select PNAME, PRICE from s_Product where pid = 1020;
        public Custom SelectCustom(int cid)
        {
            SqlDataReader r = null;
            try
            {
                Custom b = new Custom();
                string sql = string.Format($"select PID, PNAME, PRICE, DESCRIPTION from s_custom where pid = {cid};");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();
                    r.Read();

                    b.Number = int.Parse(r["PID"].ToString());
                    b.Name = r["PNAME"].ToString();
                    b.Phone = r[2].ToString();
                    b.Addr = r[3].ToString();
                }   // cmd.Dispose();

                return b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                r.Close();
            }
        }

        public Custom SelectCustom_Name(string cname)
        {
            SqlDataReader r = null;
            try
            {
                Custom b = new Custom();
                string sql = string.Format($"select * from s_custom where cname = '{cname}';");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();
                    r.Read();

                    b.Number = int.Parse(r["CID"].ToString());
                    b.Name = r["CNAME"].ToString();
                    b.Phone = r[2].ToString();
                    b.Addr = r[3].ToString();
                }   // cmd.Dispose();

                return b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                r.Close();
            }
        }

        #endregion

        #region DB 명령 함수(ExecuteNonQuery():I,U,D), (ExecuteScalar():S)
        private bool ExSqlCommand(string sql)
        {
            try
            {
                scmd.Connection = scon;
                scmd.CommandText = sql;

                if (scmd.ExecuteNonQuery() == 0)     // 트랜잭션을 실행하고 영향을 받는 행의 수를 반환.
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private object ExSqScalar_Command(string sql)
        {
            try
            {
                scmd.Connection = scon;
                scmd.CommandText = sql;

                return scmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
