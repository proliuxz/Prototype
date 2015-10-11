using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype0._1
{
    public static class Util
    {
        public static Double AddDoubleData(Double num1, Double num2, Int32 count)
        {
            Double result = Math.Round(num1 + num2, count);     //保留count位小数位数
            return result;
        }

        public static Double MinDoubleData(Double num1, Double num2, Int32 count)
        {
            Double result = Math.Round(num1 - num2, count);     //保留count位小数位数
            return result;
        }
    }

}