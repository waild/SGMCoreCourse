using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreGroupHW8.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly GroupsRepository groupsRepository;
        private readonly StudentsRepository repository;

        public CreateModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new StudentsRepository(connectionString);
            groupsRepository = new GroupsRepository(connectionString);
        }

        public IList<Group> Groups { get; set; }

        [BindProperty] public Student Student { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Groups = await groupsRepository.GetAllAsync();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await repository.CreateAsync(Student);

            return RedirectToPage("./Index");
        }
    }
}