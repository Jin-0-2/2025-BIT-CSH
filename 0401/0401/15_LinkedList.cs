// List.cs
using System;
using System.Collections.Generic;


namespace _0401
{
    internal class LinkedList_Sample
    {
        private LinkedList<int> datas = new LinkedList<int>();

        #region 1.저장 (Add, Insert)
        public void Add_test()
        {
            for (int i = 1; i <= 5; i++)
            {
                datas.AddLast(i);
                datas.AddFirst(i);
            }
        }

        public void Insert_test()
        {
            try
            {
                LinkedListNode<int> node = datas.First;     // vector의 begin
                datas.AddBefore(node.Next.Next.Next.Next, 10);                  // node로 접근
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
            bool ret = datas.Remove(3);     // 값 전달

            try
            {
                datas.RemoveFirst();          // 인덱스 전달
                datas.RemoveLast();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            datas.Clear();                 // 전체 삭제
        }


        #endregion

        #region 3. 수정(인덱서 활용)

        public void Update_test()
        {
            LinkedListNode<int> node = datas.First.Next.Next;
            node.Value = 300;
        }
        #endregion

        #region 4. 데이터 저장 여부 확인(contains)
        public void DataCehck_test()
        {
            bool ret = datas.Contains(5);
            if (ret)
            {
                Console.WriteLine("저장: O");
            }
            else
            {
                Console.WriteLine("저장: X");
            }
        }
        #endregion

        #region 5. 검색 (find)
        public void Select_test()
        {
            // List와 조금 다름
            // LinkedListNode로 받아옴.
            LinkedListNode<int> node = datas.Find(5);
            if (node == null)  //기본값
            {
                Console.WriteLine("없다");
            }
            else
                Console.WriteLine("검색결과 ", node.Value);
        }
        #endregion
        public void Print_All()
        {
            Console.WriteLine("저장개수: {0}", datas.Count);
            foreach (int i in datas)
                Console.Write(i + " ");
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

            sample.DataCehck_test();

            sample.Update_test();
            sample.Print_All();

            sample.Remove_test();
            sample.Print_All();
        }
    }
}
