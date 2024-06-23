using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sandesh_SMS.Data;
using Sandesh_SMS.Models;
using Sandesh_SMS.Repositories;
using Sandesh_SMS.ViewModels;

namespace Sandesh_SMS.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //Dependency injection
        public StudentController(IStudentRepository studentRepository, AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageNumber, string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            //Sorting
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateOfBirthSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["IsActiveSortParam"] = sortOrder == "isactive_asc" ? "isactive_desc" : "isactive_asc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = _studentRepository.GetAllAsync();

            // Search functionality
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;

                case "date_asc":
                    students = students.OrderBy(s => s.DateOfBirth);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "isactive_desc":
                    students = students.OrderByDescending(s => s.IsActive);
                    break;
                case "isactive_asc":
                    students = students.OrderBy(s => s.IsActive);
                    break;

                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }

            // Ensure pageNumber is at least 1
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            int pageSize = 5;
            return View(await PaginatedList<StudentViewModel>.CreateAsync(students, pageNumber, pageSize));

        }
        //GET: Student/Add
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var classes = await _studentRepository.GetAllClasses();
            var courses = await _studentRepository.GetAllCourses();
            ViewBag.Classes = new SelectList(classes, "ClassId", "ClassName");
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");

            return View();
        }

        //POST: Student/Add
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return to the form with validation errors
            }
            // Handle image upload
            //if (model.StdProfile != null)
            //{
            //    // Ensure the uploads folder exists
            //    string folder = "photo";
            //    folder += model.StdProfile.FileName + Guid.NewGuid().ToString();
            //    string severFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            //    await model.StdProfile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));



                // Save the student to the database
                await _studentRepository.AddAsync(model);

            // Redirect to List all class page
            return RedirectToAction("Index", "Student");
        }
        //GET: Student/Edit
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Fetch class details
            var classes = await _studentRepository.GetAllClasses();
            var courses = await _studentRepository.GetAllCourses();
            ViewBag.Classes = new SelectList(classes, "ClassId", "ClassName");
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");

            //Fetch the class details
            var student = await _studentRepository.GetByIdAsync(id);
            return View(student);
        }

        //POST: Student/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel student)
        {

            if (!ModelState.IsValid)
            {
                return View(student); // Return to the form with validation errors
            }
            //Update the database with modified details
            await _studentRepository.UpdateAsync(student);

            // Redirect to List all class page
            return RedirectToAction("Index", "Student");
        }

        //GET: /Class/Delete
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //Delete the data from database
            await _studentRepository.DeleteAsync(id);
            // Redirect to List all class page
            return RedirectToAction("Index", "Student");
        }
        // GET: Student/Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Fetch the student details
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        
    }
}

