using ContactService.Controller;
using ContactService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ContactService.Test {
    [TestClass]
    public class UnitTest {

        static ContactModel model = new ContactModel();
        static ContactAPIController controller;

        static UnitTest() {
            controller = new ContactAPIController(model);

            model.Contacts.Add(new Contact() { Id = 1, FirstName = "Adami", LastName = "Philipp", Email = "Adami.Philipp@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 2, FirstName = "Arbeithuber", LastName = "Gregor", Email = "Arbeithuber.Gregor@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 3, FirstName = "Barth", LastName = "Daniel", Email = "Barth.Daniel@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 4, FirstName = "Bauer", LastName = "Wolfgang", Email = "Bauer.Wolfgang@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 5, FirstName = "Benkner", LastName = "Matthias", Email = "Benkner.Matthias@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 6, FirstName = "Buchinger", LastName = "David", Email = "Buchinger.David@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 7, FirstName = "Ebner", LastName = "Mario", Email = "Ebner.Mario@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 8, FirstName = "El Banna", LastName = "Maurice", Email = "El Banna.Maurice@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 9, FirstName = "Gornik", LastName = "Stefan", Email = "Gornik.Stefan@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 10, FirstName = "Gusenleitner", LastName = "Philipp", Email = "Gusenleitner.Philipp@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 11, FirstName = "Juster", LastName = "Lukas Benjamin", Email = "Juster.Lukas Benjamin@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 12, FirstName = "KARL", LastName = "Jan", Email = "KARL.Jan@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 13, FirstName = "Köck", LastName = "Michael Karl", Email = "Köck.Michael Karl@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 14, FirstName = "Leitner", LastName = "Manuel", Email = "Leitner.Manuel@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 15, FirstName = "Maurhart", LastName = "Fabian", Email = "Maurhart.Fabian@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 16, FirstName = "Mayr", LastName = "Patrick", Email = "Mayr.Patrick@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 17, FirstName = "Öhlinger", LastName = "Günter", Email = "Öhlinger.Günter@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 18, FirstName = "Prömer", LastName = "Fabian Leon", Email = "Prömer.Fabian Leon@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 19, FirstName = "Schalhas", LastName = "Matthias", Email = "Schalhas.Matthias@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 20, FirstName = "Schöller", LastName = "Michael", Email = "Schöller.Michael@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 21, FirstName = "Schörghuber", LastName = "Franz - Filip", Email = "Schörghuber.Franz - Filip@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 22, FirstName = "Wallner", LastName = "Alexander", Email = "Wallner.Alexander@htlperg.at" });
            model.Contacts.Add(new Contact() { Id = 23, FirstName = "Zeiml", LastName = "Lukas", Email = "Zeiml.Lukas@htlperg.at" });
            model.SaveChanges();
        }


        [TestMethod]
        public void TestFilterLike() {
            var found = (IEnumerable<Contact>)((OkObjectResult)controller.Get("Sch%")).Value;

            CollectionAssert.AreEquivalent(new int[] { 19, 20, 21 }, found.Select(c => c.Id).ToArray());
        }

        [TestMethod]
        public void TestFilterMultipleCriteria() {
            var found = (IEnumerable<Contact>)((OkObjectResult)controller.Get("B m")).Value;
            CollectionAssert.AreEquivalent(new int[] { 5, 7, 8, 11, 15, 18 }, found.Select(c => c.Id).ToArray());
        }
    }
}
