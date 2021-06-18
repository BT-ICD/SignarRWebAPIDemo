using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Model
{
    public class ConnectionMapping
    {
        private readonly Dictionary<String, HashSet<string>> _connections =
            new Dictionary<String, HashSet<string>>();
        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }
        public void Add(String key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if(!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key.ToUpper(), connections);
                }
                lock (_connections)
                {
                    connections.Add(connectionId);
                }
            }
        }
        public IEnumerable<string> GetConnections(String key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key.ToUpper(), out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }
        public void Remove(String key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key.ToUpper(), out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
        public Dictionary<string, HashSet<string>>.KeyCollection GetKeys()
        {
            var result = _connections.Keys;
            return result;
        }
    }
}
