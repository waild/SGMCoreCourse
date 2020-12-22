using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreGroupHW8.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly StudentsRepository repository;

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new StudentsRepository(connectionString);
        }

        public IList<Student> Course { get; set; }

        public async Task OnGetAsync()
        {
            Course = await repository.GetAllAsync();
        }
    }
}