using System;
using System.Collections.Generic;
using WebServer.Server.Common;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.Server.HTTP
{
    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> values;

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this.Id = id;
            this.values = new Dictionary<string, object>();
        }

        public string Id { get; private set; }

        public void Add(string key, object value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            //CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.values[key] = value;
        }

        public void Clear() => this.values.Clear();

        public object Get(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.values.ContainsKey(key))
            {
                //throw new InvalidOperationException($"The given key {key} is not present in the Session Collection");
                return null;
            }

            return this.values[key];
        }

        public T Get<T>(string key)
        {
            return (T)this.Get(key);
        }
    }
}
