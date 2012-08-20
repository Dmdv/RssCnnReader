using System;
using System.Collections.Generic;

namespace RssReader.Service
{
	/// <summary>
	/// Time zone info.
	/// </summary>
	public class TimeZoneInfo
	{
		private readonly string _displayName;
		private readonly string _standardDisplayName;
		private static Dictionary<string, TimeZoneInfo> _zones;

		public TimeZoneInfo(string displayName, string standardDisplayName)
		{
			// Info should be taken from "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"
			_displayName = displayName;
			_standardDisplayName = standardDisplayName;
			Id = _displayName;
		}

		/// <summary>
		/// Gets the time zone identifier.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Gets the localized general display name that represents the time zone.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName ?? string.Empty; }
		}

		/// <returns>
		/// The localized display name of the time zone's standard time.
		/// </returns>
		public string StandardName
		{
			get { return _standardDisplayName ?? string.Empty; }
		}

		/// <summary>
		/// Gets the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).
		/// </summary>
		public TimeSpan BaseUtcOffset { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the time zone has any daylight saving time rules.
		/// </summary>
		public bool SupportsDaylightSavingTime { get; private set; }

		private static Dictionary<string, TimeZoneInfo> Zones
		{
			get { return _zones ?? (_zones = new Dictionary<string, TimeZoneInfo>()); }
		}

		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (Zones.Count == 0)
			{
				InitTimeZones();
			}

			TimeZoneInfo timeZoneInfo;
			Zones.TryGetValue(id, out timeZoneInfo);
			return timeZoneInfo;
		}

		private static void InitTimeZones()
		{
			var timeZoneInfo = new TimeZoneInfo("Eastern Standard Time", "Восточное время США (зима)")
			{
			    BaseUtcOffset = new TimeSpan(-5, 0, 0),
			    SupportsDaylightSavingTime = true
			};
			Zones.Add("Eastern Standard Time", timeZoneInfo);
		}

		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo timeZoneInfo)
		{
			return dateTime.Add(-timeZoneInfo.BaseUtcOffset);
		}
	}
}