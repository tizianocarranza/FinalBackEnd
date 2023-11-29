using FinalBackEnd.DTOs;

namespace FinalBackEnd.Interfaces
{
    public interface IClaseRepositorio
    {
        Task<List<ClaseDto>> GetClases();
        Task<ClaseDto> GetClaseById(int id);
        Task<ClaseDto> CrearOActualizar(ClaseDto socio, int id = 0);
        Task<bool> DeleteClase(int id);
    }
}
