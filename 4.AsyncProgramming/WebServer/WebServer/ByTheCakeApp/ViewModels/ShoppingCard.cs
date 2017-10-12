using System.Collections.Generic;

namespace WebServer.ByTheCakeApp.Models
{
    public class ShoppingCard
    {
        public const string SessionKey = "^%Current_Shopping_Card^%";
        
        public List<int> ProductIds { get; private set; } = new List<int>();
    }
}
