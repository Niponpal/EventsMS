using EventsMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventsMS.Repository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoryAsync(CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAsync(long id, CancellationToken cancellationToken);
    Task<Category> AddCategoryAsync(Category  category, CancellationToken cancellationToken);
    Task<Category?> UpdateCategoryAsync(Category  category, CancellationToken cancellationToken);
    Task<Category> DeleteCategoryAsync(long id, CancellationToken cancellationToken);
    IEnumerable<SelectListItem> Dropdown();
}
