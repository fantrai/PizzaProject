using Microsoft.Extensions.Logging;

namespace Pizza
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Caveat-Regular.ttf", "Caveat-Regular");
                    fonts.AddFont("BadScript.ttf", "ofont");
                    fonts.AddFont("Inter-Regular.ttf", "Inter");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
    }
}
