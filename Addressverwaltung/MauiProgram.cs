using Adressverwaltung;
using Adressverwaltung.Data;

public static class MauiProgram
{
    public static Database Database { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        string dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "addresses.db3");

        Database = new Database(dbPath);

        return builder.Build();
    }
}
