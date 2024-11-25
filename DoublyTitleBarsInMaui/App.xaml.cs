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

			if (this.Windows.Count >= 1) {
				this.Windows[0].Page = page;
				return base.CreateWindow(activationState);
			}

			return new(page);
		}
	}
}
