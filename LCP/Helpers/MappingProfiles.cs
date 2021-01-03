using AutoMapper;
using LCP.Core.Entities;
using LCP.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCP.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Client, ReturnClientDto>();
            CreateMap<ClientToBeAdded, Client>();
        }
    }
}
