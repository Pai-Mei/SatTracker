using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Zeptomoby.OrbitTools;

namespace SpaceTrack
{
	public class SpaceTrack
	{
		class NoAuthException : Exception
		{ }

		string uriBase = "https://www.space-track.org";
		string requestController = "/basicspacedata";
		string requestAction = "/query";

		public String UserName { get; set; }
		public String Password { get; set; }

		public SpaceTrack(String UserName, String Password)
		{
			this.UserName = UserName;
			this.Password = Password;
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
			var response2 = client.UploadValues(uriBase + "/auth/login", data); 
			//if (response2.Count() > 0)
				return true;
			//else
			//	return false;
		}

		public Boolean Authentication()
		{
			using (var client = new WebClientEx())
			{
				return Auth(client);
			}
		}

		public List<Satellite> GetSatellites()
		{
			string predicateValues = "/class/tle_latest/orderby/NORAD_CAT_ID desc/format/3le/limit/10/metadata/false";
			string request = uriBase + requestController + requestAction + predicateValues;
			using (var client = new WebClientEx())
			{
					
				if (Auth(client))
				{
					var response4 = client.DownloadData(request);
					var stringData = System.Text.Encoding.Default.GetString(response4).Split('\n');
					var sats = new List<Satellite>();
					for (Int32 i = 0; i < stringData.Length - 1; i++)
					{
						try
						{
							Tle tle = new Tle(stringData[i], stringData[i + 1], stringData[i + 2]);
							Satellite sat = new Satellite(tle);
							sats.Add(sat);
						}
						catch {	}
					}
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
	} 
}