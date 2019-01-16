using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Project: OOSD Threaded Project 2
* Connection String class to establish connection
* Author: Hayden Belanger
* Date: Jan 2019
* Commenter: Eugenia Chiu
*/

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
