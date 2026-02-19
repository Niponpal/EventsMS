using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers;

public class FoodtokenController : Controller
{
    private readonly IFoodTokenRepository _foodTokenRepository;
    private readonly IStudentRegistrationRepository _studentRegistrationRepository;


    public FoodtokenController(IFoodTokenRepository foodTokenRepository, IStudentRegistrationRepository studentRegistrationRepository)
    {
        _foodTokenRepository = foodTokenRepository;
        _studentRegistrationRepository = studentRegistrationRepository;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _foodTokenRepository.GetAllFoodTokenAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult>CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();
        if (id == 0)
        {
            return View(new FoodToken());
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
        ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();
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
        [HttpPost]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _foodTokenRepository.DeleteFoodTokenAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
    {
        var data = await _foodTokenRepository.GetFoodTokenByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
}
