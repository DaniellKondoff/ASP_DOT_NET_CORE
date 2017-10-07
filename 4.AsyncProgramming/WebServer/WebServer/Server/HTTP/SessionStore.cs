using System;
using System.Collections.Concurrent;
using System.Text;

namespace WebServer.Server.HTTP
{
    public static class SessionStore
    {
       public const string SessionCookieKey = "MY_SID";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions 
            = new ConcurrentDictionary<string, HttpSession>();

        
        public static HttpSession Get(string id)
        {
           return sessions.GetOrAdd(id, _ => new HttpSession(id));
        }
    }
}
