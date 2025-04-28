using _0409_저기요.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _0409_저기요_클라이언트
{
    internal class UserControl
    {
        private const int SERVER_PORT = 7000; // 서버 포트 번호
        private const string SERVER_IP = "127.0.0.1"; // 서버 포트 번호

        private static User_Client User = null; // 로그인한 유저 정보
        private string cur_restaurant = null; // 현재 선택된 레스토랑 정보
        public bool cur_r { get; private set; } = false;

        string my_id = null; // 로그인한 유저의 아이디
        public bool cur_login {  get; private set; } = false;
        int my_balance = 0;

        private MyUserClient u_client = new MyUserClient();

        #region 0. 싱글톤 패턴
        public static UserControl singleton { get; } = null;

        static UserControl() { singleton = new UserControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private UserControl()
        {
            // logmanager = new LogManager(); 이 상황은 만들어지지 않은 AccountControl의 객체를 접근함.
        }
        #endregion

        #region 1. 기능 메서드
        #region 고객 -> 서버
        // 회원 가입
        public void Signup(string id, string pw, string name, int type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pw) || string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("모든 필드를 올바르게 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }
                string packet = Packet.Signup(id, pw, name, type);
                u_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 로그인
        public void Login(string id, string pw)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pw))
                {
                    Console.WriteLine("아이디와 비밀번호를 올바르게 입력해주세요.");
                    throw new Exception("잘못된입력"); // 잘못된 입력 시 종료
                }

                string packet = Packet.Login(id, pw, Client.User);
                my_id = id;
                // 서버에 전송
                u_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 잔액 추가
        public void Add_money(int input_money)
        {
            try
            {
                if (input_money <= 0)
                {
                    Console.WriteLine("올바른 금액을 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }
                string packet = Packet.Add_Money(my_id, input_money);
                u_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
            
        }

        // 가게 선택
        public void Select_R(string r_name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(r_name))
                {
                    Console.WriteLine("레스토랑 이름을 올바르게 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }
                string packet = Packet.Select_R(r_name);

                // 서버에 전송
                u_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 메뉴 선택
        public void Select_M(string m_name, int count)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(m_name) || count <= 0)
                {
                    Console.WriteLine("올바른 메뉴 이름과 수량을 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }

                string packet = Packet.Select_M(cur_restaurant, my_id,  m_name, count);
                // 서버에 전송
                u_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 가게 리스트 출력
        public void R_print()
        {
            try
            {
                string packet = Packet.R_print();
                u_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 서버 -> 고객
        // 회원 가입
        public void Signup_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                if (isbool == true)
                {
                    // 회원가입 성공
                    Console.WriteLine($"[회원가입 성공] {info}");
                }
                else if (isbool == false)
                {
                    // 회원가입 실패
                    Console.WriteLine($"[회원가입 실패] {info}");
                }
                else
                    Console.WriteLine($"[회원가입 오류]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);       //예외 재 전송!!!!
            }
        }

        // 로그인
        public void Login_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                string c_id = sp[2];
                string c_name = sp[3];

                if (isbool == true)
                {
                    // 로그인 성공
                    my_id = c_id;
                    cur_login = true;
                    Console.WriteLine($"{c_name}님 {info} ");
                }
                else if (isbool == false)
                {
                    // 로그인 실패
                    Console.WriteLine($"[로그인 실패] {info}");
                }
                else
                    Console.WriteLine("로그인 오류");

            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 잔액 추가
        public void Add_Money_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int cur_balance = int.Parse(sp[2]);

                if (isbool == true)
                {
                    // 로그인 성공
                    my_balance = cur_balance;
                    Console.WriteLine($"{info}님 잔액: {my_balance}원 ");
                }
                else if (isbool == false)
                {
                    // 로그인 실패
                    Console.WriteLine($"[충전 실패] {info}");
                }
                else
                    Console.WriteLine("로그인 오류");

            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 가게 선택
        public void Select_R_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('&');
                bool isbool = bool.Parse(sp[0]);

                if (isbool == true)
                {
                    string r_name = sp[1];
                    cur_restaurant = r_name;
                    cur_r = true;

                    string[] sp2 = sp[2].Split('$');

                    foreach (string s in sp2)
                    {
                        string[] menu = s.Split('#');
                        Console.WriteLine($"{menu[0]} - {menu[1]}원");
                    }
                }
                else if (isbool == false)
                {
                    Console.WriteLine("없는 가게입니다.");
                }
                else
                {
                    Console.WriteLine("가게 선택 오류");
                }
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 메뉴 선택(결제 성공/실패)
        public void Select_M_Ack_Pay(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int amount = int.Parse(sp[2]);

                if (isbool == true)
                {
                    Console.WriteLine($"[{info}] 잔액 : {amount}원");
                }
                else if (isbool == false)
                {
                    // 회원가입 실패
                    Console.WriteLine($"[{info}] 필요 금액 : {amount}원");
                }
                else
                    Console.WriteLine($"[결제 오류]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);       //예외 재 전송!!!!
            }
        }

        // 메뉴 선택(주문 수락/ 소요 시간)
        public void Select_M_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int cook_t = int.Parse(sp[2]);

                if (isbool == true)
                {
                    // 주문 성공
                    Console.WriteLine($"[{info}] {cook_t}소요 예정");

                }
                else if (isbool == false)
                {
                    // 주문 실패
                    Console.WriteLine($"[주문 실패] {info}");
                }
                else
                {
                    Console.WriteLine("[주문 오류]");
                }
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }

        // 가게 리스트 출력
        public void R_print_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            
            int i = 1;
            foreach (string s in sp)
            {
                Console.WriteLine($"{i}: {s}");
                i++;
            }
        }
        #endregion
        
        

        public void LogMessage1(Socket sock, string message)
        {
            Console.WriteLine($"[log] {message}");
        }
        public void PacketMessage1(Socket sock, string message)
        {
            //1. 패킷 수신
            Console.WriteLine($"[packet] {message}");

            //2. 패킷 파싱(분석)/분할 처리
            string[] sp = message.Split('@');
            Console.WriteLine($"");
            switch (int.Parse(sp[0]))
            {
                case Packet.PACKET_R_PRINT_ACK:           R_print_Ack(sp[1]);  break;
                case Packet.PACKET_SIGNUP_ACK:            Signup_Ack(sp[1]); break;
                case Packet.PACKET_LOGIN_ACK:             Login_Ack(sp[1]);    break;
                case Packet.PACKET_SELECT_R_ACK:          Select_R_Ack(sp[1]); break;
                case Packet.PACKET_SELECTMENU_C_ARK_PAY:  Select_M_Ack_Pay(sp[1]); break;
                case Packet.PACKET_SELECTMENU_C_ARK_TIME: Select_M_Ack(sp[1]); break;
                case Packet.PACKET_ADD_MONEY_ACK:         Add_Money_Ack(sp[1]); break;
            }
        }
        #endregion

        #region 2. 시작/종료 메서드
        public void Init()
        {
            if (u_client.Start(SERVER_IP, SERVER_PORT, LogMessage1, PacketMessage1) == false)
            {
                Console.WriteLine("서버 연결 실패");
                return;
            }
            Console.WriteLine("서버 연결 성공........");
        }
        public void Exit()
        {
            u_client.Close();
        }
        #endregion
    }
}
