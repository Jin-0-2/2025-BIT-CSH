using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _0418_test
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

        // DB연결을 위한 요소
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

        // insert into 테이블 values(값, 값, 값);
        public bool Insert_Data(int stu_id, int sub_id, int jumsu)
        {
            string sql = string.Format($"insert into Jumsu values({stu_id}, {sub_id}, {jumsu})");
            return ExSqlCommand(sql);
        }

        // update 테이블 set 바꿀것 = 바꿀값  where 테이블의값 = 바꿀거
        public bool Update_Data2(int stu_id, int sub_id, int jumsu)
        {
            string sql = string.Format($"update Jumsu set jumsu_num = {jumsu} where stu_id = {stu_id} and sub_id = {sub_id};");
            return ExSqlCommand(sql);
        }

        // delete from 테이블 where 테이블의값 = 지울거
        public bool Delete_Data2(int stu_id, int sub_id)
        {
            string sql = string.Format($"delete from Jumsu where stu_id = {stu_id} and sub_id = {sub_id};");
            return ExSqlCommand(sql);
        }


        #endregion

        #region 하나의 값을 반환하는 Select
        // select SUM(원하는속성) from 테이블 where 조건 = 조건

        public bool SelectData_Name(int stu_id, int sub_id)
        {
            SqlDataReader r = null;
            try
            {
                string sql = string.Format($"select stu_id, sub_id, jumsu_num from Jumsu where stu_id = {stu_id} and sub_id = {sub_id};");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        int stdid = int.Parse(r["stu_id"].ToString());
                        int subid = int.Parse(r["sub_id"].ToString());
                        int jumsu = int.Parse(r["jumsu_num"].ToString());

                        Console.WriteLine($"학생: {stdid} 과목: {subid} 점수: {jumsu}");

                    }
                    
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                r.Close();
            }
        }

        // select MAX(원하는속성) from 테이블 > 1개만 알려줌

        #endregion

        #region 다중 로우 데이터를 반환하는 Select
        // select * from 테이블;
        public List<Jumsu> Select_Jumsu_All()
        {
            List<Jumsu> j = new List<Jumsu>();
            string sql = string.Format($"select * from Jumsu;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int Jumsu_Id = int.Parse(r["Jumsu_id"].ToString());        
                    int Stu_Id = int.Parse(r["stu_id"].ToString());              
                    int Sub_Id = int.Parse(r["sub_id"].ToString());             
                    int Jumsu_Num = int.Parse(r["jumsu_num"].ToString());               

                    Jumsu b = new Jumsu(Jumsu_Id, Stu_Id, Sub_Id, Jumsu_Num);
                    j.Add(b);
                }
                r.Close();
            }

            return j;
        }
        #region 다른 테이블 참조시 사용
        public List<Student> Select_Student_All()
        {
            List<Student> students = new List<Student>();
            string sql = string.Format($"select * from Student;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int id = int.Parse(r["stu_id"].ToString());      // r[0];      문자열 화 후 파싱
                    string name = r["stu_name"].ToString();

                    Student s = new Student(id, name);
                    students.Add(s);
                }
                r.Close();
            }   // cmd.Dispose();

            return students;
        }

        public List<Subject> Selec_Subject_All()
        {
            List<Subject> subjects = new List<Subject>();
            string sql = string.Format($"select * from Subject;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int id = int.Parse(r["sub_id"].ToString());      // r[0];      문자열 화 후 파싱
                    string name = r["sub_name"].ToString();

                    Subject s = new Subject(id, name);
                    subjects.Add(s);
                }
                r.Close();
            }   // cmd.Dispose();

            return subjects;
        }
        #endregion

        // 조인을 사용한 쿼리문들
        public bool SelectData_Names(int stu_id, int sub_id)
        {
            SqlDataReader r = null;
            try
            {
                string sql = string.Format($"select Student.stu_name, Subject.sub_name, Jumsu.jumsu_num from Student, Subject,  Jumsu where Student.stu_id = Jumsu.stu_id and Jumsu.sub_id = Subject.sub_id and Jumsu.stu_id = {stu_id} and Jumsu.sub_id = {sub_id}");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        string stu_name = r["stu_name"].ToString() ;
                        string sub_name = r["sub_name"].ToString() ;
                        int junsu = int.Parse(r["jumsu_num"].ToString());

                        Console.WriteLine($"{stu_name}\t{sub_name}\t{junsu}점");
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
        public void beststu(string name)
        {
            string sql = string.Format($"select top 1 Student.stu_name from Jumsu, Subject, Student where Jumsu.stu_id = Student.stu_id \r\nand Jumsu.sub_id = Subject.sub_id and Subject.sub_name = '{name}' order by jumsu.jumsu_num DESC;\r\n;");
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();

                string stu_name = r["stu_name"].ToString();

                r.Close();

                Console.WriteLine($"점수가 가장 높은 학생 : {stu_name}");

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
