using SQLite.Net;

namespace TruckBridges.Core.Interfaces
{
    public interface ISqlite
    {
        SQLiteConnection GetConnection();
    }
}
