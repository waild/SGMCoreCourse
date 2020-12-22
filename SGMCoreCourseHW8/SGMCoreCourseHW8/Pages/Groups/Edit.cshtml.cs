using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreGroupHW8.Pages.Groups
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly GroupsRepository repository;

        public EditModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new GroupsRepository(connectionString);
        }

        [BindProperty] public Group Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Group = await repository.GetSingleAsync(id.Value);

            if (Group == null) return NotFound();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            await repository.UpdateAsync(Group);
            return RedirectToPage("./Index");
        }
    }
}