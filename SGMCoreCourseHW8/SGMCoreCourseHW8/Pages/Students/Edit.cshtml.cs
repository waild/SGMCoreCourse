﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreGroupHW8.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly GroupsRepository groupsRepository;
        private readonly StudentsRepository repository;

        public EditModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new StudentsRepository(connectionString);
            groupsRepository = new GroupsRepository(connectionString);
        }

        [BindProperty] public Student Student { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await repository.GetSingleAsync(id.Value);

            if (Student == null) return NotFound();
            var gr = await groupsRepository.GetAllAsync();
            Groups = gr.Select(x => new SelectListItem(x.Name, x.Id.ToString()));

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            await repository.UpdateAsync(Student);
            return RedirectToPage("./Index");
        }
    }
}