using EventsMS.Models;

namespace EventsMS.Repository;

public interface IFoodTokenRepository
{
    Task<IEnumerable<FoodToken>> GetAllFoodTokenAsync(CancellationToken cancellationToken);
    Task<FoodToken?> GetFoodTokenByIdAsync(long id, CancellationToken cancellationToken);
    Task<FoodToken> AddFoodTokenAsync(FoodToken  foodToken, CancellationToken cancellationToken);
    Task<FoodToken?> UpdateFoodTokenAsync(FoodToken foodToken, CancellationToken cancellationToken);
    Task<FoodToken> DeleteFoodTokenAsync(long id, CancellationToken cancellationToken);
}
