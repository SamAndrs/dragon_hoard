using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Services
{
    public sealed class RoomCardService
    {
        private readonly IRoomCardRepository _repository;


        public RoomCardService(IRoomCardRepository repository)
        {
            _repository = repository;
        }

        public RoomCard? GetRoomCard(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public IReadOnlyList<RoomCard> GetAllRoomCards()
        {
            return _repository.GetAll();
        }

        public IReadOnlyList<RoomCard> GetAllOfEventType(RoomCardEvent eventType)
        {
            if (eventType < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(eventType));

            return _repository.GetByEventType(eventType);
        }

        public bool CardExists(int id)
        {
            return id > 0 && _repository.GetById(id) is not null;
        }
            
    }
}
