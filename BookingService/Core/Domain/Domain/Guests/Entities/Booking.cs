using Domain.Guests.Enums;
using Action = Domain.Guests.Enums.Action;

namespace Domain.Guest.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Roons.Entities.Room Room { get; set; }
        public Guest Guest { get; set; }
        private Status Status { get; set; }

        public Booking()
        {
            Status = Status.Created;
        }

        public Status CurrentStatus { get { return Status; } }

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
    }
}
