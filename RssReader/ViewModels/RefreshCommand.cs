using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Navigation;

namespace RssReader.ViewModels
{
	/// <summary>
	/// Refresh command.
	/// This could be implemented as a behaviour.
	/// </summary>
	public class RefreshCommand : ICommand
	{
		private Popup _popup;

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			var rssVm = (IUpdateVm)parameter;
			try
			{
				rssVm.LoadData(OpenPopup, ClosePopup);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception:\r\n" + ex.Message, "Application error", MessageBoxButton.OK);
				ClosePopup();
			}
		}

		public bool CanExecute(object parameter)
		{
			return parameter != null && (parameter as IUpdateVm) != null;
		}

		private void OpenPopup()
		{
			_popup = new Popup { IsOpen = true, Child = new SplashScreen() };
		}

		private void ClosePopup()
		{
			Deployment.Current.Dispatcher.BeginInvoke(() => { _popup.IsOpen = false; });
		}
	}
}