using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0409_저기요_클라이언트
{
    internal static class Packet
    {
        public const int PACKET_SIGNUP = 1;
        public const int PACKET_SIGNUP_ACK = 2;

        public const int PACKET_LOGIN = 3;
        public const int PACKET_LOGIN_ACK = 4;

        public const int PACKET_SELECT_R = 5;
        public const int PACKET_SELECT_R_ACK = 6;

        public const int PACKET_ADD_MENU = 7;
        public const int PACKET_ADD_MENU_ACK = 8;

        public const int PACKET_SELECTMENU_C = 9;
        public const int PACKET_SELECTMENU_C_ARK_PAY = 17;
        public const int PACKET_SELECTMENU_C_ARK_TIME = 13;
        public const int PACKET_SELECTMENU_R = 14;
        public const int PACKET_SELECTMENU_R_ACK = 10;

        public const int PACKET_ADD_MONEY = 11;
        public const int PACKET_ADD_MONEY_ACK = 12;

        public const int PACKET_R_PRINT = 15;
        public const int PACKET_R_PRINT_ACK = 16;

        public static string R_print()
        {
            string packet = PACKET_R_PRINT + "@";

            return packet;
        }
        public static string Signup(string id, string pw, string name, int type)
        {
            string packet = PACKET_SIGNUP + "@";
            packet += id + "#";
            packet += pw + "#";
            packet += name + "#";
            packet += type;

            return packet;
        }

        public static string Login(string id, string pw, int type)
        {
            string packet = PACKET_LOGIN + "@";
            packet += id + "#";
            packet += pw + "#";
            packet += type; // 0: 고객, 1: 사장님

            return packet;
        }
        public static string Select_R(string r_name)
        {
            string packet = PACKET_SELECT_R + "@";
            packet += r_name;

            return packet;
        }
        public static string Add_Money(string c_id, int money)
        {
            string packet = PACKET_ADD_MONEY + "@";
            packet += c_id + "#";
            packet += money;

            return packet;
        }
        public static string Menu_add(string r_name, string food, int price)
        {
            string packet = PACKET_ADD_MENU + "@";

            packet += r_name + "#";
            packet += food + "#";
            packet += price;

            return packet;
        }
        public static string Select_M(string r_name, string c_id, string m_name, int count)
        {
            string packet = PACKET_SELECTMENU_C + "@";
            packet += r_name + "#";
            packet += c_id + "#";
            packet += m_name + "#";
            packet += count;

            return packet;
        }

        public static string SelectMenu_R_TO_S(bool isbool, string c_id, int cook_t)
        {
            string packet = PACKET_SELECTMENU_R_ACK + "@";
            packet += isbool + "#";
            packet += c_id + "#";
            packet += cook_t;

            return packet;
        }

    }
}
