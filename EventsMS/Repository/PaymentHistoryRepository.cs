using EventsMS.Data;
using EventsMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsMS.Repository
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaymentHistory> AddPaymentHistoryAsync(PaymentHistory paymentHistory, CancellationToken cancellationToken)
        {
           var data =await _context.paymentHistories.AddAsync(paymentHistory, cancellationToken);
            if (data != null)
            {
                await _context.SaveChangesAsync(cancellationToken);
                return paymentHistory;
            }
            return null;
        }

        public async Task<PaymentHistory> DeletePaymentHistoryAsync(long id, CancellationToken cancellationToken)
        {
           var data = await _context.paymentHistories.FindAsync(new object[] { id }, cancellationToken);
            if (data != null)
            {
                _context.paymentHistories.Remove(data);
                await _context.SaveChangesAsync(cancellationToken);
                return data;
            }
            return null;    
        }

        public async Task<IEnumerable<PaymentHistory>> GetAllPaymentHistoryAsync(CancellationToken cancellationToken)
        {
           var data = await _context.paymentHistories.ToListAsync(cancellationToken);
            if (data != null) { 
            return data;
            }
            return null;
        }

        public async Task<PaymentHistory?> GetPaymentHistoryByIdAsync(long id, CancellationToken cancellationToken)
        {
            var data = await _context.paymentHistories.FindAsync(id,cancellationToken);
            if (data != null)
            {
                return data;
            }
            return null;


        }

        public async Task<PaymentHistory?> UpdatePaymentHistoryAsync(PaymentHistory paymentHistory, CancellationToken cancellationToken)
        {
          var data = await _context.paymentHistories.FindAsync(new object[] { paymentHistory.Id }, cancellationToken);
            if (data != null)
            {
                data.PaymentId = paymentHistory.PaymentId;
                data.Provider = paymentHistory.Provider;
                data.ProviderSessionId = paymentHistory.ProviderSessionId;
                data.Status = paymentHistory.Status;
                data.TransactionId = paymentHistory.TransactionId;
                data.ValidationId = paymentHistory.ValidationId;
                data.BankName = paymentHistory.BankName;
                data.CustomerName = paymentHistory.CustomerName;
                data.CustomerMobile = paymentHistory.CustomerMobile;
                data.Amount = paymentHistory.Amount;
                data.Currency = paymentHistory.Currency;
                data.JsonResponse = paymentHistory.JsonResponse;
                _context.paymentHistories.Update(data);
                await _context.SaveChangesAsync(cancellationToken);
                return data;
            }
            return null;
        }
    }
}
