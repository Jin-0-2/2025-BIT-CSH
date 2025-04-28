using _0409_저기요.Data;
using System;
using System.Net.Sockets;

namespace _0409_저기요_클라이언트
{
    internal class OwnerControl
    {
        private const int SERVER_PORT = 7000; // 서버 포트 번호
        private const string SERVER_IP = "127.0.0.1"; // 서버 포트 번호

        public static Restaurant restaurant = null; // 로그인한 유저 정보
        string my_r = null; // 로그인한 유저의 아이디

        private MyOwnerClinet o_client = new MyOwnerClinet();

        #region 0. 싱글톤 패턴
        public static OwnerControl singleton { get; } = null;
        static OwnerControl() { singleton = new OwnerControl(); }
        // 디폴트 생성자를 은닉(private) 시킴
        private OwnerControl()
        {
            // logmanager = new LogManager(); 이 상황은 만들어지지 않은 AccountControl의 객체를 접근함.
        }
        #endregion
        #region 1. 기능 메서드
        public void Signup(string id, string pw, string name, int type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pw) || string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("모든 필드를 올바르게 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }
                // 데이터 저장은 서버에서 함. 보내줘야 해
                string packet = Packet.Signup(id, pw, name, type);
                // 서버에 전송
                o_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void Login(string id, string pw)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pw))
                {
                    Console.WriteLine("아이디와 비밀번호를 올바르게 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }

                // 데이터 저장은 서버에서 함. 보내줘야 해
                string packet = Packet.Login(id, pw, Client.Owner);
                my_r = id;
                // 서버에 전송
                o_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void Login_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                string r_name = sp[2];

                if (isbool == true)
                {
                    // 로그인 성공
                    Console.WriteLine($"{r_name}님 {info}");
                }
                else if (isbool == false)
                {
                    // 로그인 실패
                    Console.WriteLine($"[로그인 실패] {info}");
                }
                else
                    throw new Exception("로그인 오류");
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void Food_Insert(string food, int price)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(food) || price <= 0)
                {
                    Console.WriteLine("올바른 음식 이름과 가격을 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }
                string packet = Packet.Menu_add(my_r, food, price);
                //전송
                o_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;   //예외 재 전송!!!!
            }
        }
        public void FoodInsert_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                string food = sp[2];

                if (isbool == true)
                {
                    Console.WriteLine($"{food} 음식 등록 성공");
                }
                else
                {
                    Console.WriteLine("음식 등록 저장 실패");
                    Console.WriteLine($"- {info}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Select_M(bool isbool, string c_id, int cook_t)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(c_id) || cook_t <= 0)
                {
                    Console.WriteLine("올바른 고객 ID와 조리 시간을 입력해주세요.");
                    return; // 잘못된 입력 시 종료
                }
                string packet = Packet.SelectMenu_R_TO_S(isbool, c_id, cook_t);
                //전송
                o_client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;   //예외 재 전송!!!!
            }
        }
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
            switch (int.Parse(sp[0]))
            {
                case Packet.PACKET_SELECTMENU_R: SelectMenu_R.Invoke(sp[1]); break;
                case Packet.PACKET_LOGIN_ACK:   break;
            }
        }
        #endregion
        #region 2. 시작/종료 메서드
        public void Init()
        {
            if (o_client.Start(SERVER_IP, SERVER_PORT, LogMessage1, PacketMessage1) == false)
            {
                Console.WriteLine("서버 연결 실패");
                return;
            }
            Console.WriteLine("서버 연결 성공........");
        }
        public void Exit()
        {
            o_client.Close();
        }
        #endregion
    }
}
