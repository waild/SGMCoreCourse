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
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository coursesRepository;

        public StudentsController(IStudentsRepository coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }

        // GET: Students
        public async Task<ActionResult> Index()
        {
            var data = await coursesRepository.GetAllAsync();
            var model = data.Select(x => new StudentModel
            {
                Id = x.Id,
                AverageMark = x.AverageMark,
                BirthYear = x.BirthYear,
                FirstName = x.FirstName,
                LastName = x.LastName,
                GroupId = x.GroupId
            });
            return View(model);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var data = await coursesRepository.GetSingleAsync(id);
            var model = new StudentModel
            {
                Id = data.Id,
                AverageMark = data.AverageMark,
                BirthYear = data.BirthYear,
                FirstName = data.FirstName,
                LastName = data.LastName,
                GroupId = data.GroupId
            };
            return View(model);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentModel data)
        {
            try
            {
                await coursesRepository.CreateAsync(new Student
                {
                    AverageMark = data.AverageMark,
                    BirthYear = data.BirthYear,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    GroupId = data.GroupId
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await coursesRepository.GetSingleAsync(id);
            var model = new StudentModel
            {
                Id = data.Id,
                AverageMark = data.AverageMark,
                BirthYear = data.BirthYear,
                FirstName = data.FirstName,
                LastName = data.LastName,
                GroupId = data.GroupId
            };
            return View(model);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, StudentModel data)
        {
            try
            {
                await coursesRepository.UpdateAsync(new Student
                {
                    Id = id,
                    AverageMark = data.AverageMark,
                    BirthYear = data.BirthYear,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    GroupId = data.GroupId
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await coursesRepository.GetSingleAsync(id);
            var model = new StudentModel
            {
                Id = data.Id,
                AverageMark = data.AverageMark,
                BirthYear = data.BirthYear,
                FirstName = data.FirstName,
                LastName = data.LastName,
                GroupId = data.GroupId
            };
            return View(model);
        }

        // POST: Students/Delete/5
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