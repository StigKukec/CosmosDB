using App.DALModels;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;

namespace App.Controllers
{
    public class PersonController : Controller
    {
        private readonly ICosmosDBService service = CosmosDBServiceProvider.Service!;
        // GET: PersonController
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM Person"));
        }

        // GET: PersonController/Details/5
        public async Task<ActionResult> Details(string id) => await ShowPerson(id);

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMPerson person)
        {

            if (ModelState.IsValid)
            {
                person.IDPerson = Guid.NewGuid().ToString();
                string? contacts = Request.Form["storedContacts"];
                if (contacts != null && contacts != string.Empty)
                {
                    List<VMContact> vmContacts = new List<VMContact>();
                    string[] detailedContacts = contacts!.Split(',');
                    for (int i = 0; i < detailedContacts.Length; i++)
                    {
                        vmContacts.Add(new VMContact() { IDContact = Guid.NewGuid().ToString(), Number = detailedContacts[i], PersonID = person.IDPerson });    
                    }
                    person.Contact = vmContacts;
                }
                await service.CreatePersonAsync(person);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: PersonController/Edit/5
        public async Task<ActionResult> Edit(string id) => await ShowPerson(id);

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, VMPerson person)
        {
            person = await service.GetPersonAsync(id);
            string? contacts = Request.Form["storedContacts"];
            if (contacts != null && contacts != string.Empty)
            {
                List<VMContact> vmContacts = new List<VMContact>();
                string[] detailedContacts = contacts!.Split(',');
                for (int i = 0; i < detailedContacts.Length; i++)
                {
                    vmContacts.Add(new VMContact() { IDContact = Guid.NewGuid().ToString(), Number = detailedContacts[i], PersonID = person.IDPerson });
                }
                person.Contact = vmContacts;
            }
            if (ModelState.IsValid)
            {
                await service.UpdatePersonAsync(person);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: PersonController/Delete/5
        public async Task<ActionResult> Delete(string id) => 
            await ShowPerson(id);

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, VMPerson person)
        {
            person = await service.GetPersonAsync(id);
            await service.DeletePersonAsync(person);
            return RedirectToAction(nameof(Index));
        }
        private async Task<ActionResult> ShowPerson(string id)
        {
            if (id == null)
                return BadRequest();

            var person = await service.GetPersonAsync(id);
            if (person == null)
                return NotFound();

            return View(person);
        }
        
        public async Task<ActionResult> AddContact(string id) => 
            await ShowPerson(id);

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContact(string id, VMPerson person)
        {
            person = await service.GetPersonAsync(id);
            await service.DeletePersonAsync(person);
            return RedirectToAction(nameof(Index));
        }
    }
}
