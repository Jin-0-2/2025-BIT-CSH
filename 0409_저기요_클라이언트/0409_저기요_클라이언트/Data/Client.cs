using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0409_저기요.Data
{
    internal class Client
    {
        public const int User = 1;
        public const int Owner = 2;
        public string Id { get; private set; }
        public string Pw { get; private set; }
        public int Type { get; private set; }

        public Client(string id, string pw, int type)
        {
            Id = id;
            Pw = pw;
            Type = type;
        }
    }
}
