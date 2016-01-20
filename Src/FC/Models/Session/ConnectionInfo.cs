using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FC.Models.Session
{
    public class ConnectionInfo
    {
        public ConnectionInfo(Guid connectionId)
        {
            this.ConnectionId = connectionId;
        }

        public Guid ConnectionId { get; private set; }

        public Guid AccountId { get; set; }
    }
}