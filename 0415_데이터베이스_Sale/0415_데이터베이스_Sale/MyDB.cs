using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Sale
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

        public bool Insert_Sale(string cname, string pname, int count)
        {
            string sql = string.Format($"insert into s_sale values((select cid from S_Custom where cname = '{cname}'),(select pid from S_Product where pname = '{pname}'), {count}, GETDATE());");
            return ExSqlCommand(sql);
        }

        public bool Update_Sale2(string cname, string pname, int count)
        {
            string sql = string.Format($"update s_sale set COUNT = {count}\r\nwhere CID = (select CID from S_Custom where cname = '{cname}')\r\nand\r\nPID = (select PID from S_Product where PNAME = '{pname}');");
            return ExSqlCommand(sql);
        }
        public bool Delete_Sale2(string cname, string pname)
        {
            string sql = string.Format($"delete from s_sale where \r\nCID = (select CID from S_Custom where cname = '{cname}')\r\nand\r\nPID = (select PID from S_Product where pname = '{pname}');");
            return ExSqlCommand(sql);
        }

        #endregion

        #region 하나의 값을 반환하는 Select
        public int Get_TotalPrice()
        {
            string sql = string.Format($"select SUM(price) from s_sale;");
            object obj = ExSqScalar_Command(sql);
            if (obj == null)
                throw new Exception("오류 발생");
            return (int)obj;
        }

        public int Get_MaxPrice()
        {
            string sql = string.Format($"select MAX(price) from s_sale;");
            object obj = ExSqScalar_Command(sql);
            if (obj == null)
                throw new Exception("오류 발생");
            return (int)obj;
        }
        #endregion

        #region 다중 로우 데이터를 반환하는 Select
        // select * from s_sale;
        public List<Sale> Select_Sale_All()
        {
            List<Sale> sales = new List<Sale>();
            string sql = string.Format($"select * from s_sale;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int sid = int.Parse(r["SID"].ToString());      // r[0];      문자열 화 후 파싱
                    int cid = int.Parse(r["CID"].ToString());
                    int pid = int.Parse(r["PID"].ToString());
                    int count = int.Parse(r["COUNT"].ToString());
                    DateTime dtime = DateTime.Parse(r["SaleDate"].ToString());

                    Sale b = new Sale()
                    {
                        Sid = sid,
                        Cid = cid,
                        Pid = pid,
                        Count = count,
                        Dtime = dtime
                    };
                    sales.Add(b);
                }
                r.Close();
            }   // cmd.Dispose();

            return sales;
        }
        public List<Book> Select_Book_All()
        {
            List<Book> books = new List<Book>();
            string sql = string.Format($"select * from s_product;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int number = int.Parse(r["PID"].ToString());      // r[0];      문자열 화 후 파싱
                    string name = r["PNAME"].ToString();
                    int price = int.Parse(r[2].ToString());
                    string desc = r[3].ToString();

                    Book b = new Book()
                    {
                        Number = number,
                        Name = name,
                        Price = price,
                        Description = desc
                    };
                    books.Add(b);
                }
                r.Close();
            }   // cmd.Dispose();

            return books;
        }

        public List<Custom> Selec_Custom_All()
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
        public Sale SelectSale(int pid)
        {
            SqlDataReader r = null;
            try
            {
                Sale b = new Sale();
                string sql = string.Format($"select PID, PNAME, PRICE, DESCRIPTION from s_sale where pid = {pid};");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();
                    r.Read();

                    b.Sid = int.Parse(r["SID"].ToString());
                    b.Cid = int.Parse(r["CID"].ToString());
                    b.Pid = int.Parse(r["PID"].ToString());
                    b.Count = int.Parse(r["COUNT"].ToString());
                    b.Dtime = DateTime.Parse(r["SaleDate"].ToString());
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

        public bool SelectSale_Name(string pname)
        {
            SqlDataReader r = null;
            try
            {
                string sql = string.Format($"select S_Product.PNAME, S_Sale.COUNT, S_Sale.SaleDate\r\nfrom S_Product, S_Sale\r\nwhere S_Product.PID = S_Sale.PID\r\nand CID = (select cid from S_Custom where cname = '{pname}');");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        string name = r["PNAME"].ToString();
                        int count = int.Parse(r["COUNT"].ToString());
                        DateTime dtime = DateTime.Parse(r["SaleDate"].ToString());

                        Console.WriteLine($"{name}을(를) {count}권 구매 {dtime}");
                    }
                }   // cmd.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                r.Close();
            }
        }
        public void bestseller()
        {
            string sql = string.Format($"select  top 1 pname, SUM(count) as 'COUNT of PID' from S_Sale, S_Product where s_sale.PID = S_Product.PID group by pname  order by SUM(count) DESC");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();

                string pname = r["pname"].ToString();
                int max = int.Parse(r["COUNT of PID"].ToString()) ;

                r.Close();

                Console.WriteLine($"베스트 셀러는 {pname}, {max}권 입니다");

                return;
            }
        }
        public void TotalPrice()
        {
            string sql = string.Format($"select S_Product.Price, pname, SUM(count) as 'MAX' from S_Sale, S_Product where s_sale.PID = S_Product.PID group by pname, S_Product.Price  order by MAX DESC\r\n");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                int total = 0;
                while (r.Read())
                {
                    int price = int.Parse(r["Price"].ToString());
                    string pname = r["pname"].ToString();
                    int max = int.Parse(r["MAX"].ToString());

                    total += price * max;
                }

                r.Close();

                Console.WriteLine($"전체 판매 금액 {total}원");
                return;
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
