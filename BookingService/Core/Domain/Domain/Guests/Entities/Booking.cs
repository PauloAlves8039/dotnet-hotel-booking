using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guests.Enums;
using Action = Domain.Guests.Enums.Action;

namespace Domain.Guests.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Roons.Entities.Room Room { get; set; }
        public Guests.Entities.Guest Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus { get { return Status; } }

        public Booking()
        {
            Status = Status.Created;
        }

        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => Status
            };
        }

        public bool IsValid()
        {
            try
            {
                this.ValidateState();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            this.ValidateState();

            this.Guest.IsValid();

            if (!this.Room.CanBeBooked())
            {
                throw new RoomCannotBeBookedException();
            }

            if (this.Id == 0)
            {
                var resp = await bookingRepository.CreateBooking(this);
                this.Id = resp.Id;
            }
            else
            {

            }
        }

        private void ValidateState()
        {
            if (this.PlacedAt == default(DateTime))
            {
                throw new PlacedAtIsARequiredInformationException();
            }

            if (this.Start == default(DateTime))
            {
                throw new StartDateTimeIsRequiredException();
            }

            if (this.End == default(DateTime))
            {
                throw new EndDateTimeIsRequiredException();
            }

            if (this.Room == null)
            {
                throw new RoomIsRequiredException();
            }

            if (this.Guest == null)
            {
                throw new GuestIsRequiredException();
            }
        }

    }
}
