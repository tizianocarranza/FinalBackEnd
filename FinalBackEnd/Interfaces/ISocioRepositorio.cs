using FinalBackEnd.DTOs;

namespace FinalBackEnd.Interfaces
{
    public interface ISocioRepositorio
    {
        Task<List<SocioDto>> GetSocios();
        Task<SocioDto> GetSocioById(int id);
        Task<SocioDto> CrearOActualizar(SocioDto socio, int id = 0);
        Task<bool> DeleteSocio(int id);

    }
}
