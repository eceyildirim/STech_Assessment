using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PhoneDirectory.UnitTests
{
    public class PersonOperations
    {
        public PersonModel person;
        public readonly IPersonService _personService;

        public PersonOperations(IPersonService personService)
        {
            _personService = personService;
        }

        [Fact]
        public void Task_NameSurnameCompanyNullRegistered_Return()
        {
            person = new PersonModel
            {
                Company = "",
                Name = "",
                Surname = ""
            };
            var result = _personService.CreatePerson(new PersonModel());
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(StatusCodes.Status500InternalServerError, result.Code);
        }
    }
}
