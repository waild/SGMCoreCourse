using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        private readonly StudentsRepository repository;
        public StudentsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new StudentsRepository(connectionString);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await repository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await repository.GetSingleAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Student model)
        {
            var response = await repository.CreateAsync(model);
            return Ok(response);
        }



        [HttpPut]
        public async Task<ActionResult> Update(int id, Student model)
        {
            model.Id = id;
            await repository.UpdateAsync(model);
            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }
    }
}
