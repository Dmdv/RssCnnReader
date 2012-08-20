using System.ComponentModel;

namespace RssReader.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private static bool? _isInDesignMode;

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Gets a value indicating whether the control is in design mode (running in Blend
		/// or Visual Studio).
		/// </summary>
		protected static bool IsInDesignModeStatic
		{
			get
			{
				if (!_isInDesignMode.HasValue)
				{
#if SILVERLIGHT
					_isInDesignMode = DesignerProperties.IsInDesignTool;
#else
			var prop = DesignerProperties.IsInDesignModeProperty;
			_isInDesignMode
				= (bool)DependencyPropertyDescriptor
				.FromProperty(prop, typeof(FrameworkElement))
				.Metadata.DefaultValue;
#endif
				}

				return _isInDesignMode.Value;
			}
		}

		protected void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}