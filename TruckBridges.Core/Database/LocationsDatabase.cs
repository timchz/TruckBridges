using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using TruckBridges.Core.Interfaces;
using TruckBridges.Core.Models;

namespace TruckBridges.Core.Database
{
    public class LocationsDatabase
    {
        private SQLiteConnection database;

        public LocationsDatabase(ISqlite sqlite)
        {
            database = sqlite.GetConnection();
            database.CreateTable<LocationAutoCompleteResult>();
        }

        public IEnumerable<LocationAutoCompleteResult> GetLocations()
        {
            return (from i in database.Table<LocationAutoCompleteResult>() select i).ToList();
        }

        public int InsertLocation(LocationAutoCompleteResult location)
        {
            var num = database.Insert(location);
            database.Commit();
            return num;
        }
    }
}
