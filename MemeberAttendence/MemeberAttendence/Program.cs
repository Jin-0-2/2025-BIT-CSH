using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeberAttendence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = App.singleton;
            app.Init();
            app.Run();
            app.Exit();
        }
    }
}
