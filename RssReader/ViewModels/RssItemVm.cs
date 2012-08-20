namespace RssReader.ViewModels
{
	public class RssItemVm : BaseViewModel
	{
		private string _time;
		private string _title;
		private string _link;

		public RssItemVm()
		{
			if (IsInDesignModeStatic)
			{
				Title = "Rss item title";
				Time = "Current time";
			}
		}

		public RssItemVm(string title, string time, string link)
		{
			Title = title;
			Time = time;
			Link = link;
		}

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				RaisePropertyChanged("Title");
			}
		}

		public string Time
		{
			get { return _time; }
			set
			{
				_time = value;
				RaisePropertyChanged("Time");
			}
		}

		public string Link
		{
			get { return _link; }
			set
			{
				_link = value;
				RaisePropertyChanged("Link");
			}
		}
	}
}