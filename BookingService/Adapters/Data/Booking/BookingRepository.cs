using Domain.Booking.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _dbContext;
        
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _dbContext = hotelDbContext;
        }

        public async Task<Domain.Guests.Entities.Booking> CreateBooking(Domain.Guests.Entities.Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public Task<Domain.Guests.Entities.Booking> Get(int id)
        {
            return _dbContext.Bookings.Where(x => x.Id == id).FirstAsync();
        }
    }
}
