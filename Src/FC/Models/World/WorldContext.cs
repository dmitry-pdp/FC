using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FC.Models.World
{
    public class WorldContext
    {
        private WorldContext() 
        {
        }

        private static WorldContext instance;

        public static WorldContext Instance
        {
            get { return instance ?? (instance = new WorldContext()); }
        }
    }
}