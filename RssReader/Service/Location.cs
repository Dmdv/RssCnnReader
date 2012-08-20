using System.Device.Location;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RssReader.Service
{
	/// <summary>
	/// TODO: Stub, returns current geolocation.
	/// </summary>
	public class Location
	{
		public Location()
		{
			//var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
			//var latitude = watcher.Position.Location;
		}

		/// <summary>
		/// Returns current coordinates.
		/// </summary>
		public static GeoCoordinate Current { get; set; }
	}
}
