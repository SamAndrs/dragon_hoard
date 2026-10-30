using dragon_hoard.Content.Models.Objects.Entities.Cards;
using System.Collections.Generic;

namespace dragon_hoard.Content.Repositories.Interfaces
{
    public interface IRoomTileRepository
    {
        RoomTile? GetById(int id);

        IReadOnlyList<RoomTile> GetAll();
    }
}
