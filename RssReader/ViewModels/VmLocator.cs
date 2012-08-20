namespace RssReader.ViewModels
{
	/// <summary>
	/// View model locator.
	/// </summary>
	public class VmLocator
	{
		private static RssVm _rssVm;
		private static RssItemVm _rssItemVm;

		/// <summary>
		/// Rss item view model.
		/// </summary>
		public static RssItemVm RssItemVm
		{
			get { return _rssItemVm ?? (_rssItemVm = new RssItemVm()); }
		}

		/// <summary>
		/// Rss view model.
		/// </summary>
		public static RssVm RssVm
		{
			get { return _rssVm ?? (_rssVm = new RssVm()); }
		}
	}
}