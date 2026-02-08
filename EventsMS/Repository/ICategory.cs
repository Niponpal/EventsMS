using EventsMS.Models;

namespace EventsMS.Repository;

public interface ICategory
{
    Task<IEnumerable<Category>> GetAllCategoryAsync(CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAsync(long id, CancellationToken cancellationToken);
    Task<Category> AddCategoryAsync(Category  category, CancellationToken cancellationToken);
    Task<Category?> UpdateCategoryAsync(Category  category, CancellationToken cancellationToken);
    Task<Category> DeleteCategoryAsync(long id, CancellationToken cancellationToken);
}
