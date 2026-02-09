using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsMS.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var category =await _categoryRepository.GetAllCategoryAsync(cancellationToken);
        if (category != null)
        {
            return View(category);
        }
        return View(NotFound());
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEDit(int id, CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View(new Models.Category());
        }
        var category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
        if (category != null)
        {
            return View(category);
        }
        return View(NotFound());
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEDit(Models.Category category, CancellationToken cancellationToken)
    {
      
            if (category.Id == 0)
            {
                await _categoryRepository.AddCategoryAsync(category, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _categoryRepository.UpdateCategoryAsync(category, cancellationToken);
            return RedirectToAction(nameof(Index));
            }
           
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteCategoryAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
        if (category != null)
        {
            return View(category);
        }
        return View(NotFound());

    }

    }
