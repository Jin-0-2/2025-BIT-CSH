using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스
{
    internal class Book
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }


        public Book()
        {

        }
        public Book(int number, string name, int price, string description)
        {
            Number = number;
            Name = name;
            Price = price;
            Description = description;
        }

        public void BookPrint()
        {
            // 한줄에 출력
            Console.Write(Number + "\t");
            Console.Write(Name + "\t");
            Console.Write(Price + "원\t");
            Console.WriteLine(Description);
        }

        public void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[번호] " + Number);
            Console.WriteLine("[이름] " + Name);
            Console.WriteLine("[가격] " + Price + "원");
            Console.WriteLine("[한줄 요약] " + Description);
        }

        public override string ToString()
        {
            return Number + ", " + Name + ", " + Price + ", " + Description;
        }
        // 생성자 없.
    }
}
