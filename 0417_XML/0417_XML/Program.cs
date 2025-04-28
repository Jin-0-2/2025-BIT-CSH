using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0417_XML
{
    internal class Program
    {
        static void Exam1()
        {
            new MyXML().Sample("https://www.joseilbo.com/news/htmls/2025/04/20250417541296.html");
        }
        static void Main(string[] args)
        {
            App app = App.singleton;
            app.Init();
            app.Run();
            app.Exit();
        }
    }
}
