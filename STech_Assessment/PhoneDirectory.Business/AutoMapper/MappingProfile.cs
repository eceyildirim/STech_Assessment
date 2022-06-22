using AutoMapper;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreatePersonMappings();
            CreateContactInformationMappings();
        }

        public void CreatePersonMappings()
        {
            CreateMap<PersonModel, Person>().ReverseMap();
            CreateMap<PersonModel, PersonLookedUp>().ReverseMap();
            CreateMap<Person, PersonLookedUp>().ReverseMap();
        }

        public void CreateContactInformationMappings()
        {
            CreateMap<ContactInformationModel, ContactInformation>().ReverseMap();
            CreateMap<ContactInformationModel, ContanctInformationLookedUp>();
            CreateMap<ContactInformation, ContanctInformationLookedUp>().ReverseMap();
        }
    }
}
