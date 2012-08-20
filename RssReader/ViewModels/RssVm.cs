using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RssReader.Model;

namespace RssReader.ViewModels
{
	public class RssVm : BaseViewModel, IUpdateVm
	{
		private string _title;
		private ObservableCollection<RssItemVm> _items;

		public RssVm()
		{
			_items = new ObservableCollection<RssItemVm>();
			RefreshCommand = new RefreshCommand();

			if (IsInDesignModeStatic)
			{
				Title = "Rss Title";

				Items.Add(new RssItemVm("News 1", "Time 1", @"http://www.google.com"));
				Items.Add(new RssItemVm("News 12", "Time 12", @"http://www.google.com"));
			}
			else
			{
				RefreshCommand.Execute(this);
			}
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

		public ObservableCollection<RssItemVm> Items
		{
			get { return _items; }
			set
			{
				_items = value;
				RaisePropertyChanged("Items");
			}
		}

		public ICommand RefreshCommand { get; private set; }

		public void LoadData(Action before, Action after)
		{
			before();
			var reader = new XmlFeedReader(this);
			reader.ReadCompleted += (o, args) => after();
			reader.ReadCnnFeed();
		}
	}
}