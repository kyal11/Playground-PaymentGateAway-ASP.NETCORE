using Payment.Application.Interfaces;
using Payment.Domain.Entity;
using Payment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }
    public async Task<PaymentEntity?> GetPaymentByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<PaymentEntity> CreatePaymentAsync(string invoiceId, decimal amount)
    {
        var payment = new PaymentEntity(invoiceId, amount);

        await _repository.AddAsync(payment);
        return payment;
    }

    public async Task<bool> CompletePaymentAsync(Guid id)
    {
        var payment = await _repository.GetByIdAsync(id);

        if (payment == null) return false;

        payment.MarkAsSuccess();

        await _repository.UpdateAsync(payment);

        return true;

    }

}
