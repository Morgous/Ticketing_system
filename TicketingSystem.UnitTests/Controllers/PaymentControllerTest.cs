using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using TicketingService.Application.Services;
using TicketingSystem.API.Controllers;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Controllers
{
    public class PaymentControllerTests
    {
        private readonly Mock<IRepository<Payment>> _paymentRepositoryMock;
        private readonly Mock<IRepository<Seat>> _seatRepositoryMock;
        private readonly Mock<IRepository<Order>> _orderRepositoryMock;
        private readonly PaymentService _paymentService;
        private readonly PaymentController _controller;

        public PaymentControllerTests()
        {
            _paymentRepositoryMock = new Mock<IRepository<Payment>>();
            _seatRepositoryMock = new Mock<IRepository<Seat>>();
            _orderRepositoryMock = new Mock<IRepository<Order>>();

            _paymentService = new PaymentService(_paymentRepositoryMock.Object, _seatRepositoryMock.Object, _orderRepositoryMock.Object);

            _controller = new PaymentController(_paymentService);
        }

        [Fact]
        public async Task GetAllPayments_ShouldReturnOkResult_WithListOfPayments()
        {
            var payments = new List<Payment> { new Payment { Id = Guid.NewGuid(), Status = PaymentStatus.Pending, PaymentDate = DateTime.Now } };
            _paymentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(payments);

            var result = await _controller.GetAllPayments() as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<List<Payment>>(result.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetPaymentById_ShouldReturnOkResult_WhenPaymentExists()
        {
            var paymentId = Guid.NewGuid();
            var payment = new Payment { Id = paymentId, Status = PaymentStatus.Pending, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync(payment);

            var result = await _controller.GetPaymentById(paymentId) as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<Payment>(result.Value);
            Assert.Equal(paymentId, returnValue.Id);
        }

        [Fact]
        public async Task GetPaymentById_ShouldReturnNotFound_WhenPaymentDoesNotExist()
        {
            var paymentId = Guid.NewGuid();
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync((Payment)null);

            var result = await _controller.GetPaymentById(paymentId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreatePayment_ShouldReturnCreatedAtActionResult()
        {
            var newPayment = new Payment { Id = Guid.NewGuid(), Status = PaymentStatus.Pending, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.AddAsync(newPayment)).Returns(Task.CompletedTask);

            var result = await _controller.CreatePayment(newPayment) as CreatedAtActionResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<Payment>(result.Value);
            Assert.Equal(newPayment.Id, returnValue.Id);
        }

        [Fact]
        public async Task UpdatePayment_ShouldReturnNoContent_WhenPaymentIsUpdated()
        {
            var paymentId = Guid.NewGuid();
            var updatedPayment = new Payment { Id = paymentId, Status = PaymentStatus.Completed, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.UpdateAsync(updatedPayment)).Returns(Task.CompletedTask);

            var result = await _controller.UpdatePayment(paymentId, updatedPayment);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdatePayment_ShouldReturnBadRequest_WhenPaymentIdDoesNotMatch()
        {
            var paymentId = Guid.NewGuid();
            var updatedPayment = new Payment { Id = Guid.NewGuid(), Status = PaymentStatus.Completed, PaymentDate = DateTime.Now };

            var result = await _controller.UpdatePayment(paymentId, updatedPayment);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletePayment_ShouldReturnNoContent_WhenPaymentIsDeleted()
        {
            var paymentId = Guid.NewGuid();
            _paymentRepositoryMock.Setup(repo => repo.DeleteAsync(paymentId)).Returns(Task.CompletedTask);

            var result = await _controller.DeletePayment(paymentId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CompletePayment_ShouldReturnNoContent_WhenPaymentIsCompleted()
        {
            var paymentId = Guid.NewGuid();
            var payment = new Payment { Id = paymentId, Status = PaymentStatus.Pending, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync(payment);
            _paymentRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Payment>())).Returns(Task.CompletedTask);

            Expression<Func<Order, bool>> orderPredicate = o => o.PaymentId == paymentId;
            _orderRepositoryMock.Setup(repo => repo.FindAsync(orderPredicate)).ReturnsAsync(new List<Order>());

            var result = await _controller.CompletePayment(paymentId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CompletePayment_ShouldReturnNotFound_WhenPaymentDoesNotExist()
        {
            var paymentId = Guid.NewGuid();
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync((Payment)null);

            var result = await _controller.CompletePayment(paymentId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task FailPayment_ShouldReturnNoContent_WhenPaymentIsFailed()
        {
            var paymentId = Guid.NewGuid();
            var payment = new Payment { Id = paymentId, Status = PaymentStatus.Pending, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync(payment);
            _paymentRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Payment>())).Returns(Task.CompletedTask);

            Expression<Func<Order, bool>> orderPredicate = o => o.PaymentId == paymentId;
            _orderRepositoryMock.Setup(repo => repo.FindAsync(orderPredicate)).ReturnsAsync(new List<Order>());

            var result = await _controller.FailPayment(paymentId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task FailPayment_ShouldReturnNotFound_WhenPaymentDoesNotExist()
        {
            var paymentId = Guid.NewGuid();
            _paymentRepositoryMock.Setup(repo => repo.GetByIdAsync(paymentId)).ReturnsAsync((Payment)null);

            var result = await _controller.FailPayment(paymentId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}