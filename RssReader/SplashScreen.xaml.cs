using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RssReader
{
	public partial class SplashScreen : UserControl
	{
		public SplashScreen()
		{
			InitializeComponent();
			var blinking = Resources["Blinking"] as Storyboard;
			if (blinking != null) blinking.Begin();
		}
	}
}