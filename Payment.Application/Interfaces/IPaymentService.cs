using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentEntity?> GetPaymentByIdAsync(Guid id);
    Task<PaymentEntity> CreatePaymentAsync(string invoiceId, decimal amount);
    Task<bool> CompletePaymentAsync(Guid id);
}
