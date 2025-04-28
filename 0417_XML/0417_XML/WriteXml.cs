using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0417_XML
{
    internal class WriteXml
    {
        public void Writer_Xml(List<Account> accounts)
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;

            XmlWriter xwriter = XmlWriter.Create("data.xml", xsettings);
            xwriter.WriteComment("XML 생성~ Account용도임 17/4/25");
            xwriter.WriteStartElement("WB41");

            foreach(Account account in accounts)
            {
                xwriter.WriteStartElement("Account");
                xwriter.WriteElementString("id", $"{account.Id}");
                xwriter.WriteElementString("name", account.Name);
                xwriter.WriteElementString("balance", $"{account.Balance}");
                xwriter.WriteElementString("date", $"{account.Time}");
                xwriter.WriteEndElement();  // Account 닫기
            }
            xwriter.WriteEndElement();      // WB41(Root) 닫기

            xwriter.Close();
        }

    }
}
