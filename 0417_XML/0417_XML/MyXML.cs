using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0417_XML
{
    internal class MyXML
    {
        public void Sample(string str)
        {
            XmlUrlResolver resolver = new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

            XmlReaderSettings settings = new XmlReaderSettings();  
            settings.XmlResolver = resolver;

            XmlReader reader = XmlReader.Create(str);
            WriteConsole(reader);
            reader.Close();
        }

        private void WriteConsole(XmlReader reader)
        {
            XmlWriter xmlWriter = XmlWriter.Create(Console.Out);
            xmlWriter.WriteNode(reader, false);                         // Boom
            xmlWriter.Close();
            Console.WriteLine( );
        }
    }
}
