using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreGroupHW8.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly StudentsRepository repository;

        public DeleteModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new StudentsRepository(connectionString);
        }

        [BindProperty] public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await repository.GetSingleAsync(id.Value);

            if (Student == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await repository.GetSingleAsync(id.Value);

            if (Student != null) await repository.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}