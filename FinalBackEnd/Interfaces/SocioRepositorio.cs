using AutoMapper;
using FinalBackEnd.DTOs;
using FinalBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalBackEnd.Interfaces
{
    public class SocioRepositorio : ISocioRepositorio
    {
        private readonly FinalBackEndContext _context;
        private IMapper _mapper;
        public SocioRepositorio (FinalBackEndContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SocioDto> CrearOActualizar(SocioDto socioDto, int id = 0)
        {
            var socio = _mapper.Map<SocioDto, Socio>(socioDto);

            if (id == 0)
            {
                await _context.Socios.AddAsync(socio);
            }
            else
            {
                socio.Id = id;
                _context.Socios.Update(socio);
            }

            await _context.SaveChangesAsync();

            var socioDtoResult = _mapper.Map<Socio, SocioDto>(socio);

            return socioDtoResult;
        }


        public async Task<bool> DeleteSocio(int id)
        {
            try
            {
                var socio = await _context.Socios.FindAsync(id);
                if (socio != null)
                {
                    _context.Socios.Remove(socio);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<SocioDto>> GetSocios()
        {
            var socios = await _context.Socios.ToListAsync();
            //List<PersonasDto> personasDto = new List<PersonasDto>();
            //foreach(var persona in personas)
            //{
            //    var personaDto = new PersonasDto();
            //    personaDto.Nombre = persona.Nombre;
            //    personaDto.Apellido = persona.Apellido;
            //    personasDto.Add(personaDto);

            //}
            // Mapea cada Socio a SocioDto
            var sociosDtoResult = _mapper.Map<List<Socio>, List<SocioDto>>(socios);

            return sociosDtoResult;
        }

        public async Task<SocioDto> GetSocioById(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            var socioDtoResult = _mapper.Map<Socio, SocioDto>(socio);

            return socioDtoResult; 
        }
    }
}
