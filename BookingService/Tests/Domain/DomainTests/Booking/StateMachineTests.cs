using NUnit.Framework;
using Action = Domain.Guests.Enums.Action;
using Domain.Guests.Enums;
using Domain.Guests.Entities;

namespace DomainTests.Bookings
{
    public class StateMachineTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus() 
        {
            var booking = new Booking();
            
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus() 
        {
            var booking = new Booking();
            
            booking.ChangeState(Action.Pay);
            
            Assert.AreEqual(booking.CurrentStatus, Status.Paid);
        }

        [Test]
        public void ShouldSetStatusToCanceldWhenCancelingABookingWithCreatedStatus() 
        {
            var booking = new Booking();
            
            booking.ChangeState(Action.Cancel);
            
            Assert.AreEqual(booking.CurrentStatus, Status.Canceled);
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking() 
        {
            var booking = new Booking();
            
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            
            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }

        [Test]
        public void ShouldSetStatusToRefoundedWhenRefoundingAPaidBooking() 
        {
            var booking = new Booking();
            
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);
            
            Assert.AreEqual(booking.CurrentStatus, Status.Refounded);
        }

        [Test]
        public void ShouldSetStatusToCreatedWhenReopeningACanceledBooking() 
        {
            var booking = new Booking();
            
            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);
            
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingAFinishedBookin()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }
    }
}
