using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _0409_저기요.Data
{
    internal class Restaurant : Client
    {
        public string R_Name { get; private set; }
        public List<Menu> MenuList { get; private set; }

        public Restaurant(string id, string pw, string r_name, int type) : base(id, pw, type)
        {
            R_Name = r_name;
            MenuList = new List<Menu>();
        }   

        public void Add_Menu(string name, int price)
        {
            MenuList.Add(new Menu(name, price));
        }
    }
}
