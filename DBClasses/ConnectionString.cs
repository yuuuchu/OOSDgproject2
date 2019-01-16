using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Project: OOSD Threaded Project 2
* Connection String class to establish connection with database
* Author: Hayden Belanger
* Date: Jan 2019
* Commenter: Eugenia Chiu
*/


namespace DBClasses {
	[Serializable] //Serializable: for writing to binary file

    //Connection String class
	public class ConnectionString {

		public static ConnectionString Connection;
		private readonly string _cs; //makes read only string

        //Make connection string, passing it a connection string parameter
		public ConnectionString(string ConnectionString) { 
			this._cs = ConnectionString;
		}

        //Returns the connection string value for use
		public string Value() {
			return _cs;
		}
	}
}
