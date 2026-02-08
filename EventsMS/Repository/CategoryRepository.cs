using EventsMS.Data;
using EventsMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsMS.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        var data = await _context.Categories.AddAsync(category, cancellationToken);
        if (data != null)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }
        return null!;
    }

    public async Task<Category> DeleteCategoryAsync(long id, CancellationToken cancellationToken)
    {
       var data =await _context.Categories.FindAsync(id,cancellationToken);
        if (data != null)
        {
            _context.Categories.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null!;
    }

    public async Task<IEnumerable<Category>> GetAllCategoryAsync(CancellationToken cancellationToken)
    {
      var data = await _context.Categories.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Category?> GetCategoryByIdAsync(long id, CancellationToken cancellationToken)
    {
        var  data = await _context.Categories.FindAsync(id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Category?> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        var data = await _context.Categories.FindAsync(category.Id, cancellationToken);
        if (data != null)
        {
            data.Name = category.Name;
            data.Description = category.Description;
            _context.Categories.Update(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
