using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.DATA;

namespace MemeberAttendence
{
    internal class ui_AttendenceRank
    {
        public void Invoke()
        {
            Console.WriteLine("[출석 왕]");
            try
            {
                MemberControl con = MemberControl.singleton;
                List<Member> _members = con.AttendenceRank();
                _members.Sort((x, y) => y.AttendenceCount.CompareTo(x.AttendenceCount));
                int rank = 1;
                foreach (Member member in _members)
                {
                    Console.WriteLine(string.Format("[{0}] {1}님 출석 {2}회", rank, member.Name, member.AttendenceCount));
                    rank++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[출석순위] " + ex.Message);
            }
        }
    }
}
