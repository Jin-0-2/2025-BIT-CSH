using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace _0415_데이터베이스
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
        private SqlCommand    scmd = null;

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

        public bool Insert_Book(string name, int price, string description)
        {
            string sql = string.Format($"insert into s_product values('{name}', {price}, '{description}');");
            return ExSqlCommand(sql);
        }

        public bool Update_Book1(int pid, int price)        // 일반적으로 유니크 값으로 많이 사용
        {
            string sql = string.Format($"update s_product set price = {price} where pid = {pid};");
            return ExSqlCommand(sql);
        }

        public bool Update_Book2(string name, int price)
        {
            string sql = string.Format($"update s_product set price = {price} where pname = '{name}';");
            return ExSqlCommand(sql);
        }

        public bool Delete_Book1(int pid)
        {
            string sql = string.Format($"delete from s_product where pid = {pid};");
            return ExSqlCommand(sql);
        }

        public bool Delete_Book2(string name)
        {
            string sql = string.Format($"delete from s_product where pname = '{name}';");
            return ExSqlCommand(sql);
        }

        #endregion

        #region 하나의 값을 반환하는 Select
        public int Get_TotalPrice()
        {
            string sql = string.Format($"select SUM(price) from s_product;");
            object obj = ExSqScalar_Command(sql);
            if (obj == null)
                throw new Exception("오류 발생");
            return (int)obj;
        }

        public int Get_MaxPrice()
        {
            string sql = string.Format($"select MAX(price) from s_product;");
            object obj = ExSqScalar_Command(sql);
            if (obj == null)
                throw new Exception("오류 발생");
            return (int)obj;
        }
        #endregion

        #region 다중 로우 데이터를 반환하는 Select
        // select * from s_product;
        public List<Book> SelectAll()
        {
            List<Book> books = new List<Book>();
            string sql = string.Format($"select * from s_product;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r =  cmd.ExecuteReader();
                while (r.Read())
                {
                    int number = int.Parse(r["PID"].ToString());      // r[0];      문자열 화 후 파싱
                    string name = r["PNAME"].ToString();
                    int price = int.Parse(r[2].ToString());
                    string desc = r[3].ToString();

                    Book b = new Book()
                    {
                        Number      = number,
                        Name        = name,
                        Price       = price,
                        Description = desc
                    };
                    books.Add(b);
                }
                r.Close();
            }   // cmd.Dispose();
            
            return books;
        }

        // select PNAME, PRICE from s_Product where pid = 1020;
        public Book SelectBook(int pid)
        {
            SqlDataReader r = null;
            try
            {
                Book b = new Book();
                string sql = string.Format($"select PID, PNAME, PRICE, DESCRIPTION from s_product where pid = {pid};");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();
                    r.Read();

                    b.Number = int.Parse(r["PID"].ToString());
                    b.Name = r["PNAME"].ToString();
                    b.Price = int.Parse(r[2].ToString());
                    b.Description = r[3].ToString();
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

        public Book SelectBook_Name(string pname)
        {
            SqlDataReader r = null;
            try
            {
                Book b = new Book();
                string sql = string.Format($"select * from s_product where pname = '{pname}';");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();
                    r.Read();

                    b.Number = int.Parse(r["PID"].ToString());
                    b.Name = r["PNAME"].ToString();
                    b.Price = int.Parse(r[2].ToString());
                    b.Description = r[3].ToString();
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
        private object ExSqScalar_Command (string sql)
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
