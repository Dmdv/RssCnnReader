using System;

namespace RssReader.ViewModels
{
	/// <summary>
	/// Invokes updating VM.
	/// </summary>
	public interface IUpdateVm
	{
		/// <summary>
		/// Load VM data.
		/// </summary>
		/// <param name="beforeAction">Invokes before the load.</param>
		/// <param name="afterAction">Invoked after the load.</param>
		void LoadData(Action beforeAction, Action afterAction);
	}
}