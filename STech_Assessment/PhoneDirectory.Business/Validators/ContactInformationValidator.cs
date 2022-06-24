using FluentValidation;
using PhoneDirectory.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Validators
{
    public class ContactInformationValidator : AbstractValidator<ContactInformationModel>
    {
        public ContactInformationValidator()
        {
            RuleFor(x => x.ContactInformationContent).EmailAddress().When(x => x.ContactInformationType == Core.ContactInformationType.Email).WithMessage("Lütfen doğru formatta giriş yapınız.");
            RuleFor(x => x.ContactInformationContent).Matches(@"[0-9]").MinimumLength(10).MaximumLength(10).When(x => x.ContactInformationType == Core.ContactInformationType.Phone).WithMessage("Lütfen doğru formatta giriş yapınız.");
        }
    }
}
