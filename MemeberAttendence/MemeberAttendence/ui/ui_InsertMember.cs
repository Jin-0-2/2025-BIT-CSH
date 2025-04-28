using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;

namespace MemeberAttendence
{
    internal class ui_InsertMember
    {
        public void Invoke()
        {
            Console.WriteLine("[회원가입]");
            try
            {
                string name = WbLib.InputString("이름 입력: ");
                string phone = WbLib.InputString("전화번호 입력: ");

                MemberControl con = MemberControl.singleton;
                con.MemberInsert(name, phone);
                Console.WriteLine("[회원가입 완료]");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[회원가입] " + ex.Message);
            }
        }
    }
}
