using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.DATA;
using JJY.Lib;

namespace MemeberAttendence
{
    internal class ui_SelectMember
    {
        public void Invoke()
        {
            Console.WriteLine("[회원검색]");
            try
            {
                int Id = WbLib.InputNumber("회원번호 입력: ");

                MemberControl con = MemberControl.singleton;
                Member mem = con.SelectMember(Id);
                mem.Println();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[회원검색] " + ex.Message);
            }
        }

    }
}
