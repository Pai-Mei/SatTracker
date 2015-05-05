using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Zeptomoby.OrbitTools;

namespace BinSerializer
{
	public class BinSerializer
	{
		public BinSerializer()
		{

		}

		public delegate void ProgressChangeCallback(object sender, SpaceTrack.SpaceTrack.ProgressEventArgs args);

		public void SerializeObject<T>(string fileName, T objToSerialize)
		{
			try
			{
				using (FileStream fstream = File.Open(fileName, FileMode.Create))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(fstream, objToSerialize);
				}
			}
			catch
			{
			}
		}

		public T DeserializeObject<T>(string fileName, ProgressChangeCallback callback = null)
		{
			try
			{
				T objToSerialize = default(T);
				using (FileStream fstream = File.Open(fileName, FileMode.Open))
				{
					Timer timer = null;
					if (callback != null)
					{
						timer = new Timer(delegate
						{
							callback(null, new SpaceTrack.SpaceTrack.ProgressEventArgs((int)(100*fstream.Position/fstream.Length), 100));
						},
						null, 0, 200);
					}

					BinaryFormatter binaryFormatter = new BinaryFormatter();
					var obj = (T)binaryFormatter.Deserialize(fstream);
					if (obj is T)
						objToSerialize = (T)obj;
					if (timer != null) timer.Dispose();

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
