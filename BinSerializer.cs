using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Zeptomoby.OrbitTools;

namespace BinSerializer
{
	public static class BinSerializer
	{
		public static void SerializeObject<T>(string fileName, T objToSerialize, HeaderHandler handler = null)
		{
			try
			{
				using (FileStream fstream = File.Open(fileName, FileMode.Create))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					if(handler == null)
						binaryFormatter.Serialize(fstream, objToSerialize);
				}
			}
			catch
			{
			}
		}

		public static T DeserializeObject<T>(string fileName, HeaderHandler handler = null)
		{
			try
			{
				T objToSerialize = default(T);
				using (FileStream fstream = File.Open(fileName, FileMode.Open))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					object obj = null;
					if(handler == null)
						obj = (T)binaryFormatter.Deserialize(fstream);
					else
						obj = (T)binaryFormatter.Deserialize(fstream, handler);
					if (obj is T)
						objToSerialize = (T)obj;
				}
				return objToSerialize;
			}
			catch
			{
				return default(T);
			}
		}

	}
}
