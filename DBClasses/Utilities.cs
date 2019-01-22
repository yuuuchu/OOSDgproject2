﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

		/// <summary>
		/// Serializes the object given into a binary file at specified destination.
		/// </summary>
		/// <param name="obj">Any object that can be serialized. See <see cref="SerializableAttribute"/> for more info.</param>
		/// <param name="location">The file location you wish to serialize the given file to.</param>
		/// <exception cref="SerializationException">Thrown when the object given does not have the Serializable attribute.</exception>
		public static void SerializeObject(object obj, string location) {

			try {
				string path = Path.GetDirectoryName(location);
				Stream str = File.Open(location, FileMode.OpenOrCreate);
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(str, obj);
				str.Close();
			} catch (Exception e) {
				throw e;
			}

		}

		/// <summary>
		/// Deserializes the given file into the provided type.
		/// </summary>
		/// <typeparam name="T">The type you wish to deserialize into. This must be the identical type that the file
		/// was originally serialized as</typeparam>
		/// <param name="location">The path of the binary file that you wish to deserialize</param>
		/// <returns>Object of given type from file.</returns>
		/// <exception cref="FileNotFoundException">Thrown when the given file does not exist.</exception>
		/// <exception cref="InvalidCastException">Thrown when the file attempted to deserialize does not match type T.</exception>
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
