using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace CliMovApp
{
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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
#if ANDROID
        Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser += (_, args) =>
        {
            var ex = args.Exception;
            System.Diagnostics.Debug.WriteLine($"[ANDROID CRASH] {ex}");
        };
#endif
            return builder.Build();
        }
    }
}
