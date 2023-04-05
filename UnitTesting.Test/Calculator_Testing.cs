using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting.Test
{
    public class Calculator
    {
        public int Add(int num1, int num2)
        {
            return num1 + num2;
        }
    }
    [TestClass]
    public class Calculator_Testing
    {
        [TestMethod]
        public void TestMethod1()
        {
                Calculator obj = new Calculator();
                int num1 = 3, num2 = 8;
                var result = obj.Add(num1, num2);
                Assert.AreEqual(num1 + num2, result);
        }
    }
}
