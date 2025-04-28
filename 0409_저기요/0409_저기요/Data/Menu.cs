using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0409_저기요.Data
{
    internal class Menu
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        public Menu(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
