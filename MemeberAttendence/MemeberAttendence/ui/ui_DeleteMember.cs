using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;

namespace MemeberAttendence.ui
{
    internal class ui_DeleteMember
    {
        public void Invoke()
        {
            Console.WriteLine("[회원탈퇴]");
            try
            {
                int Id = WbLib.InputNumber("회원번호 입력: ");

                MemberControl con = MemberControl.singleton;
                con.DeleteMember(Id);
                Console.WriteLine("회원 탈퇴 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[회원탈퇴] " + ex.Message);
            }
        }

    }
}
