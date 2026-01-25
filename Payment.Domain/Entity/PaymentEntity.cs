using Payment.Domain.Constant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Entity;

public class PaymentEntity
{
    public Guid Id { get; private set; }
    public string InvoiceId { get; private set; } = default!;

    public decimal Amount { get; private set; }

    public PaymentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime UpdateAt { get; private set; }
    private PaymentEntity() { }

    public PaymentEntity(string invoidId, decimal ammount)
    {
        Id = Guid.NewGuid();
        InvoiceId = invoidId;
        Amount = ammount;
        Status = PaymentStatus.Pending;
    }

    public void MarkAsSuccess()
    {
        Status = PaymentStatus.Success;
        UpdateAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = PaymentStatus.Failed;
        UpdateAt = DateTime.UtcNow;
    }
}
