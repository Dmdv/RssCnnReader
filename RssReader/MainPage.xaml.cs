using System;
using System.Windows.Controls;
using RssReader.ViewModels;

namespace RssReader
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void Listbox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			const string Msg = "/Browser.xaml?target={0}&title={1}";

			if (e.AddedItems.Count == 0) return;
			var addedItem = (RssItemVm)e.AddedItems[0];
			
			NavigationService.Navigate(new Uri(string.Format(Msg, addedItem.Link, addedItem.Title), UriKind.Relative));
		}
	}
}