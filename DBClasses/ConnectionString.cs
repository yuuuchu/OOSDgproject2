using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedProject2 
{
    [Serializable]
    public class ConnectionString
    {

        public static ConnectionString Connection;

        private readonly string _cs;

        public ConnectionString(string ConnectionString)
        {

            this._cs = ConnectionString;
        }

        public string Value()
        {
            return _cs;
        }
    }
}



