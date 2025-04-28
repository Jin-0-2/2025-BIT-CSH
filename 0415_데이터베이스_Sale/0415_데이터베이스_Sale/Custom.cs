using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Sale
{
    internal class Custom
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Addr { get; set; }


        public Custom()
        {

        }
        public Custom(int number, string name, string phone, string addr)
        {
            Number = number;
            Name = name;
            Phone = phone;
            Addr = addr;
        }

        public void CustomPrint()
        {
            // 한줄에 출력
            Console.Write(Number + "\t");
            Console.Write(Name + "\t");
            Console.Write(Phone + "\t");
            Console.WriteLine(Addr);
        }

        public void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[번호] " + Number);
            Console.WriteLine("[이름] " + Name);
            Console.WriteLine("[전화번호] " + Phone);
            Console.WriteLine("[주소] " + Addr);
        }

        public override string ToString()
        {
            return Number + ", " + Name + ", " + Phone + ", " + Addr;
        }
    }
}
