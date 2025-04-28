// MyDB1.cs
// StroedProcedure 활용한 DB 사용

using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;

namespace _0415_데이터베이스
{
    internal class MyDB1
    {
        private const string server_name = "DESKTOP-PRQ0INH\\SQLEXPRESS";
        private const string db_name = "WB41";
        private const string sql_id = "aaa";
        private const string sql_pw = "1234";

        private SqlConnection scon = null;

        #region 연결, 종료
        public bool Connect()
        {
            try
            {
                string con = string.Format($"Data Source={server_name};Initial Catalog={db_name};User ID={sql_id};Password={sql_pw}");
                scon = new SqlConnection(con);        // 커넥션 연결 문자열만 있으면 됨.
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

        #region DB 명령 함수(ExecuteNonQuery():I,U,D), (ExecuteScalar():S)
        private bool ExSqlCommand(SqlCommand cmd)
        {
            try
            {
                if (cmd.ExecuteNonQuery() == 0)     // 트랜잭션을 실행하고 영향을 받는 행의 수를 반환.
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //private object ExSqScalar_Command(string sql)
        //{
        //    try
        //    {
        //        scmd.Connection = scon;
        //        scmd.CommandText = sql;

        //        return scmd.ExecuteScalar();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
        #endregion

        #region 프로시저 사용코드
        public void AddProduct(string pname, int price, string description)
        {
            string comtext = "AddProduct";

            SqlCommand scmd = new SqlCommand(comtext, scon);
            scmd.CommandType = System.Data.CommandType.StoredProcedure;

            // 파라미터 등록
            SqlParameter param_pname = new SqlParameter("@PNAME", pname);
            scmd.Parameters.Add(param_pname);

            SqlParameter param_price = new SqlParameter("@Price", price);
            param_price.SqlDbType = System.Data.SqlDbType.Int; 
            scmd.Parameters.Add(param_price);

            SqlParameter param_descriptrion = new SqlParameter("Description", description);
            scmd.Parameters.Add(param_descriptrion);



            if(ExSqlCommand(scmd) == true)
                Console.WriteLine("성공");
            else
                Console.WriteLine("실패");
            scmd.Dispose();
    }
        public void FindCIDByName(string cname)
        {
            string comtext = "FindCIDByName";

            SqlCommand scmd = new SqlCommand(comtext, scon);
            scmd.CommandType = System.Data.CommandType.StoredProcedure;   

            // 파라미터 등록
            SqlParameter param_cname = new SqlParameter("@CNAME", cname);
            scmd.Parameters.Add(param_cname);

            SqlParameter param_cid = new SqlParameter();
            param_cid.ParameterName = "@CID";
            param_cid.SqlDbType = System.Data.SqlDbType.Int;
            param_cid.Direction = System.Data.ParameterDirection.Output;
            scmd.Parameters.Add(param_cid);


            if (ExSqlCommand(scmd) == true)
            {
                Console.WriteLine("성공");
                int cid = (int)param_cid.Value;
                Console.WriteLine(cname + "의 ID : " + cid);
            }
            else
                Console.WriteLine("실패");
            scmd.Dispose();
        }
    #endregion

}
}
