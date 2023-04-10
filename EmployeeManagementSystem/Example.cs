using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public static class Example
    {
        public static string ExampleLoadTextFile(string file)
        {
            if(file.Length < 10)
            {
                throw new ArgumentException("The file name was too short", "file");
                //throw new System.IO.FileNotFoundException();
            }
            return "The file was not correctly loaded";
        }
    }
}
