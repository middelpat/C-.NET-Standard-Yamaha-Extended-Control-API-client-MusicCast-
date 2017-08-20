using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamahaReceiverTest
{
    public class Helpers
    {
        public static T[] GetEnumValues<T>()
        {
            return ((T[])Enum.GetValues(typeof(T)));
        }
    }
}
