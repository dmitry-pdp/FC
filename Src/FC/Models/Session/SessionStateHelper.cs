using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FC.Models.Session
{
    public static class SessionStateHelper
    {
        const string ClientDeviceInfoSessionId = "SessionState.ClientDeviceInfoSessionId";

        public static ClientDeviceInfo GetClientDeviceInfo(this HttpSessionStateBase session)
        {
            return session[ClientDeviceInfoSessionId] as ClientDeviceInfo;
        }

        public static void SetClientDeviceInfo(this HttpSessionStateBase session, ClientDeviceInfo clientDeviceInfo)
        {
            session[ClientDeviceInfoSessionId] = clientDeviceInfo;
        }

        const string ConnectionInfoSessionId = "SessionState.ConnectionInfoSessionId";

        public static ConnectionInfo GetConnectionInfo(this HttpSessionStateBase session)
        {
            return session[ConnectionInfoSessionId] as ConnectionInfo;
        }

        public static void SetConnectionInfo(this HttpSessionStateBase session, ConnectionInfo connectionInfo)
        {
            session[ConnectionInfoSessionId] = connectionInfo;
        }
    }
}