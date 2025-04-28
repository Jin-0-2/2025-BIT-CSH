using _0409_저기요.Data;
using Packet;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

using WSBit41JJY.ServerNet;

namespace _0409_저기요
{
    internal class ServerControl
    {
        private const int SERVER_PORT = 7000; // 서버 포트 번호
        private MyServer _server = new MyServer();

        private Dictionary<string, Socket> C_Sockets = new Dictionary<string, Socket>(); // 고객 소켓 저장
        private Dictionary<string, Socket> R_Sockets = new Dictionary<string, Socket>(); // 가게 소켓 저장

        private List<User_Client> users = new List<User_Client>(10);                        // 고객리스트 저장

        private List<Restaurant> restaurants = new List<Restaurant>(10);                     // 가게리스트 저장
        public List<Restaurant> Restaurants { get { return restaurants; } }                  // accounts에 대한 get 프토퍼티
        public int Accounts_Count { get { return restaurants.Count; } }


        #region 0. 싱글톤 패턴
        public static ServerControl singleton { get; } = null;

        static ServerControl() { singleton = new ServerControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private ServerControl()
        {
            Restaurant rest = new Restaurant("aaa", "bbb", "rest", Client.Owner); 
            Restaurants.Add(rest);
            Menu m = new Menu("menu", 1000);
            rest.Add_Menu(m.Name, m.Price);

            User_Client user = new User_Client("aaa", "bbb", "user", Client.User, 10000);
            users.Add(user);
        }
        #endregion

        #region 1. 기능 메서드
        // 회원가입
        public void Signup(Socket sock, string message)
        {
            // 1. 데이터 획득
            string[] sp = message.Split('#');
            string id = sp[0];
            string pw = sp[1];
            string name = sp[2];
            int type = int.Parse(sp[3]);


            // 2. 데이터 처리
            if (type == Client.User)
            {
                User_Client user = users.Find(acc => acc.Id == id);
                if (user != null)
                {
                    string packet_f = Packet_Server.PACKET_Sign_up_Ack(false, "동일한 아이디 존재");
                    _server.SendData(sock, packet_f);
                }
                    
                //고객 저장
                User_Client user_Client = new User_Client(id, pw, name, type);
                users.Add(user_Client);
                
                string packet = Packet_Server.PACKET_Sign_up_Ack(true, "고객 회원가입 성공");
                _server.SendData(sock, packet);
            }
            else if (type == Client.Owner)
            {
                Restaurant restaurant = restaurants.Find(acc => acc.Id == id);
                {
                    string packet_f = Packet_Server.PACKET_Sign_up_Ack(false, "동일한 아이디 존재");
                    _server.SendData(sock, packet_f);
                }
                //계좌 저장
                Restaurant restaurant_Client = new Restaurant(id, pw, name, type);
                restaurants.Add(restaurant_Client);

                string packet = Packet_Server.PACKET_Sign_up_Ack(true, "사장님 회원가입 성공");
                _server.SendData(sock, packet);
            }
            else
            {
                string packet_f = Packet_Server.PACKET_Sign_up_Ack(false, "회원가입 실패");
                _server.SendData(sock, packet_f);
            }
        }

        // 로그인
        public void Login(Socket sock, string message)
        {
            // 1. 데이터 획득
            string[] sp = message.Split('#');
            string id = sp[0];
            string pw = sp[1];
            int type = int.Parse(sp[2]);

            // 2. 데이터 처리
            if (type == Client.User)
            {
                User_Client user = users.Find(acc => acc.Id == id);
                if (user == null)
                    throw new Exception("없는 아이디입니다.");
                if (user.Pw != pw)
                    throw new Exception("비밀번호가 틀립니다.");

                if (!C_Sockets.ContainsKey(id))
                {
                    C_Sockets[id] = sock;
                }

                string packet = Packet_Server.PACKET_Login_Ack(true, "고객 로그인 성공", user.Id, user.Name);
                _server.SendData(sock, packet);
            }
            else if (type == Client.Owner)
            {
                Restaurant restaurant = restaurants.Find(acc => acc.Id == id);
                if (restaurant == null)
                    throw new Exception("없는 아이디입니다.");
                if (restaurant.Pw != pw)
                    throw new Exception("비밀번호가 틀립니다.");

                if (!R_Sockets.ContainsKey(id))
                {
                    R_Sockets[id] = sock;
                }

                string packet = Packet_Server.PACKET_Login_Ack(true, "사장님 로그인 성공", restaurant.Id, restaurant.R_Name);
                _server.SendData(sock, packet);
            }
            else
            {
                string packet = Packet_Server.PACKET_Login_Ack(false, "로그인 실패", null, null);
                _server.SendData(sock, packet);
            }
        }

        // (고객) 잔액 추가
        public void Add_Money(Socket sock, string message)
        {
            // 1. 데이터 획득
            string[] sp = message.Split('#');
            string c_id = sp[0];
            int money = int.Parse(sp[1]);

            // 2. 데이터 처리
            User_Client user = users.Find(acc => acc.Id == c_id);
            if (user == null)
            {
                string packet_f = Packet_Server.PACKET_Add_Money_Ack(false, "사용자 없음", 0);
                _server.SendData(sock, packet_f);
            }
                

            user.Add_Money(money);
            string packet = Packet_Server.PACKET_Add_Money_Ack(true, "충전 성공", user.Balace);
            _server.SendData(sock, packet);
        }

        // (고객) 가게 선택
        public void Select_R(Socket sock, string message)
        {
            // 1. 데이터 획득
            string r_name = message;

            // 2. 데이터 처리
            Restaurant restaurant = restaurants.Find(acc => acc.R_Name == r_name);
            if (restaurant == null)
            {
                string packet_f = Packet_Server.PACKET_Select_R_Ack(false, null, null);
                _server.SendData(sock, packet_f);
            }
            string packet = Packet_Server.PACKET_Select_R_Ack(true, restaurant.R_Name ,restaurant.MenuList);
            _server.SendData(sock, packet);
        }
        public void Add_Menu(Socket sock, string message)
        {
            // 1. 데이터 획득
            string[] sp = message.Split('#');
            string r_id = sp[0];
            string menu_name = sp[1];
            int price = int.Parse(sp[2]);
            // 2. 데이터 처리
            Restaurant restaurant = restaurants.Find(acc => acc.Id == r_id);
            if (restaurant == null)
                throw new Exception("없는 가게입니다.");
            restaurant.Add_Menu(menu_name, price);
            string packet = Packet_Server.PACKET_Add_Menu_Ack(true, "메뉴 추가 성공");
            _server.SendData(sock, packet);
        }
        public void Select_M(Socket sock, string message)
        {
            // 1. 데이터 획득
            string[] sp = message.Split('#');
            string r_name = sp[0];
            string c_id = sp[1];
            string menu_name = sp[2];
            int count = int.Parse(sp[3]);

            // 2. 데이터 처리
            Restaurant restaurant = restaurants.Find(acc => acc.R_Name == r_name);
            if (restaurant == null)
                throw new Exception("없는 가게입니다.");

            Menu menu = restaurant.MenuList.Find(m => m.Name == menu_name);
            if (menu == null)
                throw new Exception("없는 메뉴입니다.");

            User_Client user = users.Find(acc => acc.Id == c_id);
            if (user == null)
                throw new Exception("없는 아이디입니다.");

            // 계산 후 잔액과 비교
            int total_price = menu.Price * count;

            if (total_price > user.Balace)
            {
                string packet = Packet_Server.PACKET_SelectMenu_Ack_To_C_Pay(false, "잔액 부족", total_price-user.Balace);
                _server.SendData(sock, packet);
            }
            else if (total_price <= user.Balace)
            {
                int amount = user.Sub_Money(total_price);
                string packet2 = Packet_Server.PACKET_SelectMenu_Ack_To_C_Pay(true, "결제 성공", user.Balace );
                _server.SendData(sock, packet2);

                string packet = Packet_Server.PACKET_SelectMenu_TO_R(menu.Name, count, c_id);
                _server.SendData(GetRSocketById(restaurant.Id), packet);
            }
            else
            {
                throw new Exception("주문 실패");
            }
        }
        public void Select_M_To_C(Socket sock, string message)
        {
            // 1. 데이터 획득
            string[] sp = message.Split('#');
            bool ischeck = bool.Parse(sp[0]);
            string c_id = sp[1];
            int cook_t = int.Parse(sp[2]);

            // 2. 데이터 처리
            if (ischeck == false)
            {
                string packet_f = Packet_Server.PACKET_SelectMenu_Ack_To_C(false, "주문 실패", 0);
                _server.SendData(GetCSocketById(c_id), packet_f);
            }

            string packet = Packet_Server.PACKET_SelectMenu_Ack_To_C(true, "주문 성공", cook_t);
            _server.SendData(GetCSocketById(c_id), packet);
        }
        public void R_print(Socket sock, string message)
        {
            // 1. 데이터 획득
            string packet = Packet_Server.PACKET_R_print_Ack(true, restaurants);
            _server.SendData(sock, packet);
        }
        #endregion

        #region 2. 시작/종료 메서드
        public void Init()
        {
            if (_server.Start(SERVER_PORT, LogMessage, PacketMessage) == false)
                return;
            Console.WriteLine("서버 실행........");
        }
        public void Exit()
        {
            _server.Close();
            Console.WriteLine("서버 종료........");
        }
        #endregion

        #region 3. 네트워크 CallBack 메서드 -> 기능 메서드
        public void LogMessage(Socket sock, string message)
        {
            Console.WriteLine($"[log] {message}");
        }
        public void PacketMessage(Socket sock, string message)
        {
            //1. 패킷 수신 (굳이?)s
            Console.WriteLine($"[packet] {message}");

            //2. 패킷 파싱(분석)
            string[] sp = message.Split('@');

            // 3. 분석 분할 처리
            switch (int.Parse(sp[0]))
            {
                case Packet_Server.PACKET_R_PRINT:              R_print(sock, sp[1]);       break;
                case Packet_Server.PACKET_SIGNUP:               Signup(sock, sp[1]);        break;
                case Packet_Server.PACKET_LOGIN:                Login(sock, sp[1]);         break;
                case Packet_Server.PACKET_SELECT_R:             Select_R(sock, sp[1]);      break;
                case Packet_Server.PACKET_ADD_MENU:             Add_Menu(sock, sp[1]);      break;
                case Packet_Server.PACKET_ADD_MONEY:            Add_Money(sock, sp[1]);      break;
                case Packet_Server.PACKET_SELECTMENU_C:         Select_M(sock, sp[1]);      break;
                case Packet_Server.PACKET_SELECTMENU_R_ACK:     Select_M_To_C(sock, sp[1]); break;
            }
        }
        #endregion

        private Socket GetRSocketById(string id)
        {
            return R_Sockets.TryGetValue(id, out var socket) ? socket : null;
        }
        private Socket GetCSocketById(string id)
        {
            return C_Sockets.TryGetValue(id, out var socket) ? socket : null;
        }
    }
}
