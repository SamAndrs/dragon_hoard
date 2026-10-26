using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Repositories.Interfaces;
using System.Collections.Generic;

namespace dragon_hoard.Content.Services
{
    public sealed class RoomTileService
    {
        private readonly IRoomTileRepository _repository;


        public RoomTileService(IRoomTileRepository repository)
        {
            _repository = repository;
        }



        public IReadOnlyList<RoomTile> GetAllRoomTiles()
        {
            return _repository.GetAll();
        }



        public bool TileExists(int id)
        {
            return id > 0 && _repository.GetById(id) is not null;
        }
    }
}
