using Foundation;
using Avalonia;
using Avalonia.iOS;
using Avalonia.ReactiveUI;
using CassetteBuilds.Code.Logic;
using CassetteBuilds.iOS.Code;

namespace CassetteBuilds.iOS;

// The UIApplicationDelegate for the application. This class is responsible for launching the
// User Interface of the application, as well as listening (and optionally responding) to
// application events from iOS.
[Register("AppDelegate")]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public partial class AppDelegate : AvaloniaAppDelegate<App>
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
	protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
	{
		Features.UrlOpener = new IOSUrlOpener();

		return base.CustomizeAppBuilder(builder)
			.WithInterFont()
			.UseReactiveUI();
	}
}
