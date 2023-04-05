using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EmployeeManagementSystem;
using System.IO;

namespace EmployeeManagementSystemTests
{
    public class Example_Tests
    {
        [Fact]
        public void ExampleLoadTextFile_ValidNameShouldWork()
        {
            string actual = Example.ExampleLoadTextFile("This is a valid file name");

            Assert.True(actual.Length > 0);
        }
        [Fact]
        public void ExampleLoadTextFile_InValidNameShouldFail()
        {
            Assert.Throws<ArgumentException>("file", ()=>Example.ExampleLoadTextFile(""));
        }
    }
}
