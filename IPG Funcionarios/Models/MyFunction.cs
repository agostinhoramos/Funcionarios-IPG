using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public static class MyFn
    {
        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static int ParseDbCount(decimal data)
        {
            return (int)Math.Ceiling(data);
        }

    }
}
