namespace Domain.Guests.Ports
{
    public interface IGuestRepository
    {
        Task<Guest.Entities.Guest> Get(int Id);
        Task<int> Create(Guest.Entities.Guest guest);
    }
}
