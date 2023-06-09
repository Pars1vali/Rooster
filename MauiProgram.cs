using CommunityToolkit.Maui;
using MauiToolkitPopupSample;
using MetroLog.MicrosoftExtensions;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace Rooster;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Montserrat-Medium.ttf", "MontserratMedium");
            });

		builder.Services.AddSingleton(AudioManager.Current);
		builder.Services.AddTransient<MainPage>();

		builder.Logging.AddTraceLogger(_ => { });
		builder.Services.AddTransient<MainPage>();
        return builder.Build();
	}
}
