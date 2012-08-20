using System;
using RssReader.ViewModels;

namespace RssReader.Model
{
	public class FeedReaderArgs : EventArgs
	{
		public FeedReaderArgs()
		{
		}

		public FeedReaderArgs(RssVm rssVm)
		{
			RssVm = rssVm;
		}

		public RssVm RssVm { get; private set; }
	}
}