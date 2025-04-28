// wbarray.cs
using System;

namespace WSBit41JJY.Lib
{
    internal class WbArray
    {
        #region 1. 멤버 필드 및 프로퍼티, 배열의 인덱서
        private object[] arr;                                  // 동적 배열의 주소 저장(저장소)	
        public object[] Arr { get { return arr; } }            // 외부에서 배열을 읽을 수 있도록 속성 구현

        public object this[int idx]
        {
            get
            {
                if (idx < 0 || idx >= Size)
                    throw new Exception("범위를 벗어났다.");   // 외부에서 배열 요소를 읽을 수 있도록 인덱서 구현
                return arr[idx];
            }
        }

        public int Max { get; private set; }                   // 최대 저장개수
        public int Size { get; private set; }                  // 현재 저장된 데이터 개수(저장 위치)
        #endregion

        #region 2. 생성자
        public WbArray(int _max = 10)
        {
            Max = _max;
            Size = 0;
            arr = new object[Max];
        }
        #endregion

        #region 3. 기능 메서드
        public void Add(object value)
        {
            if (Size >= Max)
                throw new Exception("Overflow - 저장 공간 부족");
            arr[Size] = value;
            Size++;
        }

        public void Remove(int idx)
        {
            if (idx < 0 || idx >= Size)                 // 0보다 작거나 size보다 크다면
                throw new Exception("범위를 벗어났다.");
            for (int i = idx; i < Size - 1; i++)
                arr[i] = arr[i + 1];
            Size--;
        }

        #endregion
    }
}
