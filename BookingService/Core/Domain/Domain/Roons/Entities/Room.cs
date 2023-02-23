using Domain.Roons.Exceptions;
using Domain.Roons.Ports;
using Domain.Guests.ValueObjects;

namespace Domain.Roons.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }

        public async Task Save(IRoomRepository roomRepository) 
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await roomRepository.Create(this);
            }
        }

        public bool IsAvailable
        {
            get
            {
                if (InMaintenance || HasGuest)
                {
                    return false;
                }

                return true;
            }
        }

        public bool HasGuest
        {
            get { return true; }
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw new InvalidRoomDataException();
            }
        }
    }
}
