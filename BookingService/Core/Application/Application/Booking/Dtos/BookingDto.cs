using Domain.Guests.Enums;
using Entities = Domain.Guests.Entities;

namespace Application.Booking.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        private Status Status { get; set; }

        public BookingDto()
        {
            this.PlacedAt = DateTime.UtcNow;
        }

        public static Entities.Booking MapToEntity(BookingDto bookingDto)
        {
            return new Entities.Booking
            {
                Id = bookingDto.Id,
                Start = bookingDto.Start,
                Guest = new Entities.Guest { Id = bookingDto.GuestId },
                Room = new Domain.Roons.Entities.Room { Id = bookingDto.RoomId },
                End = bookingDto.End
            };
        }
    }
}
