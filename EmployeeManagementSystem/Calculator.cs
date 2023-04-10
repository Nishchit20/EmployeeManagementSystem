using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem
{
    public static class Calculator
    {
        public static double Add(double x,double y)
        {
            return x + y;
        }
        public static double Subtract(double x, double y)
        {
            return x - y;
        }
        public static double Multiply(double x, double y)
        {
            return x * y;
        }
        public static double Divide(double x, double y)
        {
            if(y != 0)
            {
                return x / y;
            }
            else
            {
                //Custom buisness logic for divide by zero
                return 0;
            }
        }
    }
}
