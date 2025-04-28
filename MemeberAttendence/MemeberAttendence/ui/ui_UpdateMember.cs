using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;

namespace MemeberAttendence.ui
{
    internal class ui_UpdateMember
    {
        public void Invoke()
        {
            Console.WriteLine("[회원정보수정]");
            try
            {
                string name = WbLib.InputString("이름 입력: ");
                string phone = WbLib.InputString("전화번호 입력: ");


                MemberControl con = MemberControl.singleton;
                con.UpdateMember(name, phone);

                Console.WriteLine("회원 정보 수정 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[회원정보수정] " + ex.Message);
            }
        }

    }
}
