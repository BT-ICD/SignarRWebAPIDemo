using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Model
{
    public class HubUserData
    {
        public string UserName { get; set; }
        public int Status { get; set; }
        public DateTime UpdatedOn { get; set; }

        public static List<HubUserData> list= new List<HubUserData>();
        public static Dictionary<string, HubUserData> connectedList= new Dictionary<string, HubUserData>();
        public readonly static ConnectionMapping _connections =
        new ConnectionMapping();
    }
}
