using AutoMapper;
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
        }

        public void CreatePersonMappings()
        {
            CreateMap<PersonModel, Person>().ReverseMap();
        }
    }
}
