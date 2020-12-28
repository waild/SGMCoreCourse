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
    public class GroupsController : Controller
    {
        private readonly IGroupsRepository groupsRepository;

        public GroupsController(IGroupsRepository groupsRepository)
        {
            this.groupsRepository = groupsRepository;
        }

        // GET: Groups
        public async Task<ActionResult> Index()
        {
            var data = await groupsRepository.GetAllAsync();
            var model = data.Select(x => new GroupModel
            {
                Id = x.Id,
                Name = x.Name,
                Year = x.Year
            });
            return View(model);
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var data = await groupsRepository.GetSingleAsync(id);
            var model = new GroupModel
            {
                Id = data.Id,
                Name = data.Name,
                Year = data.Year
            };
            return View(model);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupModel data)
        {
            try
            {
                await groupsRepository.CreateAsync(new Group
                {
                    Name = data.Name,
                    Year = data.Year
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Groups/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await groupsRepository.GetSingleAsync(id);
            var model = new GroupModel
            {
                Id = data.Id,
                Name = data.Name,
                Year = data.Year
            };
            return View(model);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, GroupModel data)
        {
            try
            {
                await groupsRepository.UpdateAsync(new Group
                {
                    Id = id,
                    Name = data.Name,
                    Year = data.Year
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await groupsRepository.GetSingleAsync(id);
            var model = new GroupModel
            {
                Id = data.Id,
                Name = data.Name,
                Year = data.Year
            };
            return View(model);
        }

        // POST: Groups/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await groupsRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}