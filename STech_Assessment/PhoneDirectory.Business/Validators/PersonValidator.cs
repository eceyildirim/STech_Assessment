using FluentValidation;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Validators
{
    public class PersonValidator : AbstractValidator<PersonModel>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().When(x => x.Surname == null && x.Company == null).WithMessage(CustomMessage.ThisFieldIsRequired);
            RuleFor(x => x.Surname).NotEmpty().NotNull().When(x => x.Name == null && x.Company == null).WithMessage(CustomMessage.ThisFieldIsRequired);
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen doğru formatta giriş yapınız.");
            RuleFor(x => x.PhoneNumber).Matches(@"[0-9]").MinimumLength(10).MaximumLength(10).WithMessage("Lütfen doğru formatta giriş yapınız.");
            RuleFor(x => x.Company).NotEmpty().NotNull().When(x => x.Name == null && x.Surname == null).WithMessage(CustomMessage.ThisFieldIsRequired);
        }
    }
}
