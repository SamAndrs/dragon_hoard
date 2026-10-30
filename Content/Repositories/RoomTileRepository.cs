using dragon_hoard.Content.Data;
using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace dragon_hoard.Content.Repositories
{
    public sealed class RoomTileRepository : IRoomTileRepository
    {
        private readonly GameDatabase _database;

        private readonly MainGame _game;

        public RoomTileRepository(MainGame game, GameDatabase database)
        {
            _game = game;
            
            _database = database;
        }



        public RoomTile GetById(int id)
        {
            using var connection = _database.OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT Id, RoomType, DoorValue, DoorUp, DoorRight, DoorDown, DoorLeft, EventName,
                EventText, TileImageReference, TileRoomImageReference
                FROM RoomTiles
                WHERE Id = $id;
                """;

            command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

           

            RoomTile tile = MapItem(reader);

            tile.TileImage = _game.Content.Load<Texture2D>($"Images/RoomTileImages/{tile.TileImageReference}"); // TODO add later.
            tile.TileRoomImage = _game.Content.Load<Texture2D>($"Images/RoomCardImages/{tile.TileRoomImageReference}");

            return tile;
        }



        public IReadOnlyList<RoomTile> GetAll()
        {
            var tiles = new List<RoomTile>();

            using var connection = _database.OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT Id, RoomType, DoorValue, DoorUp, DoorRight, DoorDown, DoorLeft, EventName,
                EventText, TileImageReference, TileRoomImageReference
                FROM RoomTiles
                ORDER BY Id
                """;

            using var reader = command.ExecuteReader();

            while (reader.Read())
                tiles.Add(MapItem(reader));

            foreach (var tile in tiles)
            {
                tile.TileImage = _game.Content.Load<Texture2D>($"Images/RoomTileImages/{tile.TileImageReference}"); // TODO fix images later.
                tile.TileRoomImage = _game.Content.Load<Texture2D>($"Images/RoomCardImages/{tile.TileRoomImageReference}");
            }

            return tiles;
        }


        private static RoomTile MapItem(SqliteDataReader reader)
        {
            return new RoomTile
            {
                Id = reader.GetInt32(0),
                RoomType = (RoomType)reader.GetInt64(1),
                DoorValue = reader.GetInt32(2),
                
                DoorUp = (DoorType)reader.GetInt32(3),
                DoorRight = (DoorType)reader.GetInt32(4),
                DoorDown = (DoorType)reader.GetInt32(5),
                DoorLeft = (DoorType)reader.GetInt32(6),
                
                EventName = reader.GetString(7),
                EventText = reader.GetString(8),
                TileImageReference = reader.GetString(9),
                TileRoomImageReference = reader.GetString(10),
            };
        }

       
    }
}
