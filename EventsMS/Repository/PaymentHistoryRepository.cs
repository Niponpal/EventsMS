using EventsMS.Models;

namespace EventsMS.Repository
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        public Task<PaymentHistory> AddPaymentHistoryAsync(PaymentHistory paymentHistory, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentHistory> DeletePaymentHistoryAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentHistory>> GetAllPaymentHistoryAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentHistory?> GetPaymentHistoryByIdAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentHistory?> UpdatePaymentHistoryAsync(PaymentHistory paymentHistory, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
