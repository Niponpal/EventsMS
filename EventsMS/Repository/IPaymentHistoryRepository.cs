using EventsMS.Models;

namespace EventsMS.Repository;

public interface IPaymentHistoryRepository
{
    Task<IEnumerable<PaymentHistory>> GetAllPaymentHistoryAsync(CancellationToken cancellationToken);
    Task<PaymentHistory?> GetPaymentHistoryByIdAsync(long id, CancellationToken cancellationToken);
    Task<PaymentHistory> AddPaymentHistoryAsync(PaymentHistory  paymentHistory, CancellationToken cancellationToken);
    Task<PaymentHistory?> UpdatePaymentHistoryAsync(PaymentHistory  paymentHistory, CancellationToken cancellationToken);
    Task<PaymentHistory> DeletePaymentHistoryAsync(long id, CancellationToken cancellationToken);
}
