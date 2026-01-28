using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entity;
using Payment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Infrastructure.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Payments.
            FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(PaymentEntity paymet)
    {
        await _context.Payments.AddAsync(paymet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PaymentEntity payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }
}
