using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using JJY.DATA;
using JJY.Lib;

namespace MemeberAttendence
{
    internal class ui_PrintAllMember
    {
        public void Invoke()
        {
            Console.WriteLine("[전체회원출력]");
            try
            {
                MemberControl con = MemberControl.singleton;
                Member_PrintAll(con.Members, con.Members_Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[전체회원출력] " + ex.Message);
            }
        }
        private void Member_PrintAll(List<Member> members, int count)
        {
            Console.WriteLine("회원 저장 개수 : " + count);
            foreach (Member member in members)
            {
                member.Print();
            }
        }

    }
}
