using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers;

public class MenuController : Controller
{
    private readonly IMenuRepository _menuRepository;
    
    public MenuController(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _menuRepository.GetAllMenuAsync(cancellationToken);
        if (data != null)
        {
            return View(data);
        }   
        return View(NotFound());
    }
    [HttpGet]   
    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View(new Models.Menu());
        }
        else
        {
            var data = await _menuRepository.GetMenuByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Models.Menu menu, CancellationToken cancellationToken)
    {
        if (menu.Id == 0)
        {
           await _menuRepository.AddMenuAsync(menu, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        else
        {
           await _menuRepository.UpdateMenuAsync(menu, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await _menuRepository.DeleteMenuAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
     [HttpGet]
     public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
    {
        var data = await _menuRepository.GetMenuByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
}
