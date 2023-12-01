using MedicalAppointmentSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.Services
{
    public interface ISpecializationAppService
    {
        Task<List<SpecializationDTO>> GetAllSpecializations();
    }
}
