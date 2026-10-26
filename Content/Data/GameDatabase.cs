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
            string databasePath = Path.Combine(AppContext.BaseDirectory, "drgHoardDB.db");

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

/* USE CASE:
  
 * Fetch game data
Let's create a C# class representing an item in your game.

Create GameItem.cs:


csharp
public class GameItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int Damage { get; set; }
}

* Next, add a method to GameDatabase.cs that fetches a specific item by its ID:
public GameItem? GetItemById(int id)
{
    using var connection = OpenConnection();
    using var command = connection.CreateCommand();

    command.CommandText = """
        SELECT Id, Name, Description, Damage
        FROM Items
        WHERE Id = $id;
        """;

    command.Parameters.AddWithValue("$id", id);

    using var reader = command.ExecuteReader();

    if (!reader.Read())
        return null;

    return new GameItem
    {
        Id = reader.GetInt32(0),
        Name = reader.GetString(1),
        Description = reader.GetString(2),
        Damage = reader.GetInt32(3)
    };
}

* 
* protected override void LoadContent()
    {
        // Retrieve an item from the database.
        GameItem? sword = _itemService.GetItem(1);

        if (sword is not null)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Item: {sword.Name}, Damage: {sword.Damage}");
        }

        // Retrieve all items.
        IReadOnlyList<GameItem> items =
            _itemService.GetAllItems();

        foreach (GameItem item in items)
        {
            System.Diagnostics.Debug.WriteLine(item.Name);
        }
    }
*/
