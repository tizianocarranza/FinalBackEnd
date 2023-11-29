using AutoMapper;
using FinalBackEnd.DTOs;
using FinalBackEnd.Models;

namespace FinalBackEnd.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping() {
            CreateMap<SocioDto, Socio>();
            CreateMap<ClaseDto, Clase>();   
        }
    }
}
