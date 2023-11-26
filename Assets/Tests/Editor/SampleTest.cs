//腳本需放在Editor資料夾內

using System.Collections.Generic;
using NUnit.Framework; //引用NUnit
using UnityEngine;

//可透過namespace來分類測試
namespace MyTest.Sample
{
    public class SampleTest
    {
        [SetUp]
        public void Setup()
        {
            Debug.Log("Setup");
        }

        [Test]
        //常見的Assert方法
        public void test1()
        {
            List<int> list1 = null;
            Assert.IsNull(list1);

            List<int> list2 = new List<int>() { 1, 2, 3 };
            Assert.IsNotNull(list2);

            int a = 1;
            int b = 2;

            Assert.IsTrue(b > a);
            Assert.IsFalse(a == 2);
            Assert.AreEqual(1, a);

            List<int> list3 = null;
            Assert.Throws(typeof(System.NullReferenceException), () =>
            {
                list3[0]++;
            });
        }

        [Test]
        //執行順序: 先執行Setup, 再執行Test
        public void test2()
        {
            Debug.Log("test2");
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        //執行順序: 每個TestCase執行前都會執行一次Setup
        public void test3(int num)
        {
            Debug.Log($"test3({num})");
        }
    }
}