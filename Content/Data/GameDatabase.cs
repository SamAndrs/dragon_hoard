using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.Sqlite;
using dragon_hoard.Content.Models.Objects.Entities;

namespace dragon_hoard.Content.Data
{
    ////////////////////////////////////////// GAME DATABASE CLASS /////////////////////////////////////////////////
    /*
        This class represents the main Database for the game, in which is stored the data for all
        the RoomTiles, RoomCards and similar classes and their derived objects.
        The database is set to readonly state simply because no data is to be written to it at runtime.

        This class doesn't keep a connection open permanently. Instead, it provides a method for 
        opening a connection whenever a query needs to be made to the database.
        Thit makes it straightforward to manage resources and to run different queries from 
        different parts of the game.

    :: TODO ---------------------------


    :: DONE ---------------------------
        v.0.001a (10/25) Initial installation and setup.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public sealed class GameDatabase
    {
        private readonly string _connectionString;

        public GameDatabase()
        {
            //string databasePath = Path.Combine(AppContext.BaseDirectory, "drgHoardDB.db");

            string databasePath = Path.Combine(AppContext.BaseDirectory, "drgHoardDBTest.db");

            if (!File.Exists(databasePath))
            {
                throw new FileNotFoundException("The game's database could not be found.", databasePath);
            }

            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = databasePath,
                Mode = SqliteOpenMode.ReadOnly
            };

            _connectionString = builder.ToString();
        }

        public SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection(_connectionString);

            try
            {
                connection.Open();
                return connection;
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }
    }
}