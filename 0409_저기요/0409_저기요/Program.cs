using _0409_저기요.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0409_저기요
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = App.singleton;
            app.Init();
            Console.Clear();
            Console.ReadLine();
            app.Exit();
        }
    }
}
