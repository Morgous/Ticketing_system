using Microsoft.AspNetCore.Mvc;
using TicketingService.Application.Services;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(Payment newPayment)
        {
            await _paymentService.AddPaymentAsync(newPayment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = newPayment.Id }, newPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, Payment updatedPayment)
        {
            if (id != updatedPayment.Id)
            {
                return BadRequest();
            }

            await _paymentService.UpdatePaymentAsync(updatedPayment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }

        [HttpPost("{paymentId}/complete")]
        public async Task<IActionResult> CompletePayment(Guid paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null)
            {
                return NotFound();
            }

            payment.Status = PaymentStatus.Completed;
            await _paymentService.UpdatePaymentAsync(payment);
            await _paymentService.UpdateSeatsStatusByPaymentIdAsync(paymentId, SeatStatus.Sold);

            return NoContent();
        }

        [HttpPost("{paymentId}/failed")]
        public async Task<IActionResult> FailPayment(Guid paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null)
            {
                return NotFound();
            }

            payment.Status = PaymentStatus.Failed;
            await _paymentService.UpdatePaymentAsync(payment);
            await _paymentService.UpdateSeatsStatusByPaymentIdAsync(paymentId, SeatStatus.Available);

            return NoContent();
        }
    }
}