using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Interfaces;

public interface IPaymentRepository
{
    Task<PaymentEntity?> GetByIdAsync(Guid id);
    Task AddAsync(PaymentEntity paymet);
    Task UpdateAsync(PaymentEntity payment);
}
