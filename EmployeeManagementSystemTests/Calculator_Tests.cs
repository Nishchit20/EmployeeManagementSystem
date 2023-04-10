using EmployeeManagementSystem;
using System;
using Xunit;

namespace EmployeeManagementSystemTests
{
    public class Calculator_Tests
    {
        [Theory]
        [InlineData(4,3,7)]
        [InlineData(4.10, 13.25, 17.35)]
        [InlineData(double.MaxValue, 5, double.MaxValue)]
        public void Add_Result(double x, double y, double expected)
        {
            double actual = Calculator.Add(x, y);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(8,4,2)]
        public void Divide_Result(double x, double y, double expected)
        {
            //Arrange

            //Act
            double actual = Calculator.Divide(x, y);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Divide_ResultDivideByZero()
        {
            //Arrange
            double expected = 0;

            //Act
            double actual = Calculator.Divide(15, 0);

            //Assert
            Assert.Equal(expected, actual);
        }

        //[Fact]
        //public void Add_Result()
        //{
        //    //Arrange
        //    double expected = 5;
        //Actual
        //    double actual = Calculator.Add(3, 2);
        //Assert
        //    Assert.Equal(expected, actual);
        //}
        //[Fact]
        //public void PassingTest()
        //{
        //    Assert.Equal(4, Add(2, 2));
        //}

        //[Fact]
        //public void FailingTest()
        //{
        //    Assert.Equal(5, Add(2, 2));
        //}

        //int Add(int x, int y)
        //{
        //    return x + y;
        //}


        //[Theory]
        //[InlineData(3)]
        //[InlineData(5)]
        //[InlineData(6)]
        //public void MyFirstTheory(int value)
        //{
        //    Assert.True(IsOdd(value));
        //}

        //bool IsOdd(int value)
        //{
        //    return value % 2 == 1;
        //}
    }
}
