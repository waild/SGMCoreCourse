using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreCourseHW8.Pages.Shared
{
    public class DetailsModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly CoursesRepository repository;

        public DetailsModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new CoursesRepository(connectionString);
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Course = await repository.GetSingleAsync(id.Value);

            if (Course == null) return NotFound();
            return Page();
        }
    }
}