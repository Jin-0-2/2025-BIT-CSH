// Packet_Server.cs

using _0409_저기요.Data;
using System.Collections.Generic;

namespace Packet
{

    internal static class Packet_Server
    {
        public const int PACKET_SIGNUP          = 1;
        public const int PACKET_SIGNUP_ACK      = 2;

        public const int PACKET_LOGIN           = 3;
        public const int PACKET_LOGIN_ACK       = 4;

        public const int PACKET_SELECT_R        = 5;
        public const int PACKET_SELECT_R_ACK    = 6;

        public const int PACKET_ADD_MENU        = 7;
        public const int PACKET_ADD_MENU_ACK    = 8;

        public const int PACKET_SELECTMENU_C      = 9;
        public const int PACKET_SELECTMENU_C_ARK_PAY = 17;
        public const int PACKET_SELECTMENU_C_ARK_TIME  = 13;
        public const int PACKET_SELECTMENU_R      = 14;
        public const int PACKET_SELECTMENU_R_ACK  = 10;

        public const int PACKET_ADD_MONEY       = 11;
        public const int PACKET_ADD_MONEY_ACK   = 12;

        public const int PACKET_R_PRINT = 15;
        public const int PACKET_R_PRINT_ACK = 16;

        public static string PACKET_R_print_Ack(bool isbool, List<Restaurant> restaurants)
        {
            string packet = PACKET_R_PRINT_ACK + "@";
            foreach (Restaurant r in restaurants)
            {
                packet += r.R_Name + "#";
            }
            packet = packet.TrimEnd('#');
            return packet;
        }
        public static string PACKET_Sign_up_Ack(bool isbool, string msg) 
        {
            string packet = PACKET_SIGNUP_ACK + "@";
            packet += isbool + "#";
            packet += msg;

            return packet;
        }
        public static string PACKET_Login_Ack(bool isbool, string msg, string c_id, string c_name)
        {
            string packet = PACKET_LOGIN_ACK + "@";
            packet += isbool + "#";
            packet += msg + "#";
            packet += c_id + "#";
            packet += c_name;

            return packet;
        }
        public static string PACKET_Add_Money_Ack(bool isbool, string msg, int balance)
        {
            string packet = PACKET_ADD_MONEY_ACK + "@";
            packet += isbool + "#";
            packet += msg + "#";
            packet += balance;

            return packet;
        }
        public static string PACKET_Select_R_Ack(bool isbool, string r_name, List<Menu> menus)
        {
            string packet = PACKET_SELECT_R_ACK + "@";
            packet += isbool + "&";
            packet += r_name + "&";

            foreach (Menu m in menus)
            {
                packet += m.Name + "#";
                packet += m.Price + "$";
            }
            packet = packet.TrimEnd('$');

            return packet;
        }
        public static string PACKET_Add_Menu_Ack(bool isbool, string msg)
        {
            string packet = PACKET_ADD_MENU_ACK + "@";
            packet += isbool + "#";
            packet += msg;

            return packet;
        }
        public static string PACKET_SelectMenu_TO_R(string menu, int count, string id)
        {
            string packet = PACKET_SELECTMENU_R + "@";

            packet += menu + "#";
            packet += count + "#";
            packet += id;

            return packet;
        }
        public static string PACKET_SelectMenu_Ack_To_C(bool isbool, string msg, int cook_t)
        {
            string packet = PACKET_SELECTMENU_C_ARK_TIME + "@";
            packet += isbool + "#";
            packet += msg + "#" ;
            packet += cook_t;

            return packet;
        
        }
        public static string PACKET_SelectMenu_Ack_To_C_Pay(bool isbool, string msg, int amount)
        {
            string packet = PACKET_SELECTMENU_C_ARK_PAY + "@";
            packet += isbool + "#";
            packet += msg + "#";
            packet += amount;

            return packet;
        }
    }
}
