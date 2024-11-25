#if WINDOWS
using System.Reflection;
using Microsoft.Maui.Platform;
#endif

namespace DoublyTitleBarsInMaui
{
	public partial class App : Application
	{
		public App()
		{
			this.InitializeComponent();

			//this.MainPage = new AppShell();
		}

		protected override Window CreateWindow(IActivationState? activationState)
		{
#if WINDOWS && WORKAROUND_WITH_REFLECTION
			if (activationState?.Context.Services.GetRequiredService<NavigationRootManager>()                                    is not null and var nrm &&
				nrm.GetType().GetMethod("SetTitleBarVisibility", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(bool)]) is not null and var mi) {
				mi.Invoke(nrm, [ false ]);
			}
#endif // WORKAROUND_WITH_REFLECTION

			var page = new AppShell();

			Window wnd;

			if (this.Windows.Count >= 1) {
				this.Windows[0].Page = page;
				wnd = base.CreateWindow(activationState);
			} else {
				wnd = new(page);
			}

#if false // This does not work.
			var titleBar = new TitleBar();
			titleBar.IsVisible = false;
			wnd.TitleBar = titleBar;
#endif

#if false // This does not work.
			if (wnd.TitleBar is TitleBar titleBar) {
				titleBar.IsVisible = false;
			}
#endif

			return wnd;
		}
	}
}
