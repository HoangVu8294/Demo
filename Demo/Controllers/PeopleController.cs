using Demo.Data;
using Demo.Models;
using Demo.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class PeopleController : Controller
    {
        private readonly Context dbContext;
        public PeopleController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await dbContext.People.ToListAsync();
            return View(people);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPersonModel addPersonRequest) 
        {
            var person = new Person()
            {
                Id = Guid.NewGuid(),
                Name = addPersonRequest.Name,
                Email = addPersonRequest.Email
            };

            await dbContext.People.AddAsync(person);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(x => x.Id == id);
            if (person != null)
            {
                var updatePersonModel = new UpdatePersonModel()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Email = person.Email
                };

                return await Task.Run(() => View("View", updatePersonModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdatePersonModel updatePersonModel)
        {
            var person = await dbContext.People.FindAsync(updatePersonModel.Id);
            if (person != null)
            {
                person.Name = updatePersonModel.Name;
                person.Email = updatePersonModel.Email;

                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePersonModel updatePersonModel)
        {
            var person = await dbContext.People.FindAsync(updatePersonModel.Id);
            if (person != null)
            {
                dbContext.People.Remove(person);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
