using EventsMS.Data;
using EventsMS.Models;
using Microsoft.EntityFrameworkCore;
namespace EventsMS.Repository;

public class FoodTokenRepository : IFoodTokenRepository
{
    private readonly ApplicationDbContext _context;
    public FoodTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<FoodToken> AddFoodTokenAsync(FoodToken foodToken, CancellationToken cancellationToken)
    {
        var data = await _context.FoodTokens.AddAsync(foodToken, cancellationToken);
        if (data != null)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return foodToken;
        }
        return null;
    }

    public async Task<FoodToken> DeleteFoodTokenAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.FoodTokens.FindAsync(id,cancellationToken);
        if (data != null)
        {
            _context.FoodTokens.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public async Task<IEnumerable<FoodToken>> GetAllFoodTokenAsync(CancellationToken cancellationToken)
    {
        var data = await _context.FoodTokens.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<FoodToken?> GetFoodTokenByIdAsync(long id, CancellationToken cancellationToken)
    {
       var data =await _context.FoodTokens.FindAsync(id,cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<FoodToken?> UpdateFoodTokenAsync(FoodToken foodToken, CancellationToken cancellationToken)
    {
       var data = await _context.FoodTokens.FindAsync(foodToken.Id,cancellationToken);
        if (data != null)
        {
            data.RegistrationId = foodToken.RegistrationId;
            data.TokenCode = foodToken.TokenCode;
            data.IsUsed = foodToken.IsUsed;
            data.IssuedUtc = foodToken.IssuedUtc;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
