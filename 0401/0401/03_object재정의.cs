using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    internal class Member
    {
        private string name;
        private string phone;

        public Member(string _name, string _phone)
        {
            this.name = _name;
            this.phone = _phone;
        }

        public void Print()
        {
            Console.WriteLine(name + "\t" + phone);
        }

        public override string ToString()
        {
            return name + "\t" + phone;
        }

        public override bool Equals(object obj)
        {
            Member _member = (Member)obj;
            return(name == _member.name) && (phone == _member.phone);
        }
    }
}
