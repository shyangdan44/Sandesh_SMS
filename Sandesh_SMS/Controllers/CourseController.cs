using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sandesh_SMS.Repositories;
using Sandesh_SMS.ViewModels;

namespace Sandesh_SMS.Controllers
{
    [Authorize(Roles = "Admin,,User")]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IActionResult> Index()
        {
            //Fetch the data from database
            var courses = await _courseRepository.GetAllAsync();
            return View(courses);
        }
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CourseViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model); // Return to the form with validation errors
            }

            //Insert data to the database           
            await _courseRepository.AddAsync(model);

            // Redirect to List all Course page
            return RedirectToAction("Index", "Course");
        }

        //GET: /Course/Edit
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var classe = await _courseRepository.GetByIdAsync(id);
            return View(classe);
        }
        //POST: /Course/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CourseViewModel course)
        {

            if (ModelState.IsValid)
            {
                //Update the database with modified details
                await _courseRepository.UpdateAsync(course);

                // Redirect to List all Course page
                return RedirectToAction("Index", "Course");
            }

            // If the model is not valid, return the view with the validation errors
            return View(course);
        }

        //GET: /Course/Delete
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //Delete the data from database
            await _courseRepository.DeleteAsync(id);

            // Redirect to List all Course page
            return RedirectToAction("Index", "Course");
        }
    }
}
