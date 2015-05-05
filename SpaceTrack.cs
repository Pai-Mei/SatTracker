using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Zeptomoby.OrbitTools;

using System.Xml.Serialization;

namespace SpaceTrack
{
	public class SpaceTrack
	{
		public class StatusEventArgs : EventArgs
		{
			public String Status { get; set; }

			public StatusEventArgs()
			{

			}

			public StatusEventArgs(string Status)
			{
				this.Status = Status;
			}
		}

		public class ProgressEventArgs : EventArgs
		{
			public int MaxValue { get; set; }
			public int CurrentValue { get; set; }

			public ProgressEventArgs ()
			{

			}

			public ProgressEventArgs(int Current, int Max)
			{
				MaxValue = Max;
				CurrentValue = Current;
			}
		}

		class NoAuthException : Exception
		{ }

		string uriBase = "https://www.space-track.org";
		string requestController = "/basicspacedata";
		string requestAction = "/query";

		public String UserName { get; set; }
		public String Password { get; set; }

		public List<Satellite> AllSats { get; set; }

		private readonly string SatsDataFilePath = Environment.CurrentDirectory + "\\sats.bin";

		private void SaveSats(List<Satellite> sats)
		{
			BinSerializer.BinSerializer.SerializeObject<List<Satellite>>(SatsDataFilePath, sats); 
		}

		private List<Satellite> LoadSats()
		{
			return BinSerializer.BinSerializer.DeserializeObject<List<Satellite>>(SatsDataFilePath);
		}

		public SpaceTrack(String UserName, String Password)
		{
			
			this.UserName = UserName;
			this.Password = Password;
		}

		public void Load(bool Refresh)
		{
			var CurDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (!File.Exists(CurDir + "/satdata.dat") || Refresh)
			{
				OnStatus("Загрузка данных из базы...");
				AllSats = GetSatellites();
			}
			else
			{
				if (File.Exists(SatsDataFilePath))
				{
					OnStatus("Загрузка данных из файла...");
					AllSats = LoadSats();
					if (AllSats == null)
					{
						OnStatus("Ошибка загрузки данных из файла...");
						File.Delete(SatsDataFilePath);
					}
					else
						return;
				}
				using (var sr = new StreamReader(CurDir + "\\satdata.dat"))
				{
					OnStatus("Чтение последнего запроса к базе...");
					AllSats = new List<Satellite>();
					var data = new List<string>();
					while (!sr.EndOfStream)
					{
						data.Add(sr.ReadLine()); // line 0
						sr.ReadLine();
						data.Add(sr.ReadLine()); // line 1
						sr.ReadLine();
						data.Add(sr.ReadLine()); // line 2
						sr.ReadLine();						
					}
					OnStatus("Генерация орбитальных данных...");
					for (int i = 0; i < data.Count; i += 3)
					{
						if (i > 300)
							break;
						try
						{
							Tle tle = new Tle(data[i], data[i+1], data[i+2]);
							Satellite sat = new Satellite(tle);
							AllSats.Add(sat);
							OnProgress(new ProgressEventArgs(i, data.Count));
						}
						catch
						{

						}
					}
					OnStatus("Сохранение данных в файл...");
					SaveSats(AllSats);

				}
			}
			OnStatus("Загрузка завершена!");
		}

		public class WebClientEx : WebClient
		{
			// Create the container to hold all Cookie objects
			private CookieContainer _cookieContainer = new CookieContainer();

			// Override the WebRequest method so we can store the cookie 
			// container as an attribute of the Web Request object
			protected override WebRequest GetWebRequest(Uri address)
			{
				WebRequest request = base.GetWebRequest(address);

				if (request is HttpWebRequest)
					(request as HttpWebRequest).CookieContainer = _cookieContainer;

				return request;
			}
		}   // END WebClient Class

		private Boolean Auth(WebClient client)
		{
			// Store the user authentication information
			var data = new NameValueCollection
                {
                    { "identity", UserName },
                    { "password", Password },
                };

			// Generate the URL for the API Query and return the response
			try
			{
				var response2 = client.UploadValues(uriBase + "/auth/login", data);
				string request = "https://www.space-track.org/basicspacedata/query/class/tle_latest/NORAD_CAT_ID/01317/orderby/NORAD_CAT_ID asc/limit/1/format/3le/metadata/false";
				var response4 = client.DownloadData(request);
				if (response4.Count() > 0)
					return true;
				else
					return false;
			}
			catch
			{
				return false;
			}
		}

		public Boolean Authentication()
		{
			OnStatus("Авторизация...");
			using (var client = new WebClientEx())
			{
				return Auth(client);
			}
		}

		private List<Satellite> GetSatellites(bool Cache = true)
		{
			var CurDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			string predicateValues = "/class/tle_latest/ORDINAL/1/EPOCH/%3Enow-30/orderby/NORAD_CAT_ID%20desc/format/3le";
			string request = uriBase + requestController + requestAction + predicateValues;
			using (var client = new WebClientEx())
			{
				if (Auth(client))
				{
					var response4 = client.DownloadData(request);
					var stringData = System.Text.Encoding.Default.GetString(response4).Split('\n');
					using (var sw = new StreamWriter(CurDir + "/lastrequest.dat"))
					{
						sw.Write(System.Text.Encoding.Default.GetString(response4));
					}
					var sats = new List<Satellite>();
					
					using (var sw = new StreamWriter(CurDir + "/satdata.dat"))
					{
						for (Int32 i = 0; i < stringData.Length - 1; i+=3)
						{
							try
							{
								if (stringData[i].Contains("DEB"))
									continue;
								if (sats.Count > 100)
									break;
								Tle tle = new Tle(stringData[i], stringData[i + 1], stringData[i + 2]);
								sw.WriteLine(stringData[i]);
								sw.WriteLine(stringData[i+1]);
								sw.WriteLine(stringData[i+2]);
								Satellite sat = new Satellite(tle);
								sats.Add(sat);
								OnProgress(new ProgressEventArgs(i, stringData.Length - 1));
							}
							catch { }
						}
					}
					SaveSats(sats);
					return (sats);
					
				}
				else
				{
					throw new NoAuthException();
				}
			}
		}

		public List<Satellite> GetSatellites(string LanuchYaer)
		{

			string predicateValues = "/class/tle_latest/LAUNCH_YEAR/=" + LanuchYaer + "/orderby/INTLDES asc/metadata/false";
			string request = uriBase + requestController + requestAction + predicateValues;

			// Create new WebClient object to communicate with the service
			using (var client = new WebClientEx())
			{
				if (Auth(client))
				{
					var response4 = client.DownloadData(request);
					var stringData = (System.Text.Encoding.Default.GetString(response4)).Split('\n');
					List<Satellite> result = new List<Satellite>();
					for (Int32 i = 0; i < stringData.Length - 1; i += 3)
					{
						Tle tle = new Tle(stringData[i], stringData[i + 1], stringData[i + 2]);
						Satellite sat = new Satellite(tle);
						result.Add(sat);
						OnProgress(new ProgressEventArgs(i, stringData.Length - 1));
					}
					return result;
				}
				else
				{
					throw new NoAuthException();
				}
			}
		}

		public List<Satellite> GetSatellites(string[] Norad)
		{

			string predicateValues = "/class/tle_latest/ORDINAL/1/NORAD_CAT_ID/" + string.Join(",", Norad) + "/orderby/NORAD_CAT_ID%20ASC/format/3le";
			string request = uriBase + requestController + requestAction + predicateValues;

			// Create new WebClient object to communicate with the service
			using (var client = new WebClientEx())
			{
				if (Auth(client))
				{
					var response4 = client.DownloadData(request);
					var stringData = (System.Text.Encoding.Default.GetString(response4)).Split('\n');
					List<Satellite> result = new List<Satellite>();
					for (Int32 i = 0; i < stringData.Length - 1; i += 3)
					{
						Tle tle = new Tle(stringData[i], stringData[i + 1], stringData[i + 2]);
						Satellite sat = new Satellite(tle);
						result.Add(sat);
						OnProgress(new ProgressEventArgs(i, stringData.Length - 1));
					}
					return result;
				}
				else
				{
					throw new NoAuthException();
				}
			}
		}
		
		public string GetSpaceTrack(string[] norad)
		{
			string predicateValues = "/class/tle_latest/ORDINAL/1/NORAD_CAT_ID/" + string.Join(",", norad) + "/orderby/NORAD_CAT_ID%20ASC/format/3le";
			string request = uriBase + requestController + requestAction + predicateValues;

			using (var client = new WebClientEx())
			{
				if (Auth(client))
				{
					var response4 = client.DownloadData(request);
					return (System.Text.Encoding.Default.GetString(response4));
				}
				else
				{
					throw new NoAuthException();
				}
			}
		}
				
		public string GetSpaceTrack(string[] norad, DateTime dtstart, DateTime dtend)
		{
			string predicateValues = "/class/tle/EPOCH/" + dtstart.ToString("yyyy-MM-dd--") + dtend.ToString("yyyy-MM-dd") + "/NORAD_CAT_ID/" + string.Join(",", norad) + "/orderby/NORAD_CAT_ID%20ASC/format/3le";
			string request = uriBase + requestController + requestAction + predicateValues;

			// Create new WebClient object to communicate with the service
			using (var client = new WebClientEx())
			{
				if (Auth(client))
				{
					var response4 = client.DownloadData(request);
					return (System.Text.Encoding.Default.GetString(response4));
				}
				else
				{
					throw new NoAuthException();
				}
			}
		}

		public List<Satellite> GetOrbitData(string[] norad)
		{
			string predicateValues = "/class/omm/NORAD_CAT_ID/" + norad + "/orderby/NORAD_CAT_ID%20ASC/limit/1";
			string request = uriBase + requestController + requestAction + predicateValues;

			using (var client = new WebClientEx())
			{
				if (Auth(client))
				{
					var response4 = client.DownloadData(request);
					var str = System.Text.Encoding.Default.GetString(response4).Split('\n');
					for (Int32 i = str.Length - 4; i < str.Length - 1; i++)
					{
						
					}
					return null;
				}
				else
				{
					throw new NoAuthException();
				}
			}
		}

		public event EventHandler<ProgressEventArgs> Progress;

		public event EventHandler<StatusEventArgs> Status; 

		protected virtual void OnProgress(ProgressEventArgs args)
		{
			if (Progress != null)
				Progress(this, args);
		}

		protected virtual void OnStatus(StatusEventArgs args)
		{
			if (Status != null)
				Status(this, args);
		}

		protected virtual void OnStatus(String status)
		{
			if (Status != null)
				Status(this, new StatusEventArgs(status));
		}
	} 
}