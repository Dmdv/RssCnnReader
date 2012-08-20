using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using RssReader.ViewModels;
using TimeZoneInfo = RssReader.Service.TimeZoneInfo;

namespace RssReader.Model
{
	public class XmlFeedReader
	{
		private const string Uri = @"http://rss.cnn.com/rss/cnn_topstories.rss";
		private readonly RssVm _rssVm;

		public event EventHandler<FeedReaderArgs> ReadCompleted;

		public XmlFeedReader(RssVm rssVm)
		{
			_rssVm = rssVm;
		}

		public void ReadCnnFeed()
		{
			var client = new WebClient();
			client.OpenReadCompleted += WebClientRequestCompleted;
			client.OpenReadAsync(new Uri(Uri));
		}

		private void OnReadCompleted(FeedReaderArgs e)
		{
			var handler = ReadCompleted;
			if (handler != null) handler(this, e);
		}

		private void WebClientRequestCompleted(object sender, OpenReadCompletedEventArgs e)
		{
			ReadStreamInternal(e);
			OnReadCompleted(null);
		}

		private void ReadStreamInternal(OpenReadCompletedEventArgs e)
		{
			var stream = e.Result;
			using (stream)
			{
				var doc = CreateXDoc(stream);
				InitRssTitle(doc);
				InitRssItems(doc);
			}
		}

		private static XDocument CreateXDoc(Stream stream)
		{
			XDocument doc;
			try
			{
				doc = XDocument.Load(stream);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to load network stream", ex);
			}
			return doc;
		}

		private void InitRssTitle(XContainer doc)
		{
			try
			{
				_rssVm.Title = doc.Descendants("title").First().Value;
			}
			catch (Exception ex)
			{
				throw new XmlException("Xml schema has changed: missing '/rss/channel/title' node", ex);
			}
		}

		private void InitRssItems(XContainer doc)
		{
			var items = doc.Descendants(@"item");
			foreach (var item in items)
			{
				var title = item.Element("title");
				var date = item.Element("pubDate");
				var link = item.Element("link");

				if (title == null)
				{
					throw new XmlException("Xml schema has changed: missing '//item/title' node");
				}
				if (date == null)
				{
					throw new XmlException("Xml schema has changed: missing '//item/pubDate' node");
				}
				if (link == null)
				{
					throw new XmlException("Xml schema has changed: missing '//item/link' node");
				}

				_rssVm.Items.Add(new RssItemVm(title.Value, ParseTime(date), link.Value));
			}
		}

		private static string ParseTime(XElement date)
		{
			var est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			var dateTime = DateTime.Parse(date.Value.Replace("EDT", string.Empty).Trim(), CultureInfo.InvariantCulture);
			var localTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, est).ToLocalTime();
			var time = string.Format("{0} {1}", localTime.ToLongDateString(), localTime.ToLongTimeString());
			return time;
		}
	}
}