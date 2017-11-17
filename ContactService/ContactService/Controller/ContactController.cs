using ContactService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactService.Controller {

    [Route("api/contacts")]
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller {

        public ContactModel ContactModel { get; }

        public ContactController(ContactModel contactModel) {
            ContactModel = contactModel;
        }


        [HttpPost]
        public IActionResult Add([FromBody] Contact contact) {
            try {
                ContactModel.Add(contact);
                ContactModel.SaveChanges();
            } catch (ArgumentException) {
                return StatusCode(400, "Invalid input(e.g.required field missing or empty)");
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult Get() => Ok(ContactModel.Contacts);

        [HttpDelete, Route("{personId}")]
        public IActionResult Delete([FromRoute]string personIdString) {
            int personId;
            if (!int.TryParse(personIdString, out personId))
                return StatusCode(400, "Invalid ID supplied");
            var person = ContactModel.Contacts.Find(personId);
            if (person == null)
                return StatusCode(404, "Person not found");
            ContactModel.Remove(person);
            ContactModel.SaveChanges();
            return Ok();
        }

        [HttpGet, Route("findByName/{nameFilter}"), Route("findByName")]
        public IActionResult Get([FromRoute]string nameFilter) {
            string[] parts = (nameFilter ?? "").Split(' ');
            IEnumerable<Contact> res = ContactModel.Contacts;
            foreach (string part in parts)
                res = res.Where(c => EF.Functions.Like(c.FirstName, part) || EF.Functions.Like(c.LastName, part));
            int count = res.Count();
            if (count == 0) {
                return StatusCode(400, "Invalid or missing name");
            }
            return Ok(res);
        }
    }
}
