using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreGroupHW8.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly StudentsRepository repository;

        public DetailsModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new StudentsRepository(connectionString);
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await repository.GetSingleAsync(id.Value);

            if (Student == null) return NotFound();
            return Page();
        }
    }
}