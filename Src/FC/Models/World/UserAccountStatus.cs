using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FC.Models.World
{
    public enum UserAccountStatus : int
    {
        Active = 1,
        Banned = 2,
        Disabled = 3
    }
}