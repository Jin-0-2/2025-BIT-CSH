// LinkedList.cs (Member)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    internal class LinkedList_Sample
    {
        private LinkedList<Member> datas = new LinkedList<Member>();

        #region 1.저장 (Add, Insert)
        public void Add_test()
        {
            for (int i = 1; i <= 5; i++)
            {
                datas.AddLast(new Member("홍길동", "010-1111-1111"));
                datas.AddFirst(new Member("김길동", "010-2222-2222"));
                
            }
        }

        public void Insert_test()
        {
            try
            {
                LinkedListNode<Member> node = datas.First;                                          // vector의 begin
                datas.AddBefore(node.Next, new Member("고길공", "010-3333-3333"));                  // node로 접근
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        #endregion

        #region 2. 삭제(Remove, RemoveFirst, RemoveLast, Clear)
        public void Remove_test()
        {
            LinkedListNode<Member> member = datas.First;                   // 멤버를 찾아야하는데 멤버를 매개변수로 넣으라는 건 뭔소리노
            // bool ret = datas.Remove(member);    
        }

        public void RemoveFirst_Last_test()
        {
            try
            {
                datas.RemoveFirst();
                datas.RemoveLast();
                // datas.RemoveAt();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveClear_test()
        {
            datas.Clear();
        }

        #endregion

        #region 3. 수정(인덱서 활용)

        public void Update_test()
        {
            LinkedListNode<Member> node = datas.First.Next.Next;
            Member member = node.Value;
            member.Phone = "010-6666-6666";
        }
        #endregion

        #region 4. 데이터 저장 여부 확인(contains)
        public void DataCehck_test()
        {
            //bool ret = datas.Contains();
            //if (ret)
            //{
            //    Console.WriteLine("저장: O");
            //}
            //else
            //{
            //    Console.WriteLine("저장: X");
            //}
        }
        #endregion

        #region 5. 검색 (find)
        private Member FindName(string name)
        {
            foreach (Member member in datas)
            {
                if (member.Name == name)
                    return member;
            }
            return null;
        }
        public void Select_test()
        {
            // List와 조금 다름
            // LinkedListNode로 받아옴.
            string name = "홍길동";
            Member member = FindName(name);
            LinkedListNode<Member> node = datas.Find(member);

            // Name이 "Bob"인 멤버 찾기 ??
            Member foundMember = datas.FirstOrDefault(m => m.Name == name);

            if (node == null)  //기본값
            {
                Console.WriteLine("없다");
            }
            else
                Console.WriteLine("검색결과 ", foundMember.ToString());
        }
        #endregion
        public void Print_All()
        {
            Console.WriteLine("저장개수: {0}", datas.Count);
            foreach (Member member in datas)
                Console.Write(member.ToString() + " ");
            Console.WriteLine();
        }
    }

    internal class Start
    {
        static void Main(string[] args)
        {
            LinkedList_Sample sample = new LinkedList_Sample();
            sample.Add_test();
            sample.Print_All();

            sample.Insert_test();
            sample.Print_All();

            sample.Select_test();

            //sample.Update_test();
            //sample.Print_All();

            //sample.Remove_test();
            //sample.Print_All();
        }
    }
}
