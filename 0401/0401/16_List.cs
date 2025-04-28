// 16. List.cs (Member 객체 저장)
using System;
using System.Collections.Generic;


namespace _0401
{
    internal class Member
    {
        public string Name { get; private set; }
        public string Phone { get; set; }

        public Member(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public override string ToString()
        {
            return Name + "\t" + Phone;
        }
    }
        internal class List_Sample
        {
        private List<Member> datas = new List<Member>();

        #region 1.저장 (Add, Insert)
        public void Add_test()
        {
                datas.Add(new Member("홍길동", "010-1111-1111"));
                datas.Add(new Member("김길동", "010-2222-2222"));
                datas.Add(new Member("이길동", "010-3333-3333"));
        }

        public void Insert_test()
        {
            try
            {
                // 가능 범위 : 0~ Count
                datas.Insert(0, new Member("정진영", "010-5668-4478"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        #endregion

        #region 2. 삭제(Remove, RemoveAt, Clear)
        public void Remove_test()
        {
            string select_phone = "010-2222-2222";
            Member member = datas.Find(n => n.Phone == select_phone);
            bool ret = datas.Remove(member);
            if (ret == true)
            {
                Console.WriteLine("삭ㅈ 완료");
            }
        }
        public void RemoveAt_test()
        {
            try
            {
                datas.RemoveAt(0);          // 인덱스 전달
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Clear_test()
        {
            datas.Clear();
        }

        #endregion

        #region 3. 수정(인덱서 활용)

        public void Update_test()
        {
            string update_phone = "010-3333-3333";
            Member member = datas.Find(n => n.Phone == update_phone);
            member.Phone = "010-7777-7777";
        }
        #endregion

        #region 4. 데이터 저장 여부 확인(contains)
        public void DataCehck_test()
        {
            Member member = datas[1];
            bool ret = datas.Contains(member);
            if (ret)
            {
                Console.WriteLine("저장: O");
                int idx = datas.IndexOf(member);
                Console.WriteLine("저장된 인덱스: " + idx);
            }
            else
            {
                Console.WriteLine("저장: X");
                int idx = datas.IndexOf(member);
                Console.WriteLine("저장된 인덱스: " + idx);
            }
        }
        #endregion

        #region 5. 검색 (find)
        public void Select_test()
        {
            //람다식: 함수의 기능을 표현
            /*
            n      : 매개변수       저장하ㅡㄴ 값에 따라 타입이 바뀜.
            =>     : 구분자
            n == 5 : 함수 코드 작성부
             
             */
            string update_phone = "010-3333-3333";
            Member member = datas.Find(n => n.Phone == update_phone);
            if (member == null)  //기본값
            {
                Console.WriteLine("없다");
            }
            else
                Console.WriteLine("검색결과 ", member.ToString());
        }
        #endregion
        public void Print_All()
        {
            Console.WriteLine("저장개수: {0}", datas.Count);
            for (int i = 0; i < datas.Count; i++)
            {
                Member member = (Member)datas[i];
                Console.Write(member.ToString() + " ");
            }
        }
    }
    internal class Start
    {
        static void Main(string[] args)
        {
            List_Sample sample = new List_Sample();
            sample.Add_test();
            sample.Print_All();

            sample.Insert_test();
            sample.Print_All();

            sample.DataCehck_test();

            sample.Update_test();
            sample.Print_All();

            sample.Remove_test();
            sample.Print_All();

            sample.RemoveAt_test();
            sample.Print_All();

            sample.Clear_test();
            sample.Print_All();
        }
    }
}
