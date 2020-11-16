using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SimpleWebAPI.Controllers;
using SimpleWebAPI.Services;
using System;
using System.Collections.Generic;

namespace SimpleWebAPI.Tests {
    public class PersonControllerTests {
        List<Person> samplePeople = new List<Person>() {
            new Person { FirstName = "Laz", LastName = "Padron", Address = "123 Rainbow Road", Age = 29, Interests = "Video Games" },
            new Person { FirstName = "Baxter", LastName = "Padron", Address = "123 Rainbow Road", Age = 9, Interests = "Barking" },
            new Person { FirstName = "Jenny", LastName = "Padron", Address = "123 Rainbow Road", Age = 26, Interests = "Roller Derby" }
        };

        [Test]
        public void GetsAll_NoQueryAllResults() {
            var mockRepository = new Mock<IPersonRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(samplePeople);

            PersonController controller = new PersonController(null, mockRepository.Object);

            var result = controller.Get();
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result[0].FirstName == "Laz");
        }

        [Test]
        public void GetsAll_QuerySingleResult() {
            var mockRepository = new Mock<IPersonRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(samplePeople);

            PersonController controller = new PersonController(null, mockRepository.Object);

            var result = controller.Get("bax");
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].FirstName == "Baxter");
        }

        [Test]
        public void GetsAll_QueryManyResults() {
            var mockRepository = new Mock<IPersonRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(samplePeople);

            PersonController controller = new PersonController(null, mockRepository.Object);

            var result = controller.Get("pad");
            Assert.IsTrue(result.Count > 1);
        }

        [Test]
        public void GetsAll_QueryNoResults() {
            var mockRepository = new Mock<IPersonRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(samplePeople);

            PersonController controller = new PersonController(null, mockRepository.Object);

            var result = controller.Get("hello there");
            Assert.IsTrue(result.Count == 0);
        }

        [Test]
        public void CreatePerson_Created() {
            var samplePerson = samplePeople[0];
            var mockRepository = new Mock<IPersonRepository>();
            mockRepository.Setup(x => x.Add(samplePerson)).Verifiable();

            PersonController controller = new PersonController(null, mockRepository.Object);

            StatusCodeResult result = controller.Create(samplePerson);
            mockRepository.Verify(x => x.Add(samplePerson), Times.Once);
            Assert.IsTrue(result.StatusCode == 201);
        }

        [Test]
        public void CreatePerson_ExceptionThrown() {
            var samplePerson = samplePeople[0];
            samplePerson.Id = 7;
            var mockRepository = new Mock<IPersonRepository>();
            mockRepository.Setup(x => x.Add(samplePerson)).Throws(new Exception());

            PersonController controller = new PersonController(null, mockRepository.Object);

            StatusCodeResult result = controller.Create(samplePerson);
            Assert.IsTrue(result.StatusCode == 500);
        }
    }
}