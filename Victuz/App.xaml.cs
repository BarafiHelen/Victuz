using Victuz.Services;
using System.IO;
using Victuz;
using Victuz.Views;

namespace Victuz;

public partial class App : Application
{
    public static DatabaseService Database { get; private set; }

    public App()
    {
        InitializeComponent();

        // SQLite databasepad instellen
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Victuz.db3");
        Database = new DatabaseService(dbPath);

        MainPage = new NavigationPage(new ApiView());
    }
}