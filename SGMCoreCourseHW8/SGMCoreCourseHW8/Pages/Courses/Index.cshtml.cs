using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreCourseHW8.Pages.Shared
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly CoursesRepository repository;

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new CoursesRepository(connectionString);
        }

        public IList<Course> Course { get; set; }

        public async Task OnGetAsync()
        {
            Course = await repository.GetAllAsync();
        }
    }
}