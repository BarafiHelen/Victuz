using Microsoft.Extensions.Logging;
using Victuz.Services;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace Victuz
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
               

            // Dependency Injection instellen
            builder.Services.AddSingleton<DatabaseService>(s =>
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Victuz.db");
                return new DatabaseService(dbPath);
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}