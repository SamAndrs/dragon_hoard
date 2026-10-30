using dragon_hoard.Content.Data;
using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace dragon_hoard.Content.Repositories
{
    public sealed class RoomCardRepository : IRoomCardRepository
    {
        private readonly GameDatabase _database;

        private readonly MainGame _game;

        public RoomCardRepository(MainGame game, GameDatabase database)
        {
            _game = game;

            _database = database;
        }

        public IReadOnlyList<RoomCard> GetAll()
        {
            var cards = new List<RoomCard>();

            using var connection = _database.OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT Id, EventName, CardEvent, EventText, ItemId, Persistent, TextureReference
                FROM RoomCards
                ORDER BY Id
                """;

            using var reader = command.ExecuteReader();

            while (reader.Read())
                cards.Add(MapItem(reader));

            foreach (var card in cards)
                card.CardImage = _game.Content.Load<Texture2D>($"Images/RoomCardImages/{card.TextureReference}");

            return cards;

        }


        public IReadOnlyList<RoomCard> GetByEventType(RoomCardEvent eventType)
        {
            throw new NotImplementedException();
        }

        public RoomCard GetById(int id)
        {
            using var connection = _database.OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT Id, EventName, CardEvent, EventText, ItemId, Persistent, TextureReference
                FROM RoomCards
                WHERE Id = $id;
                """;

            command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return MapItem(reader);
        }

       

        private static RoomCard MapItem(SqliteDataReader reader)
        {
            return new RoomCard
            {
                Id = reader.GetInt32(0),
                EventName = reader.GetString(1),
                CardEvent = (RoomCardEvent)reader.GetInt64(2),
                EventText = reader.GetString(3),
                ItemId = reader.GetInt32(4),
                Persistent = reader.GetBoolean(5),
                TextureReference = reader.GetString(6)
            };
        }
    }
}
