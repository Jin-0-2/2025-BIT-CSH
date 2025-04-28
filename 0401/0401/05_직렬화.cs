using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal class _05_직렬화
    {
        static void Main(string[] args)
        {
            // 직렬화
            string name = "홍길동";
            int age = 20;
            string phone = "010-1111-1111";
            char gender = '남';

            string str = name + "@" + age + "@" + phone + "@" + gender;
            Console.WriteLine("직렬화 결과 : " + str);

            // 역직렬화
            string[] str1 = str.Split('@');
            string name1 = str1[0];
            string age1 = str1[1];
            string phone1 = str1[2];
            char gender1 = char.Parse(str1[3]);
            Console.WriteLine(name1 + ", " + age1 + ", " + phone1 + ", " + gender1);
        }
    }
}