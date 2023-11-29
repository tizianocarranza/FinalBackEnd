using AutoMapper;
using FinalBackEnd.DTOs;
using FinalBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalBackEnd.Interfaces
{
    public class ClaseRepositorio : IClaseRepositorio
    {
        private readonly FinalBackEndContext _context;
        private IMapper _mapper;
        public ClaseRepositorio(FinalBackEndContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClaseDto> CrearOActualizar(ClaseDto claseDto, int id = 0)
        {
            var clase = _mapper.Map<ClaseDto, Clase>(claseDto);

            if (id == 0)
            {
                await _context.Clases.AddAsync(clase);
            }
            else
            {
                clase.Id = id;
                _context.Clases.Update(clase);
            }

            await _context.SaveChangesAsync();

            var claseDtoResult = _mapper.Map<Clase, ClaseDto>(clase);

            return claseDtoResult;
        }


        public async Task<bool> DeleteClase(int id)
        {
            try
            {
                var clase = await _context.Clases.FindAsync(id);
                if (clase != null)
                {
                    _context.Clases.Remove(clase);
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

        public async Task<List<ClaseDto>> GetClases()
        {
            var clases = await _context.Clases.ToListAsync();
            //List<PersonasDto> personasDto = new List<PersonasDto>();
            //foreach(var persona in personas)
            //{
            //    var personaDto = new PersonasDto();
            //    personaDto.Nombre = persona.Nombre;
            //    personaDto.Apellido = persona.Apellido;
            //    personasDto.Add(personaDto);

            //}
            // Mapea cada Socio a SocioDto
            var clasesDtoResult = _mapper.Map<List<Clase>, List<ClaseDto>>(clases);

            return clasesDtoResult;
        }

        public async Task<ClaseDto> GetClaseById(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            var claseDtoResult = _mapper.Map<Clase, ClaseDto>(clase);

            return claseDtoResult;
        }
    }
}
