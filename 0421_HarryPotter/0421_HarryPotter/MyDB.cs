using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace _0421_HarryPotter
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

        // db에 데이터가 있는지 확인
        public int check()
        {
            string sql = $"SELECT COUNT(*) from HarryPotter";
            return (int)ExSqScalar_Command(sql);
        }
        #endregion

        #region Insert, Update Delete Test

        // insert into 테이블 values(값, 값, 값);
        public  bool Insert_Member(Member m)
        {
            string sql = string.Format($"insert into HarryPotter values ('{m.Name}','{m.Sepecies}', '{m.Gender}', '{m.House}','{m.Birth}', '{m.Eye_color}', '{m.Hair_color}', '{m.Actor}', '{m.Still_Alive}', '{m.Image}');");
            return ExSqlCommand(sql);
        }

        // update 테이블 set 바꿀것 = 바꿀값  where 테이블의값 = 바꿀거


        #endregion

        #region 하나의 값을 반환하는 Select
        // select SUM(원하는속성) from 테이블 where 조건 = 조건

        public bool SelectMember_Name(string name)
        {
            SqlDataReader r = null;
            try
            {
                string sql = string.Format($"select * from HarryPotter where name = '{name}';");
                using (SqlCommand cmd = new SqlCommand(sql, scon))
                {
                    r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        string _name            = r["name"].ToString();
                        string _sepecies        = r["species"].ToString();
                        string _gender          = r["gender"].ToString();
                        string _house           = r["house"].ToString();
                        string _birth           = r["birth"].ToString();
                        string _eye_color       = r["eye_color"].ToString();
                        string _hair_color      = r["hair_color"].ToString();
                        string _actor           = r["actor"].ToString();
                        string _still_alive     = r["still_alive"].ToString();
                        string _image           = r["image"].ToString();

                        Member m = new Member(_name, _sepecies, _gender, _house, _birth, _eye_color, _hair_color, _actor, _still_alive, _image);

                        m.Println();
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

        public List<string> SelectMember_Names_All()
        {
            SqlDataReader r = null;

            string sql = string.Format($"select name from HarryPotter;");
            List<string> list = new List<string>();
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string _house = r["name"].ToString();

                    list.Add(_house);
                }
                r.Close();
            }
            return list;

        }

        public List<string> SelectMember_House(string house)
        {
            SqlDataReader r = null;

            string sql = string.Format($"select name from HarryPotter where house = '{house}';");
            List<string> list = new List<string>();
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string _house = r["name"].ToString();

                    list.Add(_house);
                }
                r.Close();
            }
            return list;

        }

        public List<string> SelectMember_Gender(string gender)
        {
            SqlDataReader r = null;

            string sql = string.Format($"select name from HarryPotter where gender = '{gender}';");
            List<string> list = new List<string>();
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string name = r["name"].ToString();

                    list.Add(name);
                }
                r.Close();
            }
            return list;
        }

        public List<string> Select_PrintSpecies()
        {
            SqlDataReader r = null;

            string sql = string.Format($"select species from HarryPotter group by species");
            List<string> list = new List<string>();
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string species = r["species"].ToString();

                    list.Add(species);
                }
                r.Close();
            }

            return list;

        }
        public List<string> SelectMember_Species(string species)
        {
            SqlDataReader r = null;

            string sql = string.Format($"select name from HarryPotter where species = '{species}';");
            List<string> list = new List<string>();
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string name = r["name"].ToString();

                    list.Add(name);
                }
                r.Close();
            }
            return list;
        }

        public List<string> SelectMember_Still_Alive(string still_alive)
        {
            SqlDataReader r = null;

            string sql = string.Format($"select name from HarryPotter where still_alive = '{still_alive}';");
            List<string> list = new List<string>();
            using (SqlCommand cmd = new SqlCommand(sql, scon))
            {
                r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string name = r["name"].ToString();

                    list.Add(name);
                }
                r.Close();
            }
            return list;
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
