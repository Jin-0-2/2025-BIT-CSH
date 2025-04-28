using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0416_MemDB계좌관리프로그램
{
    internal static class MyXml
    {
        #region XML 파일 사용 

        static readonly string schema_fname = "accounts.xsd";
        static readonly string fname = "accounts.xml";
        public static void Account_Write_Xml(DataSet ds)
        {
            ds.WriteXmlSchema(schema_fname);
            ds.WriteXml(fname);
        }

        public static DataSet Account_Read_Xml()
        {
            DataSet ds = new DataSet("Account");
            if (ds != null)
                ds.Dispose();

            ds.ReadXmlSchema(schema_fname);
            ds.ReadXml(fname);

            return ds;
        }
        #endregion

    }
}