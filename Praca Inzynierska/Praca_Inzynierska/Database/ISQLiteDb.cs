using SQLite;

namespace Praca_Inzynierska
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

