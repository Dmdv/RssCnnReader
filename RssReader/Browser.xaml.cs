using System;
using System.Windows;

namespace RssReader
{
	public partial class Browser
	{
		// ReSharper disable SuggestUseVarKeywordEverywhere

		private const string Html =
			"<html><head><title>Error</title>" + 
			"</head><body><p>Failed to load the link...</p><p>The link is invalid.</p></body></html>";

		public Browser()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
		{
			string target;
			string title = "Rss title";

			var result = NavigationContext.QueryString.TryGetValue("target", out target);
			NavigationContext.QueryString.TryGetValue("title", out title);

			RoutedEventHandler browserOnLoaded = (o, args) =>
			{
				PageTitle.Text = title;

				if (result)
				{
					browser.Navigate(new Uri(target));
				}
				else
				{
					browser.NavigateToString(Html);
				}
			};

			browser.Loaded -= browserOnLoaded;
			browser.Loaded += browserOnLoaded;
		}
	}
}