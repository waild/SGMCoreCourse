using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreGroupHW8.Pages.Groups
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly GroupsRepository repository;

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new GroupsRepository(connectionString);
        }

        public IList<Group> Group { get; set; }

        public async Task OnGetAsync()
        {
            Group = await repository.GetAllAsync();
        }
    }
}