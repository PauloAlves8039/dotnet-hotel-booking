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
        public ICollection<Guests.Entities.Booking>? Bookings { get; set; }

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
            get
            {
                var notAvailableStatuses = new List<Guests.Enums.Status>()
                {
                    Guests.Enums.Status.Created,
                    Guests.Enums.Status.Paid,
                };

                return this.Bookings?.Where(
                    b => b.Room.Id == this.Id &&
                    notAvailableStatuses.Contains(b.Status)).Count() > 0;
            }
        }

        public bool CanBeBooked()
        {
            try
            {
                this.ValidateState();
            }
            catch (Exception)
            {

                return false;
            }

            if (!this.IsAvailable)
            {
                return false;
            }

            return true;
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
