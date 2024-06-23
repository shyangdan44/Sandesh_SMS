using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sandesh_SMS.Repositories;
using Sandesh_SMS.ViewModels;

namespace Sandesh_SMS.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        public async Task<IActionResult> Index()
        {
            //Fetch the data from database
            var classes = await _classRepository.GetAllAsync();
            return View(classes);
        }
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(ClassViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model); // Return to the form with validation errors
            }

            //Insert data to the database           
            await _classRepository.AddAsync(model);

            // Redirect to List all class page
            return RedirectToAction("Index", "Class");
        }

        //GET: /Class/Edit
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var classe = await _classRepository.GetByIdAsync(id);
            return View(classe);
        }
        //POST: /Class/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ClassViewModel clas)
        {

            if (ModelState.IsValid)
            {
                //Update the database with modified details
                await _classRepository.UpdateAsync(clas);

                // Redirect to List all class page
                return RedirectToAction("Index", "Class");
            }

            // If the model is not valid, return the view with the validation errors
            return View(clas);
        }

        //GET: /Class/Delete
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //Delete the data from database
            await _classRepository.DeleteAsync(id);

            // Redirect to List all class page
            return RedirectToAction("Index", "Class");
        }
    }
}
