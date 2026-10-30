using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Repositories.Interfaces
{
    public interface IRoomCardRepository
    {
        RoomCard? GetById(int id);

        IReadOnlyList<RoomCard> GetAll();

        IReadOnlyList<RoomCard> GetByEventType(RoomCardEvent eventType);
    }
}
