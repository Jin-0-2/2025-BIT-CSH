using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    // 객체를 한개만 생성한다. -> Gof 디자인 패턴 중 싱글톤 패턴
    class Sample
    {
        #region 싱글톤 패턴
        // 속성
        public static Sample singleton { get; } = null;

        // 디폴트 생성자를 은닉(private) 시킴
        private Sample() { }

        static Sample() { singleton = new Sample(); }

        public void Funtion()
        {
            Console.WriteLine("싱글톤 패턴");
        }
        #endregion
    }
    internal class Start
    {
       
        static void Main(string[] args)
        {
            Sample sample = Sample.singleton;
            sample.Funtion();
            Sample sample2 = Sample.singleton;
            sample.Equals(sample2);
        }
    }
}
