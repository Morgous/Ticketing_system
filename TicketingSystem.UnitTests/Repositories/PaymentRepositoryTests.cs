using Moq;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Repositories
{
    public class PaymentRepositoryTests
    {
        private readonly Mock<IRepository<Payment>> _paymentRepositoryMock;

        public PaymentRepositoryTests()
        {
            _paymentRepositoryMock = new Mock<IRepository<Payment>>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfPayments()
        {
            var payments = new List<Payment> { new Payment { Id = Guid.NewGuid(), Status = PaymentStatus.Pending, PaymentDate = DateTime.Now } };
            _paymentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(payments);

            var result = await _paymentRepositoryMock.Object.GetAllAsync();
            Assert.NotNull(result);
            Assert.IsType<List<Payment>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPayment_WhenPaymentExists()
        {
            var paymentId = Guid.NewGuid();
            var payment = new Payment { Id = paymentId, Status = PaymentStatus.Pending, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync(payment);
            var result = await _paymentRepositoryMock.Object.GetByIdAsync(paymentId);

            Assert.NotNull(result);
            Assert.IsType<Payment>(result);
            Assert.Equal(paymentId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenPaymentDoesNotExist()
        {
            var paymentId = Guid.NewGuid();
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync((Payment)null);

            var result = await _paymentRepositoryMock.Object.GetByIdAsync(paymentId);
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPayment()
        {
            var newPayment = new Payment { Id = Guid.NewGuid(), Status = PaymentStatus.Pending, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.AddAsync(newPayment)).Returns(Task.CompletedTask);
            await _paymentRepositoryMock.Object.AddAsync(newPayment);

            _paymentRepositoryMock.Verify(repo => repo.AddAsync(newPayment), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePayment()
        {
            var updatedPayment = new Payment { Id = Guid.NewGuid(), Status = PaymentStatus.Completed, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.UpdateAsync(updatedPayment)).Returns(Task.CompletedTask);
            await _paymentRepositoryMock.Object.UpdateAsync(updatedPayment);
            _paymentRepositoryMock.Verify(repo => repo.UpdateAsync(updatedPayment), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeletePayment()
        {
            var paymentId = Guid.NewGuid();
            _paymentRepositoryMock.Setup(repo => repo.DeleteAsync(paymentId)).Returns(Task.CompletedTask);
            await _paymentRepositoryMock.Object.DeleteAsync(paymentId);

            _paymentRepositoryMock.Verify(repo => repo.DeleteAsync(paymentId), Times.Once);
        }
    }
}