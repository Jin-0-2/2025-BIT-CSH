using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal class _08_인덱서
    {
        private string[] str = new string[5];

        public string this[int idx]
        {
            get { return str[idx]; }
            set { str[idx] = value; }
        }

        public string this[string msg]
        {
            get
            {
                if (msg == "첫번째") return str[0];
                else if (msg == "두번째") return str[1];
                else return "";
            }
            set
            {
                if(msg == "첫번째")     str[0] = value;
                else if(msg =="두번째") str[1] = value;
            }
        }

        static void Main(string[] args)
        {
            _08_인덱서 ob = new _08_인덱서();
            ob[0] = "ABC";
            Console.WriteLine(ob[0]);

            ob["두번째"] = "abc";
            Console.WriteLine(ob[1]);
        }
    }
}
