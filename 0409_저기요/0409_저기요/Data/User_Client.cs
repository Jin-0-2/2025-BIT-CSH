using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0409_저기요.Data
{
    internal class User_Client : Client
    {
        public string Name { get; private set; }
        public int Balace { get; private set; }

        public User_Client(string id, string pw, string name, int type) : base(id, pw, type)
        {
            Name = name;
            Balace = 0;
        }
        public User_Client(string id, string pw, string name, int type, int balace) : base(id, pw, type)
        {
            Name = name;
            Balace = balace;
        }

        public void Add_Money(int money)
        {
            Balace += money;
        }

        public int Sub_Money(int money)
        {
            return Balace -= money;
        }
    }
}
