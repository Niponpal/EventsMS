using EventsMS.Models;

namespace EventsMS.Repository;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllPaymentAsync(CancellationToken cancellationToken);
    Task<Payment?> GetPaymentByIdAsync(long id, CancellationToken cancellationToken);
    Task<Payment> AddPaymentAsync(Payment  payment, CancellationToken cancellationToken);
    Task<Payment?> UpdatePaymentAsync(Payment  payment, CancellationToken cancellationToken);
    Task<Payment> DeletePaymentyAsync(long id, CancellationToken cancellationToken);
}

