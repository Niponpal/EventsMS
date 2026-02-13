using EventsMS.Data;
using EventsMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsMS.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;
    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Menu> AddMenuAsync(Menu menu, CancellationToken cancellationToken)
    {
      var data = await _context.Menus.AddAsync(menu, cancellationToken);
        if (data != null)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return menu;
        }
        return null;
    }

    public async Task<Menu> DeleteMenuAsync(long id, CancellationToken cancellationToken)
    {
       var data = await _context.Menus.FindAsync(id,cancellationToken);
        if (data != null)
        {
            _context.Menus.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public async Task<IEnumerable<Menu>> GetAllMenuAsync(CancellationToken cancellationToken)
    {
       var data = await _context.Menus.ToListAsync(cancellationToken);
        if (data != null) { 
        return data;
        }
        return null;
    }

    public async Task<Menu?> GetMenuByIdAsync(long id, CancellationToken cancellationToken)
    {
       var data = await _context.Menus.FindAsync(id,cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Menu?> UpdateMenuAsync(Menu menu, CancellationToken cancellationToken)
    {
        var data = await _context.Menus.FindAsync(menu.Id,cancellationToken);
        if (data != null)
        {

            data.Title = menu.Title;
            data.Url = menu.Url;
            data.ParentId = menu.ParentId;
            data.SortOrder = menu.SortOrder;
            data.IsActive = menu.IsActive;
             _context.Menus.Update(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
