// List.cs
using System;
using System.Collections.Generic;


namespace _0401
{
    internal class List_Sample
    {
        private List<int> datas = new List<int>();

        #region 1.저장 (Add, Insert)
        public void Add_test()
        {
            for (int i = 1; i <= 5; i++)
            {
                datas.Add(i);
            }
        }

        public void Insert_test()
        {
            try
            {
                // 가능 범위 : 0~ Count
                datas.Insert(0, 10);
                datas.Insert(6, 10);
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
            bool ret = datas.Remove(3);     // 값 전달

            try
            {
                datas.RemoveAt(0);          // 인덱스 전달
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // datas.Clear();                 // 전체 삭제
        }


        #endregion

        #region 3. 수정(인덱서 활용)

        public void Update_test()
        {
            datas[0] = 10;
        }
        #endregion

        #region 4. 데이터 저장 여부 확인(contains)
        public void DataCehck_test()
        {
            int value = 50;
            bool ret = datas.Contains(5);
            if (ret)
            { 
                Console.WriteLine("저장: O"); 
                int idx = datas.IndexOf(value);
                Console.WriteLine("저장된 인덱스: " + idx);
            }
            else
            {
                Console.WriteLine("저장: X");
                int idx = datas.IndexOf(value);
                Console.WriteLine("저장된 인덱스: " + idx);
            }
        }
        #endregion

        #region 5. 검색 (find)
        public void Select_test()
        {
            //람다식: 함수의 기능을 표현
            /*
            n      : 매개변수
            =>     : 구분자
            n == 5 : 함수 코드 작성부
             
             */
            int value = datas.Find(n => n == 5);
            if (value == 0)  //기본값
            {
                Console.WriteLine("없다");
            }
            else
                Console.WriteLine("검색결과 ", value);
        }
        #endregion
        public void Print_All()
        {
            Console.WriteLine("저장개수: {0}", datas.Count);
            foreach(int i in datas)
                Console.WriteLine(i);
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
        }
    }
}
