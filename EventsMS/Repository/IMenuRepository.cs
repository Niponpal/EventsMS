using EventsMS.Models;

namespace EventsMS.Repository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllMenuAsync(CancellationToken cancellationToken);
        Task<Menu?> GetMenuByIdAsync(long id, CancellationToken cancellationToken);
        Task<Menu> AddMenuAsync(Menu  menu, CancellationToken cancellationToken);
        Task<Menu?> UpdateMenuAsync(Menu  menu, CancellationToken cancellationToken);
        Task<Menu> DeleteMenuAsync(long id, CancellationToken cancellationToken);
    }
}
