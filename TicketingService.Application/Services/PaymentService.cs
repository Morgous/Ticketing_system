using TicketingSystem.Domain.Enums;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingService.Application.Services
{
    public class PaymentService
    {

        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IRepository<Order> _orderRepository;

        public PaymentService(IRepository<Payment> paymentRepository, IRepository<Seat> seatRepository, IRepository<Order> orderRepository)
        {
            _paymentRepository = paymentRepository;
            _seatRepository = seatRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task AddPaymentAsync(Payment newPayment)
        {
            await _paymentRepository.AddAsync(newPayment);
        }

        public async Task UpdatePaymentAsync(Payment updatedPayment)
        {
            await _paymentRepository.UpdateAsync(updatedPayment);
        }

        public async Task DeletePaymentAsync(Guid id)
        {
            await _paymentRepository.DeleteAsync(id);
        }

        public async Task UpdateSeatsStatusByPaymentIdAsync(Guid paymentId, SeatStatus status)
        {
            var orders = await _orderRepository.FindAsync(o => o.PaymentId == paymentId);
            var seatIds = orders.Select(o => o.SeatId).ToList();

            foreach (var seatId in seatIds)
            {
                var seat = await _seatRepository.GetByIdAsync(seatId);
                if (seat != null)
                {
                    seat.Status = status;
                    await _seatRepository.UpdateAsync(seat);
                }
            }
        }
    }
}
