using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Filters;
using MVC.Models;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace MVC.Controllers
{

    [TypeFilter(typeof(ExceptionFilter))]
    public class CoursesController : Controller
    {
        private readonly ICoursesRepository coursesRepository;

        public CoursesController(ICoursesRepository coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }

        // GET: Courses
        public async Task<ActionResult> Index()
        {
            var data = await coursesRepository.GetAllAsync();
            var model = data.Select(x => new CourseModel
            {
                Id = x.Id,
                LectorName = x.LectorName,
                Name = x.Name
            });
            return View(model);
        }

        // GET: Courses/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var data = await coursesRepository.GetSingleAsync(id);
            var model = new CourseModel
            {
                Id = data.Id,
                LectorName = data.LectorName,
                Name = data.Name
            };
            return View(model);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseModel data)
        {
            try
            {
                await coursesRepository.CreateAsync(new Course
                {
                    Id = data.Id,
                    LectorName = data.LectorName,
                    Name = data.Name
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await coursesRepository.GetSingleAsync(id);
            var model = new CourseModel
            {
                Id = data.Id,
                LectorName = data.LectorName,
                Name = data.Name
            };
            return View(model);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CourseModel data)
        {
            try
            {
                await coursesRepository.UpdateAsync(new Course
                {
                    Id = id,
                    LectorName = data.LectorName,
                    Name = data.Name
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await coursesRepository.GetSingleAsync(id);
            var model = new CourseModel
            {
                Id = data.Id,
                LectorName = data.LectorName,
                Name = data.Name
            };
            return View(model);
        }

        // POST: Courses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await coursesRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}