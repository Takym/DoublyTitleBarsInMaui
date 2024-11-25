using Microsoft.Extensions.Logging;

#if WINDOWS
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif

namespace DoublyTitleBarsInMaui
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts => {
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

#if WINDOWS
			builder.ConfigureLifecycleEvents(
				builder => builder.AddWindows(
					builder => builder.OnWindowCreated(window => {
						window.AppWindow.TitleBar.ExtendsContentIntoTitleBar = false;

#if WORKAROUND_BY_OVERLAPPING
						// Workaround by overlapping:
						if (window.Content is FrameworkElement elem) {
							elem.Margin = new(0, -32, 0, 0);
							Canvas.SetZIndex(elem, 0);
						}
#endif // WORKAROUND_BY_OVERLAPPING
					})
				)
			);
#endif // WINDOWS

			return builder.Build();
		}
	}
}
