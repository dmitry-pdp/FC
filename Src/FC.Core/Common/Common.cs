using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Core.Common
{
    public static class CommonExtensions
    {
        public static T RequireNotNull<T>(this T value, string name) where T: class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }

            return value;
        }
    }
}
