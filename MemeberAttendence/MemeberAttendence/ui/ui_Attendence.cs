using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;

namespace MemeberAttendence
{
    internal class ui_Attendence
    {
        public void Invoke()
        {
            Console.WriteLine("[출석체크]");
            try
            {
                string name = WbLib.InputString("이름 입력: ");
                
                MemberControl con = MemberControl.singleton;
                con.Attendence(name);
                Console.WriteLine("출석!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[회원가입] " + ex.Message);
            }
        }
    }
}
