using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public static class MyFn
    {
        public static bool IsNumericType(this string o)
        {
            foreach (char c in o)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static int ParseDbCount(decimal data)
        {
            return (int)Math.Ceiling(data);
        }

    }
}
