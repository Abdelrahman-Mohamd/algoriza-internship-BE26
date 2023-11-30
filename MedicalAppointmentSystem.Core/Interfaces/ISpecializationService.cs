using MedicalAppointmentSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Interfaces
{
    public interface ISpecializationService
    {
        Task<List<SpecializationModel>> GetAllSpecializations();
    }
}
