using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreCourseHW8.Pages.Shared
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly CoursesRepository repository;

        public CreateModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new CoursesRepository(connectionString);
        }

        [BindProperty] public Course Course { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await repository.CreateAsync(Course);

            return RedirectToPage("./Index");
        }
    }
}