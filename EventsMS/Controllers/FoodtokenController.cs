using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsMS.Controllers;

public class FoodtokenController : Controller
{
    private readonly IFoodTokenRepository _foodTokenRepository;

    public FoodtokenController(IFoodTokenRepository foodTokenRepository)
    {
        _foodTokenRepository = foodTokenRepository;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _foodTokenRepository.GetAllFoodTokenAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult>CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View(new Models.FoodToken());
        }
        else
        {
            var data = await _foodTokenRepository.GetFoodTokenByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(FoodToken foodToken, CancellationToken cancellationToken)
    {
        if (foodToken.Id == 0)
            {
                await _foodTokenRepository.AddFoodTokenAsync(foodToken, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _foodTokenRepository.UpdateFoodTokenAsync(foodToken, cancellationToken);
                return RedirectToAction(nameof(Index));
            }     
    }
}
