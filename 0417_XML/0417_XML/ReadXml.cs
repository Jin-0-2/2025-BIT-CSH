using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0417_XML
{
    internal class ReadXml
    {
        public void Read_Xml(List<Account> accounts)
        {
            XmlReader reader = XmlReader.Create("data.xml");
            while (reader.Read())
            {
                if(reader.IsStartElement("Account"))
                {
                    Account acc = MakeAccount(reader);
                    if (acc != null) accounts.Add(acc);
                }
            }
        }

        private static Account MakeAccount(XmlReader xr)
        {
            int id = 0;
            string name = string.Empty;
            int balance = 0;
            DateTime dt = DateTime.MinValue;

            xr.ReadToDescendant("id");
            id = int.Parse(xr.ReadElementString("id"));

            xr.ReadToNextSibling("name");
            name = xr.ReadElementString("name");

            xr.ReadToNextSibling("balance");
            balance = int.Parse(xr.ReadElementString("balance"));

            xr.ReadToNextSibling("date");
            dt = DateTime.Parse(xr.ReadElementString("date"));

            return new Account(id, name, balance, dt);
        }
        
    }
}
