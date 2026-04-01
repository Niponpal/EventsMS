using EventsMS.Data;
using EventsMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsMS.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;
    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Payment> AddPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        var data = await _context.Payments.AddAsync(payment, cancellationToken);
        if (data != null)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return data.Entity;
        }
        return null;
    }

    public async Task<Payment> DeletePaymentyAsync(long id, CancellationToken cancellationToken)
    {
       var data = await _context.Payments.FindAsync(new object[] { id }, cancellationToken);
        if (data != null)
        {
            _context.Payments.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentAsync(CancellationToken cancellationToken)
    {
       

        var data = await  _context.Payments.Include(x=>x.Registration).ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Payment?> GetPaymentByIdAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.Payments
          .Include(p => p.Registration)
          .Include(p => p.PaymentHistories)
          .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;

    }

    public async Task<Payment?> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
       var data = await _context.Payments.FindAsync(new object[] { payment.Id }, cancellationToken);
        if (data != null)
        {
            data.InvoiceNumber = payment.InvoiceNumber;
            data.RegistrationId = payment.RegistrationId;
            data.Provider = payment.Provider;
            data.ProviderSessionId = payment.ProviderSessionId;
            data.Status = payment.Status;
            data.TransactionId = payment.TransactionId;
            data.ValidationId = payment.ValidationId;
            data.Amount = payment.Amount;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public async Task<Payment?> GetPaymentByRegistrationIdAsync(long registrationId, CancellationToken cancellationToken)
    {
        return await _context.Payments
            .Include(p => p.Registration)
            .FirstOrDefaultAsync(p => p.RegistrationId == registrationId, cancellationToken);
    }

}
