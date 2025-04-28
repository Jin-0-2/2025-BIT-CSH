using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Sale
{
    internal class Sale
    {
        public int Sid { get;  set; }
        public int Cid { get;  set; }
        public int Pid { get;  set; }
        public int Count { get;  set; }
        public DateTime Dtime { get;  set; }

        public Sale()
        {

        }
        public Sale(int sid, int cid, int pid, int count,  DateTime dtime)
        {
            Sid = sid;
            Cid = cid;
            Pid = pid;
            Count = count;
            Dtime = dtime;
        }

        public void SalePrint()
        {
            // 한줄에 출력
            Console.Write(Sid + "\t");
            Console.Write(Cid + "\t");
            Console.Write(Pid + "\t");
            Console.Write(Count + "권\t");
            Console.WriteLine(Dtime);
        }

        public void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[판매 번호] " + Sid);
            Console.WriteLine("[고객 번호] " + Cid);
            Console.WriteLine("[도서 번호] " + Pid);
            Console.WriteLine("[구매 갯수] " + Count + "권");
            Console.WriteLine("[구매 시간] " + Dtime);
        }

        public override string ToString()
        {
            return Sid + ", " + Cid + ", " + Pid + ", " + Count + ", " + Dtime;
        }
    }
}
