using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

/*
OOSD Threaded Project 2: DBClasses.Utilities
Class with util methods used to serialize and deserialize an object.
Adapted from Hayden Belanger's Lab3.
Hayden Belanger
*/
namespace DBClasses {
	public class Utilities {


		//Serializes any SERIALIZABLE object into binary into a file specified.
		public static void SerializeObject(object obj, string location) {

			try {
				string path = Path.GetDirectoryName(location);
				Stream str = File.Open(location, FileMode.OpenOrCreate);
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(str, obj);
				str.Close();
			} catch(Exception e) {
				throw e;
			}
			
		}

		//Deserializes a file (must have been serialized as type T) from location and returns a specified class instance.
		public static T DeserializeObject<T>(string location) where T : class {
			try {
				Stream str;
				try {
					str = File.Open(location, FileMode.Open);
				} catch (FileNotFoundException e) {
					throw e;
				}

				BinaryFormatter bf = new BinaryFormatter();

				T val = (T)bf.Deserialize(str);

				str.Close();
				return val;
			} catch(Exception e) {
				throw e;
			}
			

		}
	}
}
