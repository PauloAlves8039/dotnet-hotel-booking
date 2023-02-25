namespace Domain.Booking.Ports
{
    public interface IBookingRepository
    {
        Task<Guests.Entities.Booking> Get(int id);
        Task<Guests.Entities.Booking> CreateBooking(Guests.Entities.Booking booking);
    }
}
